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
                MailAddress toMailAddress = new MailAddress("kigix36941@idcbill.com");

                if (!String.IsNullOrEmpty(Email.Text) && !int.TryParse(Email.Text, out x))
                {
                    MailMessage msg = new MailMessage(fromMailAddress, toMailAddress);

                    msg.Subject = "test message";

                    msg.Body = "<h1 style='font-size: 60px; color: pink; text-align: center;'>Hello</h1>";
                    msg.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
                    smtp.Credentials = new NetworkCredential("a_platon.2001@mail.ru", "bokuhapuratondesu");
                    smtp.EnableSsl = true;
                    smtp.Send(msg);

                    MessageBox.Show("На вашу почту отправлен код подтверждения", "Message",
                        MessageBoxButton.OK);
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
