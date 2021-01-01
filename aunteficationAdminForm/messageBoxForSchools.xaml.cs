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
    /// Логика взаимодействия для messageBoxForSchools.xaml
    /// </summary>
    public partial class messageBoxForSchools : Window
    {
        public static string idSchool = "";

        addSchools addSchool = new addSchools();
        correctSchools corSchools = new correctSchools();
        public messageBoxForSchools(string id)
        {
            InitializeComponent();

            idSchool = id;
            correctSchools corSchools = new correctSchools(idSchool);
        }


        private void add_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            addSchool.Show();
        }

        private void correct_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            corSchools.Show();
        }
    }
}
