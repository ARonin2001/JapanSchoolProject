using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace aunteficationAdminForm
{
    /// <summary>
    /// Логика взаимодействия для RegistrationOperator.xaml
    /// </summary>
    public partial class RegistrationOperator : Window
    {
        public RegistrationOperator()
        {
            InitializeComponent();
        }


        
        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            

            if (login.Text != "" && login.Text != " ")
            {
                registrationUsers rgUsers = new registrationUsers("autoriz_op", "operator");
                MessageBox.Show(rgUsers.intoDateUser("operator_idoperator", "idoperator", login.Text));
                MessageBox.Show("Всё сработало!)");
            }
        }

        private void login_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (login.Text.Length >= 6)
                registerButton.IsEnabled = true;
            else
                registerButton.IsEnabled = false;

            if (login.Text.Length >= 6) registerButton.IsEnabled = true;

            if (login.Text.Length < 6)
                warning.Visibility = Visibility;
            else
                warning.Visibility = Visibility.Hidden;
        }
    }
}
