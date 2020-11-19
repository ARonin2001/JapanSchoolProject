using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aunteficationAdminForm
{
    class ChangeDate
    {
        public static string dateAmerica(string dateText)
        {
            string oneDate = "";
            string twoDate = "";

            string[] strDate = dateText.Split(new char[] { '.' });

            oneDate = strDate[2];
            twoDate = strDate[0];

            string dateBirthSql = oneDate + "." + strDate[1] + "." + twoDate;

            return dateBirthSql;
        }
    }
}
