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
using System.Data;
using MySql.Data.MySqlClient;

namespace aunteficationAdminForm
{
    /// <summary>
    /// Логика взаимодействия для SchoolList.xaml
    /// </summary>
    public partial class SchoolList : Window
    {
        SearchFormClients formClients = new SearchFormClients();
        dataBase db = new dataBase();

        DataTable dataTable = new DataTable();
        MySqlDataAdapter dataAdapter = new MySqlDataAdapter();

        public string commandSql = "";
        public string[] cityListBox = { "Любой", "Tokyou", "Kyotou", "Minsai", "Saitama", "Nara", "Kiba" };
        public SchoolList()
        {
            InitializeComponent();

            cityList.ItemsSource = cityListBox;
            cityList.SelectedIndex = 0;
            
        }

        private void beforeBtn_Click(object sender, RoutedEventArgs e)
        {
            AdminFrom adminFrom = new AdminFrom();
            this.Close();
            adminFrom.Show();
        }

        private void btnVisibl_Click(object sender, RoutedEventArgs e)
        {
            if (searchContent.Visibility == Visibility)
            {
                dopSearch.MaxWidth = 0;
                searchContent.Visibility = Visibility.Hidden;
            }
            else
            {
                dopSearch.MaxWidth = 200;
                searchContent.Visibility = Visibility;
            }
        }

        private void searchInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            commandSql = $"SELECT * FROM schools WHERE name LIKE '%{searchInput.Text}%'";

            DataTable dataTable = new DataTable();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(commandSql,
                new MySqlConnection("server=localhost;port=3307;username=root;password=root;database=japaneseproject"));

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataTable);
            infoSchools.ItemsSource = dataTable.AsDataView();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            commandSql = "SELECT * FROM schools";
            DataTable dataTable = new DataTable();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(commandSql,
                new MySqlConnection("server=localhost;port=3307;username=root;password=root;database=japaneseproject"));

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataTable);

            infoSchools.ItemsSource = dataTable.AsDataView();
        }
    }
}
