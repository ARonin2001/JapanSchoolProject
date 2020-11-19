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
    /// Логика взаимодействия для messageBoxCorrectAdd.xaml
    /// </summary>
    public partial class messageBoxCorrectAdd : Window
    {
        dataBase db = new dataBase();

        public string[] nameTextBoxs;
        public string id;
        public string commandSql;
        public static bool ready = false;

        public messageBoxCorrectAdd(string[] nameTBox)
        {
            InitializeComponent();

            nameTextBoxs = nameTBox;
        }

        public messageBoxCorrectAdd(string[] nameTBox, string ID)
        {
            InitializeComponent();

            nameTextBoxs = nameTBox;
            id = ID;
        }
        public messageBoxCorrectAdd()
        {
            InitializeComponent();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Добавить запись?", "Добавление записи", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                commandSql = $"INSERT INTO typeservice(type, price, description) " +
                        $"VALUES('{nameTextBoxs[0]}', {nameTextBoxs[1]}, '{nameTextBoxs[2]}')";
                shellCreation(commandSql);
            }

        }

        private void correct_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Изменить запись?", "Изменение записи", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                commandSql = $"UPDATE typeservice SET type = '{nameTextBoxs[0]}', price = {nameTextBoxs[1]}, description = '{nameTextBoxs[2]}' " +
                        $"WHERE id = {id}";
                shellCreation(commandSql);
            }
        }

        public static bool resultReturn()
        {
            return ready;
        }

        public void shellCreation(string commandSql)
        {
            try
            {
                string result = dataBase.textBoxSearchNullInput(nameTextBoxs);

                if (result == "Всё сработало:)")
                {
                    db.sqlWork(commandSql);
                }

                this.Close();
                MessageBox.Show(result);
            }
            catch
            {
                this.Close();
                MessageBox.Show("Неизвестная ошибка", "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
