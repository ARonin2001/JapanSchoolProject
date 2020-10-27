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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace aunteficationAdminForm
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class Autorization : Window
    {
        public Autorization()
        {
            InitializeComponent();
        }

        private void lab_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DoubleAnimation lAn = new DoubleAnimation();
            lAn.From = lab.FontSize;
            lAn.To = 18;
            lAn.Duration = TimeSpan.FromSeconds(3);
            lab.BeginAnimation(Label.FontSizeProperty, lAn);
        }

    }
}
