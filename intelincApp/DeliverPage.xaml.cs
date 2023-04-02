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
    /// Логика взаимодействия для DeliverPage.xaml
    /// </summary>
    public partial class DeliverPage : Page
    {
        private List<Deliver> devVisual = intelicBDEntities.GetContext().Delivers.ToList();
        public DeliverPage()
        {
            InitializeComponent();

        }
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
                return;
            var deliver = dataGrid.SelectedItems as Deliver;
            try
            {
                intelicBDEntities.GetContext().Delivers.Remove(deliver);
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
            NavigationService.Navigate(new DeliverAddEditPage());

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
                return;

            NavigationService.Navigate(new DeliverAddEditPage(dataGrid.SelectedItem as Deliver));
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
           devVisual = devVisual.Where(d=> tBoxSearch.Text.Length > 0 && d.Item.Name.ToLower().Contains(tBoxSearch.Text.ToLower())).ToList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateList();
            dataGrid.ItemsSource = devVisual;
        }
    }
}
