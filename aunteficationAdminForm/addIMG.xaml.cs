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
using MySql.Data.MySqlClient;
using System.Data;
using System.Threading;
using System.IO;
using f = System.Windows.Forms;


namespace aunteficationAdminForm
{
    /// <summary>
    /// Логика взаимодействия для addIMG.xaml
    /// </summary>
    public partial class addIMG : Window
    {
        dataBase db = new dataBase();

        public string nameDb;

        public int count = 0;
        public int i = 0;

        public string[] imgArr = new string[5] { "null", "null", "null", "null", "null" };

        public string commandSql;

        public string nameT, adres, description, com, renta, depozit;
        public int contract;

        public bool result, res = false;
        public bool x = false;
        public string idPhoto;

        public addIMG()
        {
            InitializeComponent();
        }
        public addIMG(string name)
        {
            InitializeComponent();
            nameDb = name;
            Load();
        }
        public addIMG(string name, bool r)
        {
            InitializeComponent();
            nameDb = name;
            res = r;
            Load();
        }

        public addIMG(string nameDB, string name, string adr, string desc, string comus, string rent, string dep, int cont)
        {
            InitializeComponent();
            nameDb = nameDB;
            nameT = name;
            adres = adr;
            description = desc;
            com = comus;
            renta = rent;
            depozit = dep;
            contract = cont;
            Load();
        }

        async void Load()
        {
            WrapList.Children.Clear();
            BRef.IsEnabled = false;
            SelectIMG.Source = null;
            SelectIMG.DataContext = null;
            string command = $"select * from {nameDb}";
            MySqlCommand cmd = new MySqlCommand(command, db.getConnection());
            if (db.connect.State == ConnectionState.Closed)
                await db.connect.OpenAsync();
            System.Data.Common.DbDataReader red = await cmd.ExecuteReaderAsync();
            // - В Mysql.Data
            // - System.Data.Common.DbDataReader red = (System.Data.Common.DbDataReader)await cmd.ExecuteNonQueryAsync();
            if (red.HasRows)
                while (await red.ReadAsync())
                    await Task.Run(() =>
                    {
                        Thread.Sleep(50);
                        Dispatcher.Invoke(() => GetByteIMG(red[0].ToString(), red[1].ToString()));
                    });
            else
                Console.WriteLine("БД пустая");
            //red.DisposeAsync();
            red.Dispose();
            cmd.Dispose();
            BRef.IsEnabled = true;
        }

        void GetByteIMG(string ID, string ByteGet)
        {

            byte[] imgB = ByteGet.Split(';').Select(a => byte.Parse(a)).ToArray();
            MemoryStream ms = new MemoryStream(imgB);
            Image i = new Image();
            i.Source = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            i.MouseDown += I_MouseDown;
            i.Style = TryFindResource("IMG") as Style;
            i.DataContext = ID;
            WrapList.Children.Add(i);
        }

        private void I_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(sender is Image i)
            {
                SelectIMG.Source = i.Source;
                SelectIMG.DataContext = i.DataContext;
            }
        }

        //
        private void AddIMG(object sender, RoutedEventArgs e)
        {
            if(count >= 5)
            {
                MessageBox.Show("Максимум картинок 5!", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                f.OpenFileDialog o = new f.OpenFileDialog();
                o.Filter = "Картиночки мне давай | *.png; *.jpg; *.jpeg";
                if (o.ShowDialog() == f.DialogResult.OK)
                {
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.UriSource = new Uri(o.FileName, UriKind.Relative);
                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.DecodePixelHeight = 256;
                    bi.EndInit();
                    GetIMGFolder(bi);
                }
            }
        }

        //
        private DataSet connectSql(string commandSql)
        {
            MySqlDataAdapter da = new MySqlDataAdapter(commandSql, db.getConnection());
            DataSet ds = new DataSet();
            da.Fill(ds, $"{nameDb}");

            return ds;
        }

        //
        void forImg(DataSet ds, string[] dataSql)
        {
            int c = 0;
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataRow row in dt.Rows)
                {
                    // получаем все ячейки строки
                    var cells = row.ItemArray;
                    foreach (object cell in cells)
                    {
                        if(c < 1)
                            dataSql[i] = cell.ToString();
                        c++;
                    }
                }
            }
            i++;
        }
        //

        void GetIMGFolder(BitmapImage bi)
        {
            PngBitmapEncoder pe = new PngBitmapEncoder();
            MemoryStream ms = new MemoryStream();
            StringBuilder sb = new StringBuilder();
            pe.Frames.Add(BitmapFrame.Create(bi));
            pe.Save(ms);
            byte[] imgB = ms.ToArray();
            foreach (byte b in imgB)
                sb.Append(b).Append(";");
            sb.Remove(sb.Length - 1, 1);
            GetIMGBD(sb.ToString());
        }

        async void GetIMGBD(string Get)
        {
            string command = $"insert into {nameDb} (img) values(@B)";
            MySqlCommand cmd = new MySqlCommand(command, db.getConnection());
            cmd.Parameters.Add("@B", MySqlDbType.LongText).Value = Get;
            if (db.connect.State == ConnectionState.Closed)
                await db.connect.OpenAsync();
            if (await cmd.ExecuteNonQueryAsync() == 1)
            {
                MessageBox.Show("Картинка успешно добавлена!)");

                DataSet ds = connectSql($"select * from {nameDb} order by id desc limit 1");
                forImg(ds, imgArr);

                count++;
            }
            else
                MessageBox.Show("Ошибка добавления", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);

            cmd.Dispose();
        }



        private void RefreshIMG(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private async void RemoveIMG(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Вы точно хотите удалить каритнку", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                string command = $"delete from {nameDb} where id = @ID";
                MySqlCommand cmd = new MySqlCommand(command, db.getConnection());
                cmd.Parameters.Add("@ID", MySqlDbType.Int32).Value = SelectIMG.DataContext;
                if (db.connect.State == ConnectionState.Closed)
                    await db.connect.OpenAsync();
                if (await cmd.ExecuteNonQueryAsync() == 1)
                {
                    Load();
                    MessageBox.Show("Картинка успешно удалена");
                    if (count > 0) count--;
                }
                else
                    MessageBox.Show("Ошибка Удаления");
                cmd.Dispose();
            }
        }

        private void finish_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (count == 0)
                {
                    this.Close();
                }
                else
                {
                    commandSql = $"insert into containerimg (`img_one`, `img_two`, `img_three`, `img_four`, `img_five`)" +
                        $"  VALUES({imgArr[0]}, {imgArr[1]}, {imgArr[2]}, {imgArr[3]}, {imgArr[4]})";
                    db.sqlWork(commandSql);
                    MessageBox.Show("Фотографии успешно добавлены!)");
                    x = true;

                    if (nameDb == "imgschools")
                    {
                        forSchoolsIdIMG("containerimg");
                    }

                    this.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void forSchoolsIdIMG(string nameD)
        {
            int c = 0;
            DataSet ds = connectSql($"select * from {nameD} order by id desc limit 1");
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataRow row in dt.Rows)
                {
                    var cells = row.ItemArray;
                    foreach (object cell in cells)
                    {
                        if (c < 1)
                            idPhoto = cell.ToString();
                        c++;
                    }
                }
            }
            i++;
        }

        void getIdSchoolsIMG()
        {
            DataSet ds = connectSql($"select * from {nameDb} order by id desc limit 1");
            forImg(ds, imgArr);
        }

        bool getResult() {
            if (count > 0)
                result = true;
            else
                result = false;

            return result;
        }
    }
}
