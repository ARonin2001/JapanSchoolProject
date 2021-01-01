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
    /// Логика взаимодействия для addDormitory.xaml
    /// </summary>
    public partial class addDormitory : Window
    {
        string minCont;
        int count = 0;

        int minContCount = 0;
        public string[] constructor;
        public bool resultIMG = false, res = false;

        public string rentText = null,
            depozitText = null;

        public string[] dormitoryArr = new string[5] { "null", "null", "null", "null", "null" };



        public string commandSql;

        dataBase db = new dataBase();
        

        public addDormitory()
        {
            InitializeComponent();

            minContTerm.SelectedIndex = 0;

            ComboBox comboBox = (ComboBox)minContTerm;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;

            minCont = selectedItem.Content.ToString();

        }

        public addDormitory(string name, string adr, string desc, string comus, string rent, string dep, int cont, bool res)
        {
            InitializeComponent();
            constructor = new string[6] { name, adr, desc, comus, rent, dep };
            resultIMG = true;

            checkTextBoxArr();
            minContTerm.SelectedIndex = cont;
        }

        private void btnResult_Click(object sender, RoutedEventArgs e)
        {
            if(count != 5)
            {
                if (!checkTextBox())
                {
                    MessageBox.Show("Заполните все поля!", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    count++;
                    btnFinish.IsEnabled = true;
                }
                else
                {
                    checkTextBoxTwo();
                    addSqlDormitory();
                }
            }
        }

        void addSqlDormitory()
        {
            try
            {
                commandSql = $"INSERT INTO dormitory (name, description, address, minContractTerm, comUslugi, rent, depozit, img)" +
                    $" VALUES('{name.Text}', '{description.Text}', '{adres.Text}', '{minCont}', '{comUs.Text}', '{rentText}', '{depozitText}'";

                if (resultIMG)
                {
                    commandSql += ", (SELECT id FROM containerimg ORDER BY id DESC LIMIT 1))";
                    db.sqlWork(commandSql);

                }
                else
                {
                    commandSql += ", null)";
                    db.sqlWork(commandSql);
                }

                res = true;
                btnFinish.IsEnabled = true;
                forSchoolsIdIMG("dormitory");
                MessageBox.Show("Общежитие добавлено!)");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        void forSchoolsIdIMG(string nameD)
        {
            int c = 0;
            int i = 0;
            DataSet ds = connectSql($"select * from {nameD} order by id desc limit 1");
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataRow row in dt.Rows)
                {
                    var cells = row.ItemArray;
                    foreach (object cell in cells)
                    {
                        if (c < 1)
                            dormitoryArr[i] = cell.ToString();
                        c++;
                    }
                }
            }
            i++;
        }

        private DataSet connectSql(string commandSql)
        {
            MySqlDataAdapter da = new MySqlDataAdapter(commandSql, db.getConnection());
            DataSet ds = new DataSet();
            da.Fill(ds, "dormitory");

            return ds;
        }

        void checkTextBoxArr()
        {
            TextBox[] tb = new TextBox[6] { name, adres, description, comUs, rent, depozit };
            for(int i = 0; i < tb.Length; i++)
            {
                tb[i].Text = constructor[i];
            }
        }

        void checkTextBoxTwo()
        {
            if (rent.Text != "")
                rentText = rent.Text;
            if (depozit.Text != "")
                depozitText = depozit.Text;
        }

        public bool checkTextBox()
        {
            bool result = true; 
            int count = 0;
            TextBox[] tb = new TextBox[4] { name, adres, description, comUs };

            for(int i = 0; i < tb.Length; i++)
            {
                if (tb[i].Text == "")
                    result = false;
            }

            return result;
        }

        private void minContTerm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectComboBox(sender);
        }

        void selectComboBox(object sender)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;

            minCont = selectedItem.Content.ToString();
            minContCount = comboBox.SelectedIndex;
        }

        private void finish(object sender, RoutedEventArgs e)
        {
            try
            {
                if (count < 6)
                {
                    commandSql = $"insert into conteinerdormitores (`id_one`, `id_two`, `id_three`, `id_four`, `id_five`)" +
                            $"  VALUES({dormitoryArr[0]}, {dormitoryArr[1]}, {dormitoryArr[2]}, {dormitoryArr[3]}, {dormitoryArr[4]})";
                    db.sqlWork(commandSql);
                    MessageBox.Show("Общежития успешно добавлены!)");
                    res = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Общежитий может быть только 5!", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(commandSql);
            }
            
            
        }

        private void btnAddPhoto_Click(object sender, RoutedEventArgs e)
        {
            //addIMG img = new addIMG("imgdormitory", name.Text, adres.Text, description.Text, comUs.Text, rent.Text, depozit.Text, minContCount);
            addIMG img = new addIMG("imgdormitory", true);
            img.ShowDialog();
        }

        private void beforeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
