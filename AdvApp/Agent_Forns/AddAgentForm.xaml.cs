using System.Windows;

namespace AdvApp
{
    /* Окно c добавлением рекламы */
    public partial class AddAgentForm : Window
    {
        // Список агентов (получение переменных)
        public Agent NewAgent { get; private set; }

        public AddAgentForm()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            NewAgent = new Agent();
        }

        // Добавление данных в Agent
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            int agentId;

            decimal commissionPercentage;
            decimal totalSalesAmount;

            bool isAgentIdValid = int.TryParse(AgentIdTextBox.Text, out agentId);
            bool isCommissionPercentageValid = decimal.TryParse(CommissionPercentageTextBox.Text, out commissionPercentage);
            bool isTotalSalesAmountValid = decimal.TryParse(TotalSalesAmountTextBox.Text, out totalSalesAmount);
            bool isNameValid = !string.IsNullOrWhiteSpace(NameTextBox.Text);

            if (isAgentIdValid && isCommissionPercentageValid && isTotalSalesAmountValid && isNameValid)
            {
                NewAgent = new Agent
                {
                    AgentId = agentId,
                    Name = NameTextBox.Text,
                    CommissionPercentage = commissionPercentage,
                    TotalSalesAmount = totalSalesAmount
                };

                base.DialogResult = true;
            }
            else MessageBox.Show("Пожалуйста, проверьте введенные данные и попробуйте снова.", "Некорректные данные", MessageBoxButton.OK, MessageBoxImage.Hand);
        }
    }
}