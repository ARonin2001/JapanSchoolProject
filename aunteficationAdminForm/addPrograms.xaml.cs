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
    /// Логика взаимодействия для addPrograms.xaml
    /// </summary>
    public partial class addPrograms : Window
    {
        string dlitProgram = "Короткая";
        public bool result, resultP = false;
        public bool correctResult = false;
        string program;

        string commandSql;

        string textBoxName, TextBoxAdres, descriptionT;

        public int exDocument = 0,
            entranceFee = 0;
        public int count = 0;

        dataBase db = new dataBase();

        public addPrograms()
        {
            InitializeComponent();
            checkTwoTextBox();
            textLabel.Text = "ДОБАВИТЬ КОРОТКУЮ ПРОГРАММУ";
            periodText.Text = "3 месяца";
        }

        //public addPrograms()
        //{
        //    InitializeComponent();
        //    checkTwoTextBox();

            

        //    //textBoxName = name;
        //    //TextBoxAdres = adres;
        //    //descriptionT = description;
        //}

        public addPrograms(string programName)
        {
            try
            {
                InitializeComponent();

                correctResult = true;
                program = programName;

                addValueTextBox(programName);

                //textBoxName = name;
                //TextBoxAdres = adres;
                //descriptionT = description;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        void addValueTextBox(string program)
        {
            TextBox[] tb = new TextBox[5] { exams, vstupExams, education, culturProgram, insuranseTr };
            string[] dataSql = new string[5];

            DataSet ds = connectSql($"select * from programs where dlitProgram Like '{program}' order by id desc limit 1");
            if (program == "Длительная")
                textLabel.Text = "КОРРЕКТИРОВАТЬ ДЛИТЕЛЬНУЮ ПРОГРАММУ";
            else
                textLabel.Text = "КОРРЕКТИРОВАТЬ КОРОТКУЮ ПРОГРАММУ";
            forDataSet(ds, dataSql);

            for (int i = 0; i < 5; i++)
            {
                tb[i].Text = dataSql[i];
            }
        }

        void forDataSet(DataSet ds, string[] dataSql)
        {
            int i = 0;
            int count = 0;
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataRow row in dt.Rows)
                {
                    // получаем все ячейки строки
                    var cells = row.ItemArray;
                    foreach (object cell in cells)
                    {
                        if(count >= 2 && count < 7)
                        {
                            dataSql[i] = cell.ToString();
                            i++;
                        }
                        count++;
                    }
                }
            }
        }

        private DataSet connectSql(string commandSql)
        {
            MySqlDataAdapter da = new MySqlDataAdapter(commandSql, db.getConnection());
            DataSet ds = new DataSet();
            da.Fill(ds, "programs");

            return ds;
        }

        private void btnResult_Click(object sender, RoutedEventArgs e)
        {
            if (correctResult)
                resultCorrect();
            else
                resultadd();
        }

        void resultadd()
        {
            if ((MessageBox.Show("Вы действительно хотите добавить запись?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning))
                == MessageBoxResult.Yes)
            {
                commandSql = "insert into programs (`period`, `examinationDocuments`, `entranceFee`, `education`, `culturalProgram`, `InsuranceTrainingMaterials`," +
                    " dlitProgram)" +
                    $" values('{periodText.Text}', {exams.Text}, {vstupExams.Text},  {education.Text}, {culturProgram.Text}, {insuranseTr.Text}, '{dlitProgram}')";
                if (programMain(commandSql))
                {
                    if (dlitProgram == "Длительная")
                    {
                        //addSchools addSc = new addSchools(getResult(), textBoxName, TextBoxAdres, descriptionT);
                        this.Close();
                    }

                    clearTextBox();
                    dlitProgram = "Длительная";
                    textLabel.Text = "ДОБАВИТЬ ДЛИТЕЛЬНУЮ ПРОГРАММУ";
                    periodText.Text = "от 1 года";

                    count = 1;
                }
            }
        }

        void resultCorrect()
        {
            if ((MessageBox.Show("Вы действительно хотите изменить запись?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning))
                   == MessageBoxResult.Yes)
            {
                commandSql = $"UPDATE programs SET examinationDocuments = {exams.Text}, entranceFee = {vstupExams.Text}," +
                    $" education = {education.Text}, culturalProgram = {culturProgram.Text}, InsuranceTrainingMaterials ={insuranseTr.Text}" +
                    $" WHERE dlitProgram LIKE '{program}'" +
                    $" ORDER BY `id` DESC" +
                    $" LIMIT 1";
                if (programMain(commandSql))
                {
                    //addSchools addSc = new addSchools(getResult(), textBoxName, TextBoxAdres, descriptionT);
                    this.Close();
                    //addSc.ShowDialog();
                }
            }
        }

        bool programMain(string commandSql)
        {
            bool result = false;
            if (checkTextBox())
            {
                MessageBox.Show("Заполните все поля", "Warning!");
            }
            else
            {
                addProgram(commandSql);
                result = true;
            }
            return result;
        }

        void addProgram(string commandSql)
        {
            try
            {

                db.sqlWork(commandSql);

                resultP = true;
                MessageBox.Show("Успешно выполнено!)");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void checkTwoTextBox()
        {
            exams.Text = "0";
            vstupExams.Text = "0";
        }
        private bool checkTextBox()
        {
            string[] arrTextBox = new string[3] { insuranseTr.Text, education.Text, culturProgram.Text };

            foreach (string i in arrTextBox)
                if (i == "")
                    result = true;
                else
                    result = false;
            return result;
        }

        void clearTextBox()
        {
            TextBox[] arrTextBox = new TextBox[5] { exams, vstupExams, insuranseTr, education, culturProgram };

            foreach (TextBox i in arrTextBox)
                i.Text = "";
        }

        private void beforeBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((MessageBox.Show("Если вы выйдите, все добавленные данные будут потеряны. Вы действительно хотите выйти?", "Warning!",
                MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
            {
                if(count == 1)
                    db.sqlWork("DELETE FROM programs ORDER BY id DESC LIMIT 1");
                resultP = false;
                //addSchools addSc = new addSchools(textBoxName, TextBoxAdres, descriptionT);
                this.Close();
            }
        }

        public bool getResult()
        {
            return true;
        }
    }
}
