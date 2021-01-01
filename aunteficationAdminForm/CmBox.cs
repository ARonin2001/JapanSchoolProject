using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aunteficationAdminForm
{
    class CmBox
    {
        private static string commandSql = "";

        static dataBase db = new dataBase();
        public static void comboboxAddItems(string param, string nameDb, string cmdSql, System.Windows.Controls.ComboBox comboBox)
        {
            //commandSql = $"SELECT {param} FROM {nameDb}";
            commandSql = cmdSql;
            //schoolList.ItemsSource = dataBase.getDataTable(commandSql);

            MySqlDataAdapter da = new MySqlDataAdapter(commandSql, db.getConnection());
            DataSet ds = new DataSet();
            da.Fill(ds, nameDb);
            comboBox.ItemsSource = ds.Tables[0].DefaultView;
            comboBox.DisplayMemberPath = ds.Tables[0].Columns[param].ToString();
        }
    }
}
