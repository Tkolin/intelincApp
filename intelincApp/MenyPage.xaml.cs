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
    /// Логика взаимодействия для MenyPage.xaml
    /// </summary>
    public partial class MenyPage : Page
    {
        public MenyPage()
        {
            InitializeComponent();
        }

        private void deliverBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DeliverPage());
        }

        private void sellerBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SellerPage());
        }

        private void warehouseBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new WarehousePage());
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
