using System.Net;
using System.Text;

namespace Lab5
{
    public partial class MainForm : Form
    {
        string api = "https://api.vk.com/method/"; // базовая часть адреса API. после неё записываются методы

        public MainForm()
        {
            InitializeComponent();
        }

        private void MethodChanged(object sender, EventArgs e)
        {
            switch (methodComboBox.SelectedItem.ToString()) // просто создано для удобства, что при изменении метода будут дописываться нужные
            {                                                                                                                                                                       // аргументы
                case "users.search":
                    paramsTextBox.Text = "q=Artem&hometown=Chelyabinsk";
                    break;
                case "docs.get":
                    paramsTextBox.Text = "";
                    break;
                case "friends.getOnline":
                    paramsTextBox.Text = "";
                    break;
                case "friends.get":
                    paramsTextBox.Text = "";
                    break;
            }
        }

        private void ExecuteBtnClicked(object sender, EventArgs e) // обработчик события нажатия кнопки "Выполнить"
        {
            string token = null;
            if (string.IsNullOrEmpty(methodComboBox.Text))
            {
                MessageBox.Show("Поле метода не может быть пустым");
                return;
            }

            AuthForm authForm = new AuthForm(methodComboBox.Text.Split('.')[0] != "users" // создание формы авторизации, в к-тор передается то, что нам требуется
                ? methodComboBox.Text.Split('.')[0]
                : "docs");
            authForm.ShowDialog(); // вызывается ShowDialog, который заблокирует MainForm, пока не будет присвоение значение DialogResult
            if (authForm.DialogResult == DialogResult.OK) token = authForm.Token; // присвоение значения токена.
            if (string.IsNullOrEmpty(token)) // если токен равен null, то вызывается соответствующее сообщение
            {
                MessageBox.Show("Ошибка авторизации!");
                return;
            }
            using WebClient wc = new WebClient { Encoding = Encoding.UTF8 }; // создание объекта Веб-Клиента
            string json = // инициализация строки с результатом работы метода. 
                wc.DownloadString(api + $"{methodComboBox.Text}?{paramsTextBox.Text}&access_token={token}&v=5.131");
            logTextBox.Text = json;
        }
    }
}
