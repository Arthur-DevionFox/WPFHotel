using System.Windows;
using System.Windows.Controls;

namespace AdvApp.Customer_forms
{
    /* Окно со списком заказчиков */
    public partial class CustomerForm : Window
    {
        // Список заказчиков
        private List<Customer> entities;

        public CustomerForm()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            entities = XmlDataManager.LoadData<Customer>("data/Customers.xml");
            dataGrid.ItemsSource = entities;
        }

        // Открытие окна с добавлением и отправить данные в таблицу
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddCustomerForm addAgentForm = new AddCustomerForm();

            if (addAgentForm.ShowDialog() == true)
            {
                Customer newAgent = addAgentForm.NewCustomer;

                if (newAgent != null)
                {
                    entities.Add(newAgent);
                    XmlDataManager.SaveData("data/Customers.xml", entities);
                    dataGrid.Items.Refresh();
                }
            }
        }

        // Удаление выделенной строчки с данными
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem is Customer selectedAd)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Вы уверены, что хотите удалить выбранное объявление?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    entities.Remove(selectedAd);
                    dataGrid.ItemsSource = null;
                    XmlDataManager.SaveData("data/Customers.xml", entities);
                    dataGrid.ItemsSource = entities;
                }
            }
            else MessageBox.Show("Пожалуйста, выберите объявление для удаления.", "Объявление не выбрано", MessageBoxButton.OK, MessageBoxImage.Hand);
        }

        // Открытие окна с редактированием и отправить новые данные в таблицу
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem is Customer selected)
            {
                EditCustomerForm editForm = new EditCustomerForm(selected);

                if (editForm.ShowDialog() == true)
                {
                    int index = entities.IndexOf(selected);

                    entities[index] = editForm.UpdatedCustomer;
                    dataGrid.Items.Refresh();
                    XmlDataManager.SaveData("data/Customers.xml", entities);
                }
            }
        }

        // Поиск в таблице по введенному значению
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();

            List<Customer> filteredList = entities.Where((Customer customer) =>
            customer.CustomerId.ToString().Contains(searchText) ||
            customer.Name.ToLower().Contains(searchText) ||
            (customer.BankingDetails != null && customer.BankingDetails.ToLower().Contains(searchText)) ||
            (customer.Phone != null && customer.Phone.Contains(searchText)) ||
            (customer.ContactPerson != null && customer.ContactPerson.ToLower().Contains(searchText))).ToList();

            dataGrid.ItemsSource = filteredList;
        }
    }
}