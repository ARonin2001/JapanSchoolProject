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
    /// Логика взаимодействия для RegistrationMeneger.xaml
    /// </summary>
    public partial class RegistrationMeneger : Window
    {
        public RegistrationMeneger()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(login.Text != "" && login.Text != " ")
            {
                registrationUsers rgUsers = new registrationUsers("autoriz_men", "meneger");
                rgUsers.intoDateUser("meneger_idmeneger", "idmeneger", login.Text);

                MessageBox.Show("Всё сработало!)");
            }
        }

        private void login_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (login.Text.Length < 6)
                warning.Visibility = Visibility;
            else
                warning.Visibility = Visibility.Hidden;
        }
    }
}
