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
using System.Windows.Media.Animation;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

namespace aunteficationAdminForm
{
    /// <summary>
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class Autorization : Window
    {


        public Autorization()
        {
            InitializeComponent();
        }

        // remember Label
        private void lab_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RememberForm rememberForm = new RememberForm();
            rememberForm.Show();
        }

        // autorization Method
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if(Login.Text  == "" || Password.Password == "") {
                MessageBox.Show("Введите пароль/логин");
            } else
            {
                String loginUser = Login.Text;
                String passUser = Password.Password;

                dataBase db = new dataBase();

                DataTable dataTable = new DataTable();
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `autoriz_adm` WHERE `log` LIKE @uL AND par LIKE @uP", db.getConnection());
                command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginUser;
                command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = passUser;

                dataAdapter.SelectCommand = command;
                dataAdapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                    MessageBox.Show("Всё получилось!");
                else
                    MessageBox.Show("Введите правильный пароль/логин");
            }
        }

    }
}
