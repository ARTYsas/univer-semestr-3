using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace WebScraper
{
    public class Scanner : IDisposable // наследование от IDisposable означает, что у класса есть метод Dispose(), разрушающий объект
    {
        private readonly HashSet<Uri> _processedLinks = new HashSet<Uri>(); // хэш-таблица для хранения обработанных ссылок
        private readonly WebClient _webClient = new WebClient(); // сущность вебклиента, через который и будут запращиваться веб-страницы

        private readonly HashSet<string> _ignoreFiles = new HashSet<string> { ".ico", ".xml", ".png", ".jpg", ".gif" };
        // список игнорируемых расширений файлов. Картинки и иконки не нужно обрабатывать
        // К примеру страница http://www.example.com/favicon.ico обработана не будет
        
        public event Action<Uri, List<string[]>> TargetFound; // событие, которое будет вызываться при обнаружении адресов или телефонов
        private void OnTargetFound(Uri page, List<string[]> imgs) // метод возбуждающий событие TargetFound с полученными аргументами
        {
            TargetFound?.Invoke(page, imgs);
        }

        public void Scan(Uri startPage, int pageCount) // метод глубокого сканирования
        {
            _processedLinks.Clear(); // очищаем список обработанных ссылок
            var domain = $"{startPage.Scheme}://{startPage.Host}"; // приводим домен к виду http://www.example.com

            Process(domain, startPage, pageCount); // запускаем рекурсивную обработку начиная с указанной страницы
        }
        
        private void Process(string domain, Uri page, int count) // рекурсивный метод обработки страницы
        {
            if (count < 0) return; // если глубина обработки бессмысленна, то выходим из метода
            if (_processedLinks.Contains(page)) return; // если ссылка уже обработана, то выходим из метода
            _processedLinks.Add(page); // добавляем ссылку в список обработанных

            string html;
            try
            {
                html = _webClient.DownloadString(page); // скачиваем html-код страницы
            }
            catch (WebException)
            {
                return; // если страница не доступна, то выходим из метода
            }
            
            var hrefs = (from href in Regex.Matches(html, @"href=""[\/\w-\.:]+""").Cast<Match>()
                    where href.Value.Replace("href=", "").Trim('"').StartsWith("/")
                    let url = href.Value.Replace("href=", "").Trim('"')
                    select new Uri($"{domain}{url}")
                ).ToList(); // регулярное выражение для поиска ЛОКАЛЬНЫХ ссылок на странице. На выходе список ссылок. По ним дальше будет идти сканирование
            
            var imgs = (from img in Regex.Matches(html, @"(<img\s)[^>]*(src=\S+)[^>]*(\stitle=[""|'].*?[""|'])[^>]*(\/?>)").Cast<Match>()
                    //обрезаем от src до пробела
                    let src = img.Value.Substring(img.Value.IndexOf("src=") + 4, img.Value.IndexOf(" ", img.Value.IndexOf("src=")) - img.Value.IndexOf("src=") - 4)
                    //обрезаем от title до "
                    let title = img.Value.Substring(img.Value.IndexOf("title=\"") + 7, img.Value.IndexOf("\"", img.Value.IndexOf("title=\"") + 7) - img.Value.IndexOf("title=\"") - 7)
                    select new []{src, title}
                ).ToList(); // регулярное выражение для поиска ссылок на картинки.
            
            if (imgs.Count > 0) // если хоть что-то найдено, то вызываем событие
                OnTargetFound(page, imgs);


            foreach (var href in hrefs) // для каждой полученной ссылки
            {
                string fileEx = Path.GetExtension(href.LocalPath).ToLower(); // получаем расширение файла
                if (_ignoreFiles.Contains(fileEx)) continue; // если расширение файла в списке игнорируемых, то пропускаем ссылку

                Process(domain, href, --count); // обрабатываем каждую полученную ссылку
            }
        }

        public void Dispose()
        {
            _webClient.Dispose(); // отключаем веб-клиент
        }
    }
}