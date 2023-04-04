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
    /// Логика взаимодействия для WarehousePage.xaml
    /// </summary>
    public partial class WarehousePage : Page
    {
        private List<Item> itemVisual;
        public WarehousePage()
        {
            InitializeComponent();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
                return;
            Item item = dataGrid.SelectedValue as Item;
            try
            {
                intelicBDEntities.GetContext().Items.Remove(item);
                intelicBDEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные удалены!");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }

            UpdateList();
            dataGrid.ItemsSource = itemVisual;

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new WarehouseAddEditPage());

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
                return;

            NavigationService.Navigate(new WarehouseAddEditPage(dataGrid.SelectedItem as Item));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void tBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList();
            dataGrid.ItemsSource = itemVisual;
        }

        public void UpdateList()
        {
            itemVisual = intelicBDEntities.GetContext().Items.ToList();
            if(tBoxSearch.Text.Length > 0)
            itemVisual = itemVisual.Where(d =>  d.Name.ToLower().Contains(tBoxSearch.Text.ToLower())).ToList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateList();
            dataGrid.ItemsSource = itemVisual;
        }
    }
}
