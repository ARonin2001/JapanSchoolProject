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
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

namespace aunteficationAdminForm
{
    /// <summary>
    /// Логика взаимодействия для SearchFormClients.xaml
    /// </summary>
    public partial class SearchFormClients : Window
    {
        public string commandSqlText;

        public bool WindowLoad;
        public string nameDb = "meneger";

        public string gender = "AND gender in('w' AND 'm')";

        public int ageOt;
        public string ageForSql = "(YEAR(CURRENT_DATE)-YEAR(dateBirth))-(RIGHT(CURRENT_DATE,5)<RIGHT(dateBirth,5))";

        public string ageStart = "15";
        public string ageEnd = "80";
        public string citySql = "";

        public string[] cityListBox = { "Любой", "Tokyou", "Kyotou", "Minsai", "Saitama", "Nara", "Kiba" };

        public SearchFormClients()
        {
            InitializeComponent();

            comBoxContentAdd(67, ageFor);
            comBoxContentAdd(67, ageUp, "До ");

            cityList.ItemsSource = cityListBox;
            cityList.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            btnVisibility();
        }

        public void btnVisibility()
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

        private void bdList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WindowLoad)
            {
                ComboBox comboBox = (ComboBox)sender;
                ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
                nameDb = selectedItem.Content.ToString();

                if (nameDb != "customer") citySql = "";

                sqlDataBaseSearch();

                if (nameDb == "customer" && cityContainer.Visibility == Visibility.Hidden)
                    cityContainer.Visibility = Visibility;
                else
                    cityContainer.Visibility = Visibility.Hidden;

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowLoad = true;

        }

        // search TextBox Start. Поиск записей и вывод в dataGrid через TextBox
        private void searchInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            sqlDataBaseSearch();
        }
        // search TextBox End

        private void genderMen_Checked(object sender, RoutedEventArgs e)
        {
            switch (((RadioButton)sender).Name)
            {
                case "genderMen":
                    gender = "AND gender LIKE 'm'";
                    sqlDataBaseSearch();
                    break;
                case "genderWomen":
                    gender = "AND gender LIKE 'w'";
                    sqlDataBaseSearch();
                    break;
                case "genderAll":
                    gender = "AND gender in('w' AND 'm')";
                    sqlDataBaseSearch();
                    break;
            }
        }

        // age start. Заполняем возрастной ComboBox возрастом от 15 до 80
        public void comBoxContentAdd(int n, ComboBox cmBox, string s = "От ")
        {
            string[] str = new string[n];

            for (int i = 0; i < str.Length - 1; i++)
            {
                str[i] = s + (15 + i);
            }

            cmBox.ItemsSource = str;
        }
        // age end

        // ageUp Start. Вывод пользователей от n
        private void ageFor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            ageStart = getAgeSearch(sender); // Возраст От
            sqlDataBaseSearch();
        }
        // ageUp End


        // show BD Start Вывод информации из БД
        private void sqlDataBaseSearch()
        {

            commandSqlText = $"SELECT * FROM {nameDb} WHERE ((name LIKE '{searchInput.Text}%')" +
            $" OR (surName LIKE '{searchInput.Text}%')" +
            $" OR (fatherName LIKE '{searchInput.Text}%')) {gender}" +
            $" AND ({ageForSql} >= {ageStart} AND " +
            $"{ageForSql} <= {ageEnd}) {citySql}";

            dataBase db = new dataBase();

            DataTable dataTable = new DataTable();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(commandSqlText, db.getConnection());

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataTable);
            userInfromation.ItemsSource = dataTable.AsDataView();

        }
        // show BD End

        // ageUp Start. Вывод пользователей от n до n лет
        private void ageUp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ageEnd = getAgeSearch(sender); // Возраст До
            sqlDataBaseSearch();
        }

        private void cityList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(WindowLoad)
            {

                int item = ((ComboBox)sender).SelectedIndex;
                string itemName = ((ComboBox)sender).Items[item].ToString();

                if (itemName == "Любой") itemName = "%";

                citySql = $"AND city LIKE '{itemName}'";

                sqlDataBaseSearch();
            }
        }


        // ageSearch Start. Отделение начального слова от числа (возраста)
        private string getAgeSearch(object sender)
        {
            string age = "";

            if (WindowLoad)
            {

                int item = ((ComboBox)sender).SelectedIndex;
                string itemName = ((ComboBox)sender).Items[item].ToString();

                string[] comboBoxItemStr = itemName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                age = comboBoxItemStr[1];

            }
            return age;

        }
        //ageSearch End

        private void beforeBtn_Click(object sender, RoutedEventArgs e)
        {
            AdminFrom adminFrom = new AdminFrom();
            this.Close();
            adminFrom.Show();
        }
    }
}
