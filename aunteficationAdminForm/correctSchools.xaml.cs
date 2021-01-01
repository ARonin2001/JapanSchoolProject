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
    /// Логика взаимодействия для correctSchools.xaml
    /// </summary>
    public partial class correctSchools : Window
    {
        public string id = "";
        private string commandSql = "";

        dataBase db = new dataBase();
        public correctSchools(string idSchools)
        {
            id = idSchools;
        }

        public correctSchools()
        {
            InitializeComponent();

            inserTextbox();
        }

        private void inserTextbox()
        {
            commandSql = $"SELECT * FROM dormitory";

            MySqlDataAdapter da = new MySqlDataAdapter(commandSql, db.getConnection());
            DataSet ds = new DataSet();
            da.Fill(ds, "dormitory");
            name.Text = ds.Tables[0].Columns["name"].ToString();
        }

    }
}
