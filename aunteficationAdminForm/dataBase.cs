using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aunteficationAdminForm
{
    class dataBase
    {

        public readonly MySqlConnection connect = new MySqlConnection("server=localhost;port=3307;username=root;password=root;database=japaneseproject");

        DataTable dataTable = new DataTable();
        MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
        public void openConnectionPort()
        {
            if (connect.State == System.Data.ConnectionState.Closed)
                connect.Open();
        }

        public void closeConnectionPort()
        {
            if (connect.State == System.Data.ConnectionState.Open)
                connect.Close();
        }

        public MySqlConnection getConnection()
        {
            return connect;
        }

        // проверяем на одинаковое похожесть данных введённых пользователем с данными в бд
        public static string checkData(string textInput, string dbColumn)
        {

            string result = "";

            string[] nameDataBase = { "meneger", "operator" }; 

            for(int i = 0; i < nameDataBase.Length; i++)
            {
                DataTable dataTable = new DataTable();
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand($"SELECT * FROM {nameDataBase[i]} WHERE {dbColumn} LIKE @uL",
                    new MySqlConnection("server=localhost;port=3307;username=root;password=root;database=japaneseproject"));
                command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = textInput;

                dataAdapter.SelectCommand = command;
                dataAdapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    result = "Данная почта уже занята";
                    break;
                }
                else
                {
                    result = "";
                }
            }

            return result;
        }

        public void sqlWork(string commandSql)
        {
            DataTable dataTable = new DataTable();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(commandSql,
                new MySqlConnection("server=localhost;port=3307;username=root;password=root;database=japaneseproject"));

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataTable);
        }

        public static DataTable getDataTable(string commandSql)
        {
            DataTable dataTable = new DataTable();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(commandSql,
                new MySqlConnection("server=localhost;port=3307;username=root;password=root;database=japaneseproject"));

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataTable);

            return dataTable;
        }

        public static MySqlCommand getCommand(string commandSql)
        {
            DataTable dataTable = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(commandSql, 
                new MySqlConnection("server=localhost;port=3307;username=root;password=root;database=japaneseproject"));

            return command;
        }

        // В замарозке
        // Не работает 
        public void deleteRowFromSql(int selectedIndex, string nameDb)
        {
            string commandSqlText = "";

            var row = dataTable.Rows[selectedIndex];
            var rowColl = dataTable.Rows[selectedIndex][0];

            commandSqlText = $"DELETE FROM {nameDb} WHERE id = {rowColl}";

            MySqlCommand command = new MySqlCommand(commandSqlText, getConnection());
            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataTable);
            row.Delete();
        }
        //
        //
        public static string textBoxSearchNullInput(string[] tBoxName)
        {
            string[] boxName = tBoxName;
            string result = null;

            foreach (string str in boxName)
            {

                if (str == "" || str == " " || str == null)
                    result = "Введите данные или выберите редактируемое поле";
                else
                    result = "Всё сработало:)";
            }

            return result;
        }
    }
}
