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
using System.Data.SqlClient;

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


        public string sqlSchoolprogramPeriod = "SELECT `schools`.`id`, `name` AS 'Наименование', `city` AS 'Город', " +
            "`description` AS 'Описание', " +
            "`img` AS 'Фото', `id_containerDormitores` AS 'Общежития', (SELECT `period` FROM `programs` WHERE `dlitProgram` LIKE 'Длительная' LIMIT 1)" +
            " AS 'Длительный период обучения'," +
            " (SELECT `period` FROM `programs` WHERE `dlitProgram` LIKE 'Короткая' LIMIT 1) AS 'Короткий период обучения'" +
            " FROM schools, programs" +
            " WHERE `schools`.`id_shortprogram` = `programs`.`id`";

        public string commandSql = "",
            idschool = "";
        
        public SchoolList()
        {
            InitializeComponent();

            schoolBoxAdd();
            addItemsDormitoriesCombobox();
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

            commandSql = "SELECT schools.id, name AS 'Наименование', city AS 'Город', " +
            "description AS 'Описание', " +
            "img AS 'Путь к фото', id_containerDormitores AS 'Общежития', (SELECT `period` FROM `programs` WHERE `dlitProgram` LIKE 'Длительная' LIMIT 1) " +
            " AS 'Длительный период обучения'," +
            " (SELECT `period` FROM `programs` WHERE `dlitProgram` LIKE 'Короткая' LIMIT 1) AS 'Короткий период обучения'" +
            " FROM schools, programs" +
            $" WHERE `schools`.`id_shortprogram` = `programs`.`id` AND schools.name LIKE '%{searchInput.Text}%'";

            //commandSql = $"SELECT * FROM schools WHERE name LIKE '%{searchInput.Text}%'";

            infoSchools.ItemsSource = dataBase.getDataTable(commandSql).AsDataView();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //commandSql = "SELECT * FROM schools";
            commandSql = sqlSchoolprogramPeriod;
            DataTable dataTable = new DataTable();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(commandSql,
                new MySqlConnection("server=localhost;port=3307;username=root;password=root;database=japaneseproject"));

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataTable);

            infoSchools.ItemsSource = dataTable.AsDataView();
        }

        private void schoolBoxAdd() // заполняем combobox из бд
        {
            CmBox.comboboxAddItems("name", "schools", $"SELECT name FROM schools", schoolList);
        }

        private void infoSchools_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                addItemsCombobox();

                DataRowView view = (DataRowView)infoSchools.SelectedItem;
                idschool = view[0].ToString();
            }
            catch 
            {
                //dormitories.Items = "";
            }
            
        }

        private void addItemsCombobox()
        {
            try
            {
                DataRowView view = (DataRowView)infoSchools.SelectedItem;
                if (view[5].ToString() != "")
                {
                    string id = view[5].ToString();

                    commandSql = "SELECT `dormitory`.`name` FROM `dormitory`, `conteinerdormitores`, `schools`" +
                        " WHERE `schools`.`id_containerDormitores` = `conteinerdormitores`.`id` AND `dormitory`.`id`" +
                        " IN(`conteinerdormitores`.`id_one`, `conteinerdormitores`.`id_two`, `conteinerdormitores`.`id_three`," +
                        $" `conteinerdormitores`.`id_four`, `conteinerdormitores`.`id_five`) AND `schools`.`id_containerDormitores` = {id} " +
                        " GROUP BY `dormitory`.`name`";

                    //commandSql = "SELECT `dormitory`.`name` FROM `dormitory`, `conteinerdormitores`, `schools`" +
                    //" WHERE `dormitory`.`id` IN(`conteinerdormitores`.`id_one`, `conteinerdormitores`.`id_two`, `conteinerdormitores`.`id_three`," +
                    //$" `conteinerdormitores`.`id_four`, `conteinerdormitores`.`id_five`) AND `schools`.`id_containerDormitores` = {id}" +
                    //$" GROUP BY `dormitory`.`name`";

                    CmBox.comboboxAddItems("name", "dormitory", commandSql, dormitories);
                }
                else
                {
                    dormitories.Items.Clear();
                }
            }
            catch
            {
            }
        }

        private void searchDormytories(object sender)
        {
            
            int item = ((ComboBox)sender).SelectedIndex;
            DataRowView boxItem = (DataRowView)(((ComboBox)sender).Items[item]);
            string itemName = boxItem[0].ToString();

            commandSql = "SELECT `schools`.`id`, schools.`name` AS 'Наименование', schools.`city` AS 'Город', " +
            "schools.`description` AS 'Описание', " +
            "schools.`img` AS 'Фото', `id_containerDormitores` AS 'Общежития', (SELECT `period` FROM `programs` WHERE `dlitProgram` LIKE 'Длительная' LIMIT 1)" +
            " AS 'Короткий период обучения'," +
            " (SELECT `period` FROM `programs` WHERE `dlitProgram` LIKE 'Короткая' LIMIT 1) AS 'Длинный период обучения'" +
            " FROM `schools`, `programs`, dormitory, conteinerdormitores" +
            $" WHERE`schools`.`id_shortprogram` = `programs`.`id` AND schools.id_containerDormitores = conteinerdormitores.id AND" +
            $"`dormitory`.`id` IN(`conteinerdormitores`.`id_one`, `conteinerdormitores`.`id_two`, `conteinerdormitores`.`id_three`," +
            $" `conteinerdormitores`.`id_four`, `conteinerdormitores`.`id_five`)" +
            $" OR `dormitory`.`name` LIKE '{itemName}'" +
            $" GROUP BY schools.id, schools.name, schools.city, schools.description, id_containerDormitores, programs.period";

            infoSchools.ItemsSource = dataBase.getDataTable(commandSql).AsDataView();
        }

        private void addItemsDormitoriesCombobox()
        {
            CmBox.comboboxAddItems("name", "dormitory", $"SELECT name FROM dormitory", dormitorySearch);
        }

        private void dormitorySearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            searchDormytories(sender);
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (infoSchools.SelectedItems.Count == 1)
                {

                    MessageBoxResult dialogResult = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление",
                        MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

                    if (dialogResult == MessageBoxResult.Yes)
                    {

                        DataRowView view = (DataRowView)infoSchools.SelectedItem;
                        var rowColl = view[0];
                        var containerD = view[5];

                        commandSql = $"DELETE FROM schools WHERE id = {rowColl}; DELETE FROM conteinerdormitores INNER JOIN schools ON conteinerdormitores.id =" +
                            $" schools.id_containerDormitores" +
                            $" WHERE schools.id_containerDormitores = {containerD} ";

                        MySqlCommand command = new MySqlCommand(commandSql, db.getConnection());
                        dataAdapter.SelectCommand = command;
                        dataAdapter.Fill(dataTable);
                        infoSchools.ItemsSource = null;
                        infoSchools.ItemsSource = dataTable.AsDataView();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chengeBtn_Click(object sender, RoutedEventArgs e)
        {
            messageBoxForSchools msg = new messageBoxForSchools(idschool);
            msg.ShowDialog();
        }

        private void schoolList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //searchDormytories(sender);

            searchSchoolCombobox(sender);
        }

        private void CorrectSchools()
        {
        }

        private void searchSchoolCombobox(object sender)
        {
            int item = ((ComboBox)sender).SelectedIndex;
            DataRowView boxItem = (DataRowView)(((ComboBox)sender).Items[item]);
            string itemName = boxItem[0].ToString();

            commandSql = "SELECT `schools`.`id`, `name` AS 'Наименование', `city` AS 'Город', " +
            "`description` AS 'Описание', " +
            "`img` AS 'Путь к фото', `id_containerDormitores` AS 'Общежития'," +
            " (SELECT `period` FROM `programs` WHERE `dlitProgram` LIKE 'Длительная' LIMIT 1) AS 'Длительный период обучения'," +
            " (SELECT `period` FROM `programs` WHERE `dlitProgram` LIKE 'Короткая' LIMIT 1) AS 'Короткий период обучения'" +
            " FROM `schools`, programs" +
            $"  WHERE `schools`.`id_shortprogram` = `programs`.`id` AND schools.name LIKE '{itemName}'";

            infoSchools.ItemsSource = dataBase.getDataTable(commandSql).AsDataView();
        }
    }
}
