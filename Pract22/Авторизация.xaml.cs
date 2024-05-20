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
using System.Windows.Threading;

namespace Pract22
{
    /// <summary>
    /// Логика взаимодействия для Авторизация.xaml
    /// </summary>
    public partial class Авторизация : Window
    {
        public Авторизация()
        {
            InitializeComponent();
        }
        DispatcherTimer _timer;
        int _countLogin = 1;
        void GetCaptcha()
        {
            string masChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZshdfqweisd" + "1234567890yrtddfg";
            string captcha = "";
            Random rnd = new Random();
            for (int i = 1; i <= 6; i++)
            {
                captcha = captcha + masChar[rnd.Next(0, masChar.Length)];
            }
            Grid.Visibility = Visibility.Visible;
            Caprcha.Text = captcha;
            //Caprcha.Text = null;
            Caprcha.LayoutTransform = new RotateTransform(rnd.Next(-15, 15));
            line.X1 = 10;
            line.Y1 = rnd.Next(10, 40);
            line.X2 = 280;
            line.Y2 = rnd.Next(10, 40);
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            Login.Focus();
            Data.login = false;
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 10);
            _timer.Tick += new EventHandler(timer_Tick);
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            stackPanel.IsEnabled = true;
        }
        private void OnEnterButtonClick(object sender, RoutedEventArgs e)
        {
            using (Tovary2Context _db = new Tovary2Context())
            {
                var user = _db.Users.Where(user => user.UserLogin == Login.Text && user.UserPassword == Password.Password);
                if (user.Count() == 1 && Caprcha.Text == tbCaptcha.Text)
                {
                    Data.login = true;
                    Data.UserSurname = user.First().UserSurname;
                    Data.UserName = user.First().UserName;
                    Data.UserPatrononymic = user.First().UserPatronymic;
                    _db.Roles.Load();
                    Data.Right = user.First().UserRoleNavigation.RoleName;
                    Close();
                }
                else
                {
                    if (user.Count() == 1)
                    {
                        MessageBox.Show("Капча неверна! Повторите ввод");
                    }
                    else
                    {
                        MessageBox.Show("логин или пароль неверны! Повторите ввод");
                    }
                    GetCaptcha();
                    if (_countLogin >= 2)
                    {
                        stackPanel.IsEnabled = false;
                        _timer.Start();
                    }
                    _countLogin++;
                    Login.Focus();
                };
            }
        }
        private void EscClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void GuestClick(object sender, RoutedEventArgs e)
        {
            Data.login = true;
            Data.UserSurname = "Гость";
            Data.UserName = "";
            Data.UserPatrononymic = "";
            Data.Right = "клиент";
            Close();
        }
    }
}
