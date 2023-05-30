using System;
using System.Collections.Generic;
using System.Linq;

namespace WebScraper
{
    internal class Program
    {
        // обработчик события, вызываемый при обнаружении картинок
        public static void ConsoleHandler(Uri page, List<string[]> imgs) 
        {
            Console.WriteLine($"\nPage:\t{page}\nImages"); // выводим адреса страницы
            if (imgs.Any()) // если массив картинок не пустой
            {
                var counter = 0;
                foreach (var img in imgs)
                {
                    counter++; // счетчик
                    if (counter < 4)
                        Console.Write($"\tLink:{img[0]}\tName:{img[1]}\n");
                }
            }
            else
            {
                Console.WriteLine("\tNo images");
            }
        }
        // https://chel.zenmod.shop/?ysclid=lbj6hcrdg5617023254
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter the link to the site from which you want to retrieve the image data:\t");
            Console.WriteLine("Example:https://chel.zenmod.shop/?ysclid=lbj6hcrdg5617023254\t");

            using (Scanner scanner = new Scanner()) // создаем экземпляр сканера, который уничтожится после выполнения блока using
            {
                scanner.TargetFound += ConsoleHandler; // к событию TargetFound добавляем обработчик ConsoleHandler
                scanner.Scan(new Uri(Console.ReadLine()), 7); // запускаем сканирование с глубиной в 3 страницы
            }
        }
    }
}