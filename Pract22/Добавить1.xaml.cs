using Microsoft.EntityFrameworkCore;
using Pract22.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Pract22
{
    /// <summary>
    /// Логика взаимодействия для Добавить1.xaml
    /// </summary>
    public partial class Добавить1 : Window
    {
        public Добавить1()
        {
            InitializeComponent();
            _db.Складыs.Load();
        }
        Tovary2Context _db = new Tovary2Context();
        Склады _склда;
        OpenFileDialog open = new OpenFileDialog();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Data.Sklad == null)
            {
                WindowAddEdit.Title = "Добавление записи";
                AddEdit.Content = "Добавить";
                _склда = new Склады();
            }
            else
            {
                WindowAddEdit.Title = "Изменение записи";
                AddEdit.Content = "Изменить";
                _склда = _db.Складыs.Find(Data.Sklad.Номер);
            }
            WindowAddEdit.DataContext = _склда;
        }
        private void btnAdd(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (FIO1.Text.Length == 0) errors.AppendLine("Введите ФИО");
            if (Tel.Text.Length == 0) errors.AppendLine("Введите телефон");
            if (AdrSkl.Text.Length == 0) errors.AppendLine("Введите адрес");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            try
            {
                if (open.SafeFileName.Length != 0)
                {
                    string newNamePhoto = Directory.GetCurrentDirectory() + "\\image\\" + open.SafeFileName;
                    File.Copy(open.FileName, newNamePhoto, true);
                    _склда.Photo = open.SafeFileName.ToString();
                }
            } catch { }
            try
            {
                if (Data.Sklad == null)
                {
                    _db.Складыs.Add(_склда);
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
        private void AddPhoto(object sender, RoutedEventArgs e)
        {
            open.Filter = "Все файлы |*.*| Файлы *.jpg|*.jpg";
            open.FilterIndex = 2;
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                BitmapImage photoImage = new BitmapImage(new Uri(open.FileName));
                Photo.Source = photoImage;
            }
        }
        private void Clear(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
