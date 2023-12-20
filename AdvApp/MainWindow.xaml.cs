using AdvApp.User_forms;
using System.Windows;
using System.Windows.Controls;

/* Начальное окно */
namespace AdvApp
{
    public partial class MainWindow : Window
    {
        // Список пользователей
        private List<User> entities;

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            entities = XmlDataManager.LoadData<User>("data/Users.xml");
        }

        // Список пользователей
        private List<User> Users { get; set; }

        // Вход в систему
        private void Auth_Button(object sender, RoutedEventArgs e)
        {
            Users = XmlDataManager.LoadData<User>("data/Users.xml");
            User user = Users.FirstOrDefault(u => u.Login == loginBox.Text && u.Password == passwordBox.Text);

            if (user != null)
            {
                MainMenu menu = new MainMenu(user.Name, this);
                                
                menu.Show();
                Hide();
            }
        }

        // Вход в систему
        private void Reg_Button(object sender, RoutedEventArgs e)
        {
            AddUserForm addUserForm = new AddUserForm();

            // Вывод окна регистрации, не закрывая регистрацию, и проверка на закрытие окна регистрации
            if (addUserForm.ShowDialog() == true)
            {
                User newUser = addUserForm.NewUser;

                if (newUser != null)
                {
                    entities.Add(newUser);
                    XmlDataManager.SaveData("data/Users.xml", entities);
                }
            }
        }

        private void loginBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}