namespace Lab5;

public partial class AuthForm : Form
{  // код по большей мере взят из того, что было приложено в курсе.
    string authUrl = "https://oauth.vk.com/authorize"; // адрес авторизации
    string redirectUrl = "https://oauth.vk.com/blank.html"; // адрес переадресации, в котором будет храниться токен доступа к API
    string scope; // скоуп - переменная разрешения доступа к API. 

    int clientId = 51651226; // ID приложения. вставить сюда своё.

    public string Token { get; private set; } // уникальный токен доступа к API вк, меняется с каждым запросом.

    public AuthForm(string scope)
    {
        InitializeComponent();
        this.scope = scope; // scope - строка доступа. то есть, для доступа к документам туда нужно передать docs, для доступа к друзьям friends и т.д.
        webBrowser.AddressChanged += WebBrowserNavigated; // подписка на событие, происходящее, когда сменяется адрес WebBrowser
    }

    private void AuthFormLoaded(object sender, EventArgs e)
    {
        webBrowser.Load(authUrl + // как только форма загрузилась, нас тут же перекинет на форму авторизации с нужными аргументами.
                        $"?client_id={clientId}&display=page&redirect_uri={redirectUrl}&response_type=token&v=5.131&scope={scope}&state=123&revoke=0");
    }

    private void WebBrowserNavigated(object sender, EventArgs e) // обработчик события смены адреса браузера
    {
        string uri = webBrowser.Address; // получает адресную строку из свойства браузера
        if (uri.StartsWith(authUrl)) return; // если адрес начинается со строки авторизации, то сразу пропускаем этот шаг, тут ничего полезного нет
        if (uri.StartsWith(redirectUrl)) // если начинается с адреса переадресации, в котором генерируется токен, то мы создаем LINQ выражение
        {
            var parameters = (uri.Split('#')[1] // это выражение было полностью взято из тех материалов, что были приложены в курсе
                .Split('&')
                .Select(param => new { param, parts = param.Split('=') })
                .Select(t => new { Name = t.parts[0], Value = t.parts[1] })).ToDictionary(v => v.Name, v => v.Value); 
                            // финальный Select возвращает словарь
            Token = parameters["access_token"]; // получаем из словаря значение токена
            DialogResult = DialogResult.OK; // присвоение значения свойству DialogResult. после его присвоения будет закрыто окно, т.к. оно открыто как ShowDialog
        }
    }
}
