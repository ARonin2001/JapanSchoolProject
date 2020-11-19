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
    /// Логика взаимодействия для Previes.xaml
    /// </summary>
    public partial class Previes : Window
    {
        dataBase db = new dataBase();

        DataTable dataTable = new DataTable();
        MySqlDataAdapter dataAdapter = new MySqlDataAdapter();

        public string commandSqlText;
        public string rowCol = null;
        public bool gridChange = false;

        public Previes()
        {
            InitializeComponent();

            watchDataBaseService();
        }
        private void beforeBtn_Click(object sender, RoutedEventArgs e)
        {
            AdminFrom adminFrom = new AdminFrom();
            this.Close();
            adminFrom.Show();
        }

        public void watchDataBaseService()
        {
            commandSqlText = "SELECT * FROM typeservice";

            MySqlCommand command = new MySqlCommand(commandSqlText, db.getConnection());

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataTable);
            typesGrid.ItemsSource = dataTable.AsDataView();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(typesGrid.SelectedItems.Count == 1)
                { 

                    MessageBoxResult dialogResult = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление",
                        MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

                    if(dialogResult == MessageBoxResult.Yes)
                    {

                        DataRowView view = (DataRowView)typesGrid.SelectedItem;
                        var rowColl = view[0];

                        commandSqlText = $"DELETE FROM typeservice WHERE id = {rowColl}";

                        MySqlCommand command = new MySqlCommand(commandSqlText, db.getConnection());
                        dataAdapter.SelectCommand = command;
                        dataAdapter.Fill(dataTable);
                        typesGrid.ItemsSource = null;
                        typesGrid.ItemsSource = dataTable.AsDataView();

                    }
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chengeBtn_Click(object sender, RoutedEventArgs e)
        {
            string[] textBoxData = { type.Text, price.Text, description.Text };

            messageBoxCorrectAdd msBox = new messageBoxCorrectAdd(textBoxData, rowCol);
            msBox.ShowDialog();

            //messageBoxCorrectAdd msBox = new messageBoxCorrectAdd(textBoxData);

            DataTable dataTable = new DataTable();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM typeservice", db.getConnection());
            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataTable);
            //typesGrid.ItemsSource = dataTable.AsDataView();

            typesGrid.ItemsSource = null;
            typesGrid.ItemsSource = dataTable.AsDataView();
        }

        private void typesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView view = (DataRowView)typesGrid.SelectedItem;
                rowCol = view[0].ToString();
                type.Text = view[1].ToString();
                price.Text = view[2].ToString();
                description.Text =view[3].ToString();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
