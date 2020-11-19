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
    /// Логика взаимодействия для AdminFrom.xaml
    /// </summary>
    public partial class AdminFrom : Window
    {

        //Dormitory dormitoryList = new Dormitory();
        Previes previes = new Previes();
        RegistrationUser regUser = new RegistrationUser();
        SchoolList schoolList = new SchoolList();
        SchoolProgram schoolProgram = new SchoolProgram();
        SearchFormClients searchClient = new SearchFormClients();
        public AdminFrom()
        {
            InitializeComponent();

        }

        private void searchUsers_Click(object sender, RoutedEventArgs e)
        {
            switch(((Button)sender).Name)
            {
                case "searchUsers":
                    this.Close();
                    searchClient.Show();
                    break;
                case "schools":
                    this.Close();
                    schoolList.Show();
                    break;
                case "servisec":
                    this.Close();
                    previes.Show();
                    break;
                //case "dormotory":
                //    this.Close();
                //    dormitoryList.Show();
                //    break;
                case "programSchool":
                    this.Close();
                    schoolProgram.Show();
                    break;
                case "registerUsers":
                    this.Close();
                    regUser.Show();
                    break;
            }
        }
    }
}
