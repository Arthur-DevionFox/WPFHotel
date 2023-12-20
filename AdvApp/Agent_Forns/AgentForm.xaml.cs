using System.Windows;
using System.Windows.Controls;

namespace AdvApp
{
    /* Окно со списком агентов */
    public partial class AgentForm : Window
    {
        // Список агентов
        private List<Agent> entities;

        public AgentForm()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            entities = XmlDataManager.LoadData<Agent>("data/Agents.xml");
            dataGrid.ItemsSource = entities;
        }

        // Открыть окно с добавлением и отправить данные в таблицу
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddAgentForm addAgentForm = new AddAgentForm();

            if (addAgentForm.ShowDialog() == true)
            {
                Agent newAgent = addAgentForm.NewAgent;

                if (newAgent != null)
                {
                    entities.Add(newAgent);
                    XmlDataManager.SaveData("data/Agents.xml", entities);
                    dataGrid.Items.Refresh();
                }
            }
        }

        // Удалить выделенную строчку с данными
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem is Agent selectedAd)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Вы уверены, что хотите удалить выбранное объявление?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    entities.Remove(selectedAd);
                    XmlDataManager.SaveData("data/Agents.xml", entities);
                    dataGrid.Items.Refresh();
                }
            }
            else MessageBox.Show("Пожалуйста, выберите объявление для удаления.", "Объявление не выбрано", MessageBoxButton.OK, MessageBoxImage.Hand);
        }

        // Открыть окно с редактированием и отправить новые данные в таблицу
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem is Agent selected)
            {
                EditAgentForm editForm = new EditAgentForm(selected);

                if (editForm.ShowDialog() == true)
                {
                    int index = entities.IndexOf(selected);
                    entities[index] = editForm.UpdatedAgent;
                                        
                    XmlDataManager.SaveData("data/Agents.xml", entities);
                    dataGrid.Items.Refresh();
                }
            }
        }

        // Поиск в таблице по введенному значению
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();

            List<Agent> filteredList = entities.Where(agent =>
            agent.Name.ToLower().Contains(searchText) ||
            agent.AgentId.ToString().Contains(searchText) ||
            agent.CommissionPercentage.ToString().Contains(searchText) ||
            agent.TotalSalesAmount.ToString().Contains(searchText)).ToList();

            dataGrid.ItemsSource = filteredList;
        }
    }
}