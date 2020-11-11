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
        private string nameDb = "";
        public string gender = "";
        public string sqlCom = "";

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
                bool textBoxInputBool = false; // для проверки на пустоту полей

                string[] textBoxText = { name.Text, surName.Text, gender, email.Text, phone.Text, dateBirth.Text }; // заносим данные с полей
                string fatherNameSql = fatherName.Text;
                
                // перебираем данные с поелй дл проверки на пустоту строк
                for(int i = 0; i < textBoxText.Length; i++)
                {
                    if (textBoxText[i] == "") textBoxInputBool = true;
                }

                // вывод, если какая-то строка пустая
                if(textBoxInputBool)
                {
                    MessageBox.Show("Заполните все данные. *Отчество  не обязательно", "", MessageBoxButton.OK);
                // если нет пустых строк и выбран пол
                } else if(textBoxInputBool == false && gender != "")
                {
                    try
                    {
                        // переводим дату рождения в американский формат
                        string oneDate = "";
                        string twoDate = "";

                        string[] strDate = dateBirth.Text.Split(new char[] { '.' });

                        oneDate = strDate[2];
                        twoDate = strDate[0];

                        string dateBirthSql = oneDate + "." + strDate[1] + "." + twoDate;

                        if (fatherNameSql == "") fatherNameSql = " ";

                        // проверка на выбранную должность и создание SQL команды
                        if (nameDb == "meneger")
                            sqlCom = $"INSERT INTO {nameDb} (name, surName, fatherName, mail, phone, datePost, status, polojenie, gender, dateBirth, salary)" +
                            $" VALUES('{name.Text}', '{surName.Text}', '{fatherNameSql}', '{email.Text}', '{phone.Text}', CURDATE(), 'Работает', 'активен', '{gender}', '{dateBirthSql}', 0)";
                        else if (nameDb == "operator")
                            sqlCom = $"INSERT INTO {nameDb} (name, surName, fatherName, mail, phone, datePost, status, polojenie, gender, dateBirth, salary)" +
                            $" VALUES('{name.Text}', '{surName.Text}', '{fatherNameSql}', '{email.Text}', '{phone.Text}', CURDATE(), 'Работает', 'активен', '{gender}', '{dateBirthSql}', 0)";

                        // работа с бд

                        //String mailUser = email.Text;

                        MessageBox.Show(sqlCom);

                        dataBase db = new dataBase();

                        DataTable dataTable = new DataTable();
                        MySqlDataAdapter adapter = new MySqlDataAdapter();
                        MySqlCommand command = new MySqlCommand(sqlCom, db.getConnection());

                        adapter.SelectCommand = command;
                        adapter.Fill(dataTable);

                        if(nameDb == "meneger")
                        {
                            RegistrationMeneger registrationForm = new RegistrationMeneger();
                            this.Close();
                            registrationForm.Show();
                        } else if(nameDb == "operator")
                        {
                            RegistrationOperator registrationForm = new RegistrationOperator();
                            this.Close();
                            registrationForm.Show();
                        }
                    } catch
                    {
                        MessageBox.Show("Выберите регистрируемую должность", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    
                }

            }
            catch
            {
                MessageBox.Show("Что-то пошло не так, попробуйте снова / Error. Try again please",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

            }
        }

        // занесение в переменную имя выбранной должности для регистрациии
        private void userList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;

            if (selectedItem.Name == "meneger")
                nameDb = "meneger";
            else if (selectedItem.Name == "operator")
                nameDb = "operator";
        }

        // занесение пола в переменную
        private void m_Checked(object sender, RoutedEventArgs e)
        {
            gender = ((RadioButton)sender).Name;
        }

        private void Before_Click(object sender, RoutedEventArgs e)
        {
            AdminFrom adminFrom = new AdminFrom();
            this.Close();
            adminFrom.Show();

        }
    }
}
