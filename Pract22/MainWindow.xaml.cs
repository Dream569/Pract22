using Microsoft.IdentityModel.Tokens;
using Pract22.Model;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Pract22.Добавить1;

namespace Pract22
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        void LoadDBIListView()
        {
            using (Tovary2Context _db = new Tovary2Context())
            {
                int selectedIndex = List1.SelectedIndex;
                List1.ItemsSource = _db.Складыs.ToList();
                if (selectedIndex != -1)
                {
                    if (selectedIndex == List1.Items.Count) selectedIndex--;
                    List1.SelectedIndex = selectedIndex;
                    List1.ScrollIntoView(List1.SelectedItem);
                }
                List1.Focus();
            }
            using (Tovary2Context _db = new Tovary2Context())
            {
                int selectedIndex = List2.SelectedIndex;
                List2.ItemsSource = _db.Товарыs.ToList();
                if (selectedIndex != -1)
                {
                    if (selectedIndex == List2.Items.Count) selectedIndex--;
                    List2.SelectedIndex = selectedIndex;
                    List2.ScrollIntoView(List2.SelectedItem);
                }
                List2.Focus();
            }
            using (Tovary2Context _db = new Tovary2Context())
            {
                int selectedIndex = DataGrid4.SelectedIndex;
                DataGrid4.ItemsSource = _db.НаличиеТоваровs.ToList();
                if (selectedIndex != -1)
                {
                    if (selectedIndex == DataGrid4.Items.Count) selectedIndex--;
                    DataGrid4.SelectedIndex = selectedIndex;
                    DataGrid4.ScrollIntoView(DataGrid4.SelectedItem);
                }
                DataGrid4.Focus();
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDBIListView();
        }
        private void Window_Initialized(object sender, EventArgs e)
        {
            Авторизация f = new Авторизация();
            f.ShowDialog();
            if (Data.login == false) Close();
            if (Data.Right == "Admin")
            {

            }
            else
            {
                btnEdit.IsEnabled = false;
                btnAdd.IsEnabled = false;
                btnDelete.IsEnabled = false;
                btnEdit1.IsEnabled = false;
                btnAdd1.IsEnabled = false;
                btnDelete1.IsEnabled = false;
                btnAdd2.IsEnabled = false;
                btnDelete2.IsEnabled = false;
            }
            Title = Title + " " + Data.UserSurname + " " + Data.UserName + " (" + Data.Right + ")";
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Data.Sklad = null;
            Добавить1 f = new Добавить1();
            f.Owner = this;
            f.ShowDialog();
            LoadDBIListView();
        }
        private void Redact_Click(object sender, RoutedEventArgs e)
        {
            if (List1.SelectedItem != null)
            {
                Data.Sklad = (Склады)List1.SelectedItem;
                Добавить1 f = new Добавить1();
                f.Owner = this;
                f.ShowDialog();
                LoadDBIListView();
            }
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Склады row = (Склады)List1.SelectedItem;
                    if (row != null)
                    {
                        using (Tovary2Context _db = new Tovary2Context())
                        {
                            _db.Складыs.Remove(row);
                            _db.SaveChanges();
                        }
                        LoadDBIListView();
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка удаления");
                }
            }
            else List1.Focus();
        }
        private void Add_Click1(object sender, RoutedEventArgs e)
        {
            Data.Tov = null;
            Добавить2 f = new Добавить2();
            f.Owner = this;
            f.ShowDialog();
            LoadDBIListView();
        }
        private void Redact_Click1(object sender, RoutedEventArgs e)
        {
            if (List2.SelectedItem != null)
            {
                Data.Tov = (Товары)List2.SelectedItem;
                Добавить2 f = new Добавить2();
                f.Owner = this;
                f.ShowDialog();
                LoadDBIListView();
            }
        }
        private void Clear_Click1(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Товары tow = (Товары)List2.SelectedItem;
                    if (tow != null)
                    {
                        using (Tovary2Context _db = new Tovary2Context())
                        {
                            _db.Товарыs.Remove(tow);
                            _db.SaveChanges();
                        }
                        LoadDBIListView();
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка удаления");
                }
            }
            else List2.Focus();
        }
        private void Add_Click3(object sender, RoutedEventArgs e)
        {
            Data.Nal = null;
            Добавить3 f = new Добавить3();
            f.Owner = this;
            f.ShowDialog();
            LoadDBIListView();
        }
        private void Clear_Click3(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    НаличиеТоваров tow = (НаличиеТоваров)DataGrid4.SelectedItem;
                    if (tow != null)
                    {
                        using (Tovary2Context _db = new Tovary2Context())
                        {
                            _db.НаличиеТоваровs.Remove(tow);
                            _db.SaveChanges();
                        }
                        LoadDBIListView();
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка удаления");
                }
            }
            else DataGrid4.Focus();
        }
        private void Poisk_Click(object sender, RoutedEventArgs e)
        {
            List<Товары> listItem = (List<Товары>)List2.ItemsSource;
            var filtered = listItem.Where(p => p.Название.Contains(Poisk.Text)
            || (!string.IsNullOrEmpty(p.ГруппаТоваров) ? p.ГруппаТоваров.Contains(Poisk.Text) : false)
            || (!string.IsNullOrEmpty(p.ФирмаПроизводитель) ? p.ФирмаПроизводитель.Contains(Poisk.Text) : false)
            );
            if (filtered.Count() > 0)
            {
                var item = filtered.First();
                List2.SelectedItem = item;
                List2.ScrollIntoView(item);
                List2.Focus();
            }
        }
        private void Filtr_Click(object sender, RoutedEventArgs e)
        {
            if (Filtr.Text.IsNullOrEmpty() == false)
            {
                using (Tovary2Context _db = new Tovary2Context())
                {
                    var filtered = _db.Товарыs.Where(p => p.Название.Contains(Filtr.Text)
                    || (!string.IsNullOrEmpty(p.ГруппаТоваров) ? p.ГруппаТоваров.Contains(Filtr.Text) : false)
                    || (!string.IsNullOrEmpty(p.ФирмаПроизводитель) ? p.ФирмаПроизводитель.Contains(Filtr.Text) : false)
                    );
                    List2.ItemsSource = filtered.ToList();
                }
            }
            else
            {
                LoadDBIListView();
            }
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Практическая работа №22\n" +
         "Необходимо хранить информацию о существующих складах (номер склада, адрес, \r\nтелефон, фамилия руководителя склада), товарах (код, название, группа товара, фирма\u0002производитель), а также о наличии товаров на конкретных складах с указанием \r\nколичества товаров.\r\n",
         "О программе", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}