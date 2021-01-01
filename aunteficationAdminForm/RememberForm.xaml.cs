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
using System.Net;
using System.Net.Mail;
using System.Data;
using MySql.Data.MySqlClient;

namespace aunteficationAdminForm
{
    /// <summary>
    /// Логика взаимодействия для RememberForm.xaml
    /// </summary>
    public partial class RememberForm : Window
    {
        String userMail;

        dataBase db = new dataBase();

        DataTable dataTable = new DataTable();
        MySqlDataAdapter dataAdapter = new MySqlDataAdapter();

        Random rnd = new Random();

        string generateParol;

        public RememberForm()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int x;
                userMail = Email.Text;

                generateParol = rnd.Next(0, 100).ToString();

                MailAddress fromMailAddress = new MailAddress($"a_platon.2001@mail.ru", "japaneseProject");
                MailAddress toMailAddress = new MailAddress($"{userMail}");

                if (!String.IsNullOrEmpty(Email.Text) && !int.TryParse(Email.Text, out x))
                {
                    MailMessage msg = new MailMessage(fromMailAddress, toMailAddress);

                    msg.Subject = "test message";

                    msg.Body = $"<h1 style='font-size: 60px; color: pink; text-align: center;'>{generateParol}</h1>";
                    msg.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
                    smtp.Credentials = new NetworkCredential($"a_platon.2001@mail.ru", "bokuhapuratondesu");
                    smtp.EnableSsl = true;
                    smtp.Send(msg);

                    checkUserMail();

                    MessageBox.Show("На вашу почту отправлен код подтверждения", "Message",
                        MessageBoxButton.OK);

                    visibiliteButton();
                }
                else
                {
                    MessageBox.Show("Введите почту!",
                        "Incorrectly entered data",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        void checkUserMail()
        {
            if(userMail != "")
            {
                sqlMail();

                if (dataTable.Rows.Count > 0)
                    parol.IsEnabled = true;
          
            }
        }

        void sqlMail()
        {
            MySqlCommand command = new MySqlCommand($"SELECT * FROM autoriz_men WHERE `log` LIKE @uL", db.getConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = userMail;

            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataTable);
        }

        void checkParol()
        {
            if(parol.Text == generateParol)
            {
                RegistrationMeneger rMeneger = new RegistrationMeneger(true, userMail);
                rMeneger.ShowDialog();
            } else
            {
                MessageBox.Show("Введёный пароль неверный");
            }
        }

        void visibiliteButton()
        {
            if(btnMail.Visibility == Visibility.Visible)
            {
                btnMail.Visibility = Visibility.Hidden;
                btnParol.Visibility = Visibility.Visible;
            }
        }

        private void btnParol_Click(object sender, RoutedEventArgs e)
        {
            if(parol.Text != "")
            {
                checkParol();
            }
        }
    }
}
