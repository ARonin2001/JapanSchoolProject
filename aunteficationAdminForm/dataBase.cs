using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aunteficationAdminForm
{
    class dataBase
    {
        MySqlConnection connect = new MySqlConnection("server=localhost;port=3307;username=root;password=root;database=japaneseproject");
        public void openConnectionPort()
        {
            if (connect.State == System.Data.ConnectionState.Closed)
            {
                connect.Open();
            }
        }

        public void closeConnectionPort()
        {
            if (connect.State == System.Data.ConnectionState.Open)
            {
                connect.Close();
            }
        }

        public MySqlConnection getConnection()
        {
            return connect;
        }
    }
}
