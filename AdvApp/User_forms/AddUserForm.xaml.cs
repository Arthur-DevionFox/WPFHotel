using System.Windows;

namespace AdvApp.User_forms
{
    /* Окно c добавлением пользователей */
    public partial class AddUserForm : Window
    {
        // Список пользователей (получение переменных)
        public User NewUser { get; private set; }

        public AddUserForm()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            NewUser = new User();
        }

        // Добавление данных в User
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var users = XmlDataManager.LoadData<User>("data/Users.xml");

            int userId = users.Max(u => u.UserId) + 1;

            bool isNameValid = !string.IsNullOrWhiteSpace(NameTextBox.Text);
            bool isLoginValid = !string.IsNullOrWhiteSpace(LoginTextBox.Text);
            bool isPasswordValid = !string.IsNullOrWhiteSpace(PasswordBox.Text);

            if (isNameValid && isLoginValid && isPasswordValid)
            {
                NewUser = new User
                {
                    UserId = userId,
                    Name = NameTextBox.Text,
                    Login = LoginTextBox.Text,
                    Password = PasswordBox.Text
                };

                base.DialogResult = true;

                MessageBox.Show("Регистрация прошла успешно! Попробуйте войти в систему.", "Завершение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else MessageBox.Show("Пожалуйста, проверьте введенные данные и попробуйте снова.", "Некорректные данные", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}