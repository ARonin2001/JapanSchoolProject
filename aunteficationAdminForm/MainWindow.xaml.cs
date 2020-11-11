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
        AdminFrom adminForm = new AdminFrom();

        private string nameBd = "autoriz_adm";

        public bool b = false;

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
        private void autorizationMethod()
        {
            try
            {
                if (Login.Text == "" || Password.Password == "")
                {
                    MessageBox.Show("Введите пароль/логин");
                }
                else
                {
                    String loginUser = Login.Text;
                    String passUser = Password.Password;

                    dataBase db = new dataBase();

                    DataTable dataTable = new DataTable();
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
                    MySqlCommand command = new MySqlCommand($"SELECT * FROM {nameBd} WHERE `log` LIKE @uL AND par LIKE MD5(@uP)", db.getConnection());
                    command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginUser;
                    command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = passUser;

                    dataAdapter.SelectCommand = command;
                    dataAdapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        this.Close();
                        adminForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Неправильный пароль/логин");
                    }
                }

            }
            catch
            {
                MessageBox.Show("Ошибка соединения. Попробуйте ещё раз/Error connection. try again please",
                    "Error connection",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            autorizationMethod();
            
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) autorizationMethod();
        }

        private void adminBd_Click(object sender, RoutedEventArgs e)
        {
            btnBdForm(sender);
        }

        public void btnBdForm(object sender)
        {
            if (((Button)sender).Name == "adminBd")
            {
                adminBd.Background = new SolidColorBrush(Color.FromRgb(255, 103, 107));
                adminBd.Foreground = new SolidColorBrush(Colors.White);

                menegerBd.Background = new SolidColorBrush(Colors.Transparent);
                menegerBd.Foreground = new SolidColorBrush(Colors.Black);

                nameBd = "autoriz_adm";

                Title.Text = "АДМИН";
            }
            else
            {
                menegerBd.Background = new SolidColorBrush(Color.FromRgb(255, 103, 107));
                menegerBd.Foreground = new SolidColorBrush(Colors.White);

                adminBd.Background = new SolidColorBrush(Colors.Transparent);
                adminBd.Foreground = new SolidColorBrush(Colors.Black);

                nameBd = "autoriz_men";

                Title.Text = "МЕНЕДЖЕР";
            }


            if(b == true)
                b = false;
            else
                b = true;
        }

        private void adminBd_MouseEnter(object sender, MouseEventArgs e)
        {
            if(b == false)
            {
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(222, 121, 121));
                ((Button)sender).Foreground = new SolidColorBrush(Colors.White);
            }
            
        }

        private void menegerBd_MouseLeave(object sender, MouseEventArgs e)
        {
            // работает так себе
            if (b == false)
            {
                ((Button)sender).Background = new SolidColorBrush(Colors.White);
                ((Button)sender).Foreground = new SolidColorBrush(Colors.Black);
            } 


        }
    }
}
