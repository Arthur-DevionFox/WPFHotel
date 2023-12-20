using System.Windows;

namespace AdvApp
{
    /* Редактирование данных агентов */
    public partial class EditAgentForm : Window
    {
        // Список агентов (получение переменных)
        public Agent UpdatedAgent { get; private set; }

        public EditAgentForm(Agent agentToEdit)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        
            AgentIdTextBox.Text = agentToEdit.AgentId.ToString();
            NameTextBox.Text = agentToEdit.Name;
            CommissionPercentageTextBox.Text = agentToEdit.CommissionPercentage.ToString();
            TotalSalesAmountTextBox.Text = agentToEdit.TotalSalesAmount.ToString();
        }

        // Сохранение новых данных в Agent
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(CommissionPercentageTextBox.Text, out var commissionPercentage) &&
                decimal.TryParse(TotalSalesAmountTextBox.Text, out var totalSalesAmount) &&
                !string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                UpdatedAgent.Name = NameTextBox.Text;
                UpdatedAgent.CommissionPercentage = commissionPercentage;
                UpdatedAgent.TotalSalesAmount = totalSalesAmount;

                base.DialogResult = true;
            }
            else MessageBox.Show("Пожалуйста, проверьте введенные данные.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Hand);
        }
    }
}