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
    /// Логика взаимодействия для addSchools.xaml
    /// </summary>
    public partial class addSchools : Window
    {


        public bool result = false;
        public bool resultD = false, resultP = false, resultIMG = false;
        public string idPhoto;

        public string commandSql;

        dataBase db = new dataBase();
        public addSchools()
        {
            InitializeComponent();

        }

        public addSchools(string tbName, string tbAdres, string tbDescription)
        {
            InitializeComponent();
            name.Text = tbName;
            adres.Text = tbAdres;
            description.Text = tbDescription;

        }

        public addSchools(bool res, string tbName, string tbAdres, string tbDescription)
        {
            InitializeComponent();
            result = res;

            name.Text = tbName;
            adres.Text = tbAdres;
            description.Text = tbDescription;

            btnChenge();
        }


        void btnChenge()
        {
            btnCorredtShortprogram.IsEnabled = true;
            btnCorrectLongprogram.IsEnabled = true;

            btnCorrectLongprogram.Cursor = Cursors.Hand;
            btnCorredtShortprogram.Cursor = Cursors.Hand;
        }
        private void btnAddPhoto_Click(object sender, RoutedEventArgs e)
        {
            addIMG imgForm = new addIMG("imgschools");
            imgForm.ShowDialog();
            resultIMG = imgForm.x;
            idPhoto = imgForm.idPhoto;
        }

        private void btnAdPrograms_Click(object sender, RoutedEventArgs e)
        {
            
            addPrograms addP = new addPrograms();
            addP.ShowDialog();
            resultP = addP.resultP;
            if (resultP) btnChenge();
        }

        private void btnCorredtShortprogram_Click(object sender, RoutedEventArgs e)
        {
            addPrograms addP = new addPrograms("Короткая");
            addP.Show();
            
        }

        private void beforeBtn_Click(object sender, RoutedEventArgs e)
        {
            if((MessageBox.Show("Если вы выйдите, все добавленные данные будут потеряны. Вы действительно хотите выйти?", "Warning!", 
                MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
            {
                this.Close();
            }
                
        }

        private void btnCorrectLongprogram_Click(object sender, RoutedEventArgs e)
        {
            addPrograms addP = new addPrograms("Длительная");
            addP.Show();
        }

        private void btnAddDormitory_Click(object sender, RoutedEventArgs e)
        {
            addDormitory addD = new addDormitory();
            addD.ShowDialog();
            resultD = addD.res;

        }

        private void btnResult_Click(object sender, RoutedEventArgs e)
        {
            if(checkTextBox())
            {
                MessageBox.Show("Заполните все поля с пометкой *");
            } else
            {
                if (checkPrograms())
                {
                    sqlSchool();
                    addSchool();
                }
                else
                {
                    MessageBox.Show("Добавьте программы!");
                }
            }
        }

        bool checkTextBox()
        {
            TextBox[] tb = new TextBox[3] { name, adres, description };
            bool result = false;
            foreach(TextBox t in tb)
            {
                if (t.Text == "") result = true; 
            }

            return result;
        }

        bool checkPrograms()
        {
            bool result = false;

            if (btnCorrectLongprogram.IsEnabled == true) result = true;

            return result;
        }
        
        void sqlSchool()
        {
            commandSql = "INSERT INTO schools (name, city, description, img, id_containerDormitores, id_shortprogram, id_longprogram)" +
            $" VALUES('{name.Text}', '{adres.Text}', '{description.Text}', null, null, (SELECT id FROM programs WHERE dlitProgram LIKE 'Короткая'" +
            $" ORDER BY id DESC LIMIT 1), (SELECT id FROM programs WHERE dlitProgram LIKE 'Длительная' ORDER BY id DESC LIMIT 1))";

            if (resultIMG)
            {
                commandSql = "INSERT INTO schools (name, city, description, img, id_containerDormitores, id_shortprogram, id_longprogram)" +
                $" VALUES('{name.Text}', '{adres.Text}', '{description.Text}', {idPhoto}, null, (SELECT id FROM programs WHERE dlitProgram LIKE 'Короткая'" +
                $" ORDER BY id DESC LIMIT 1), (SELECT id FROM programs WHERE dlitProgram LIKE 'Длительная' ORDER BY id DESC LIMIT 1))";
            }

            if(resultD)
            {
                commandSql = "INSERT INTO schools (name, city, description, img, id_containerDormitores, id_shortprogram, id_longprogram)" +
                $" VALUES('{name.Text}', '{adres.Text}', '{description.Text}', null, (SELECT id FROM conteinerdormitores ORDER BY id DESC LIMIT 1)," +
                $" (SELECT id FROM programs WHERE dlitProgram LIKE 'Короткая'" +
                $" ORDER BY id DESC LIMIT 1), (SELECT id FROM programs WHERE dlitProgram LIKE 'Длительная' ORDER BY id DESC LIMIT 1))";
            }

            if(resultIMG && resultD)
            {
                commandSql = "INSERT INTO schools (name, city, description, img, id_containerDormitores, id_shortprogram, id_longprogram)" +
                $" VALUES('{name.Text}', '{adres.Text}', '{description.Text}', {idPhoto}, (SELECT id FROM conteinerdormitores ORDER BY id DESC LIMIT 1)," +
                $" (SELECT id FROM programs WHERE dlitProgram LIKE 'Короткая'" +
                $" ORDER BY id DESC LIMIT 1), (SELECT id FROM programs WHERE dlitProgram LIKE 'Длительная' ORDER BY id DESC LIMIT 1))";
            }
        }

        void addSchool()
        {
            try
            {
                db.sqlWork(commandSql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(commandSql);
            }
        }

    }
}
