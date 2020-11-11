using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace aunteficationAdminForm
{
    class registrationUsers
    {
        private string NameDbOne { get; set; }
        private string NameDbTwo { get; set; }
        private string sqlCom;

        public registrationUsers(string nameDbOne, string nameDbTwo)
        {
            NameDbOne = nameDbOne;
            NameDbTwo = nameDbTwo;
        }

        public string intoDateUser(string idAutorizUser, string idUser, string par)
        {
            try
            {
                sqlCom = $"INSERT INTO {NameDbOne} (log, par, {idAutorizUser})" +
                $" VALUES((SELECT MAX(mail) FROM {NameDbTwo}), MD5('{par}'), (SELECT MAX({idUser}) FROM {NameDbTwo}))";

                dataBase db = new dataBase();

                DataTable dataTable = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand(sqlCom, db.getConnection());

                adapter.SelectCommand = command;
                adapter.Fill(dataTable);

            }
            catch
            {
                return "Error";
            }

            return sqlCom;
        }
    }
}
