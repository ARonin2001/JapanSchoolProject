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
    /// Логика взаимодействия для RegistrationUser.xaml
    /// </summary>
    public partial class RegistrationUser : Window
    {
        internal string nameDb = "";
        public string gender = "";


        public RegistrationUser()
        {
            InitializeComponent();
        }

        private void email_TextChanged(object sender, TextChangedEventArgs e)
        {
            //string mailBox = email.Text;
            //string a = "";

            //if (mailBox != "") a = mailBox[mailBox.Length - 1].ToString();

            //if (a != "@")
            //{
            //    emaiLabel.Content = "Введите почту";
                
            //    MessageBox.Show(email.Text);
            //}

            
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string[] textBoxText = { name.Text, surName.Text, fatherName.Text, gender, email.Text, phone.Text, dateBirth.Text };

                foreach(string textBT in textBoxText)
                {
                    //if(textBoxText[textBT] == "")
                    MessageBox.Show(textBT);
                }

                //if (email.Text != "")ff
                //{
                //    String mailUser = email.Text;

                //    dataBase db = new dataBase();

                //    DataTable dataTable = new DataTable();
                //    MySqlDataAdapter adapter = new MySqlDataAdapter();
                //    MySqlCommand command = new MySqlCommand("", db.getConnection());

                //    adapter.SelectCommand = command;
                //    adapter.Fill(dataTable);

                //}
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так, попробуйте снова / Error. Try again please",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void userList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;

            if (selectedItem.Name == "memeger")
                nameDb = "meneger";
            else if (selectedItem.Name == "memeger")
                nameDb = "operator";
        }

        private void m_Checked(object sender, RoutedEventArgs e)
        {
            gender = ((RadioButton)sender).Name;
        }
    }
}
