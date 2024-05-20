using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для Добавить2.xaml
    /// </summary>
    public partial class Добавить2 : Window
    {
        public Добавить2()
        {
            InitializeComponent();
            _db.Товарыs.Load();
        }
        Tovary2Context _db = new Tovary2Context();
        Товары _Tov;

        private void btnAdd(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (Naz.Text.Length == 0) errors.AppendLine("Введите Код");
            if (Grup.Text.Length == 0) errors.AppendLine("Введите группу");
            if (Firma.Text.Length == 0) errors.AppendLine("Введите фирму");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            try
            {
                if (Data.Tov == null)
                {
                    _db.Товарыs.Add(_Tov);
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
            }
        }
        private void Clear(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_Loaded1(object sender, RoutedEventArgs e)
        {
            if (Data.Tov == null)
            {
                WindowAddEdit.Title = "Добавление записи";
                AddEdit.Content = "Добавить поставщика";
                _Tov = new Товары();
            }
            else
            {
                WindowAddEdit.Title = "Изменение записи";
                AddEdit.Content = "Изменить поставщика";
                _Tov = _db.Товарыs.Find(Data.Tov.Код);
            }
            WindowAddEdit.DataContext = _Tov;
        }
    }
}
