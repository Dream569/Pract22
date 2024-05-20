using Pract22.Model;
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

namespace Pract22
{
    /// <summary>
    /// Логика взаимодействия для Добавить3.xaml
    /// </summary>
    public partial class Добавить3 : Window
    {
        public Добавить3()
        {
            InitializeComponent();
        }
        Tovary2Context _db = new Tovary2Context();
        НаличиеТоваров _Nal;

        private void Fill_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (Nom.Text.Length == 0) errors.AppendLine("Введите номер");
            if (Kod.Text.Length == 0) errors.AppendLine("Введите код");
            if (Kol.Text.Length == 0) errors.AppendLine("Введите количество");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            try
            {
                if (Data.Nal == null)
                {
                    _db.НаличиеТоваровs.Add(_Nal);
                    _db.SaveChanges();
                }
                else
                {
                    _db.SaveChanges();
                }
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
#if DEBUG
                throw;
#endif
            }
        }
        private void Cansel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_Loaded4(object sender, RoutedEventArgs e)
        {
            Nom.ItemsSource = _db.НаличиеТоваровs.ToList();
            Nom.DisplayMemberPath = "НомерСклада";
            Kod.ItemsSource = _db.НаличиеТоваровs.ToList();
            Kod.DisplayMemberPath = "КодТовара";
            if (Data.Nal == null)
            {
                WindowAddEdit.Title = "Добавление записи";
                btnAddEdit.Content = "Добавить поставщика";
                _Nal = new НаличиеТоваров();
            }
            else
            {
                WindowAddEdit.Title = "Изменение записи";
                btnAddEdit.Content = "Изменить поставщика";
                _Nal = _db.НаличиеТоваровs.Find(Data.Nal.НомерСклада, Data.Nal.КодТовара);
            }
            WindowAddEdit.DataContext = _Nal;
        }
    }
}
