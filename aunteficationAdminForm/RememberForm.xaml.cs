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

namespace aunteficationAdminForm
{
    /// <summary>
    /// Логика взаимодействия для RememberForm.xaml
    /// </summary>
    public partial class RememberForm : Window
    {
        public RememberForm()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int x;

                MailAddress fromMailAddress = new MailAddress("a_platon.2001@mail.ru", "japaneseProject");
                MailAddress toMailAddress = new MailAddress("a_platon.2001@mail.ru", "me");

                if (!String.IsNullOrEmpty(Email.Text) && !int.TryParse(Email.Text, out x))
                {
                    MailMessage msg = new MailMessage(fromMailAddress, toMailAddress);

                    msg.Subject = "test message";

                    msg.Body = "<h1>Hello</h1>";
                    msg.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient("smtp.mail.ru", 465);
                    smtp.Credentials = new NetworkCredential("a_platon.2001@mail.ru", "bokuhapuratondesu");
                    smtp.EnableSsl = true;
                    smtp.Send(msg);

                    MessageBox.Show("good");


                    //MailMessage mailMessage = new MailMessage(fromMailAddress, toMailAddress);
                    //mailMessage.Body = "first text mail";
                    //mailMessage.Subject = "mail Label!";

                    //SmtpClient smtpClient = new SmtpClient();
                    //smtpClient.Host = "smtp.mail.ru";
                    //smtpClient.Port = 465;
                    //smtpClient.EnableSsl = true;
                    //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //smtpClient.UseDefaultCredentials = false;
                    //smtpClient.Credentials = new NetworkCredential(fromMailAddress.Address, "bokuhapuratondesu");

                    //smtpClient.Send(mailMessage);

                    //MessageBox.Show("good");
                }
                else
                {
                    MessageBox.Show("Введите mail!",
                        "Incorrectly entered data",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }
            } catch
            {
                MessageBox.Show("again");

            }
        }
    }
}
