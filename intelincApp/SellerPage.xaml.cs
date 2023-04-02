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
    /// Логика взаимодействия для SellerPage.xaml
    /// </summary>
    public partial class SellerPage : Page
    {
        private List<Sale> saleVisual = intelicBDEntities.GetContext().Sales.ToList();
        public SellerPage()
        {
            InitializeComponent();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
                return;
            var sale = dataGrid.SelectedItems as Sale;
            try
            {
                intelicBDEntities.GetContext().Sales.Remove(sale);
                intelicBDEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные удалены! ");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }

            UpdateList();

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SellerAddEditPage());

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
                return;

            NavigationService.Navigate(new SellerAddEditPage(dataGrid.SelectedItem as Sale));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void tBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList();
        }

        public void UpdateList()
        {
            saleVisual = saleVisual.Where(d => tBoxSearch.Text.Length > 0 && d.Item.Name.ToLower().Contains(tBoxSearch.Text.ToLower())).ToList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateList();
            dataGrid.ItemsSource = saleVisual;
        }
    }
}
