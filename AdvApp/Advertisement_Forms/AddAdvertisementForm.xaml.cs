using System.Windows;

namespace AdvApp
{
    /* Окно c добавлением рекламы */
    public partial class AddAdvertisementForm : Window
    {
        // Список реклам (получение переменных)
        public Advertisement NewAdvertisement { get; private set; }

        public AddAdvertisementForm()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        // Добавление данных в Advertisement
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(AdIdTextBox.Text, out var adId) &&
                int.TryParse(ShowIdTextBox.Text, out var showId) &&
                int.TryParse(CustomerIdTextBox.Text, out var customerId) &&
                DateDatePicker.SelectedDate.HasValue &&
                int.TryParse(DurationTextBox.Text, out var duration))
            {
                NewAdvertisement = new Advertisement
                {
                    AdId = adId,
                    ShowId = showId,
                    CustomerId = customerId,
                    Date = DateDatePicker.SelectedDate.Value,
                    DurationInMinutes = duration
                };

                base.DialogResult = true;
            }
            else MessageBox.Show("Пожалуйста, проверьте введенные данные.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Hand);
        }
    }
}