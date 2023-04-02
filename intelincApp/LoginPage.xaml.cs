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

namespace intelincApp
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            db = new intelicBDEntities();
        }
        private intelicBDEntities db;


        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string password = Password.Text;

            var user = db.Users.FirstOrDefault(u => u.Login == login && u.Password == password);

            if (user != null)
            {
                if (user.RoleID == 1)
                {
                    NavigationService.Navigate(new DeliverPage());
                }
                else if (user.RoleID == 2)
                {
                    NavigationService.Navigate(new SellerPage());
                }
                else if (user.RoleID == 3)
                {
                    NavigationService.Navigate(new MenyPage());
                }
            }
            else
            {
                MessageBox.Show("Ошибка входа");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

