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
    /// Логика взаимодействия для WarehouseAddEditPage.xaml
    /// </summary>
    public partial class WarehouseAddEditPage : Page
    {
        private Item item;
        public WarehouseAddEditPage()
        {
            InitializeComponent();
        }
        public WarehouseAddEditPage(Item item)
        {
            InitializeComponent();
            this.item = item;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (item != null)
            {
                idBox.Text = item.ID.ToString();
                nameBox.Text = item.Name;
                countBox.Text = item.Count.ToString();
                descritpBox.Text = item.Description;
               priceBox.Text = item.Price.ToString();
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (item == null)
                item = new Item();

            item.Name = nameBox.Text;
         
            item.Description = descritpBox.Text;
            item.Price = Convert.ToInt32(priceBox.Text);

            if (item.ID < 0)
            {
                intelicBDEntities.GetContext().Items.Add(item);
            }
            intelicBDEntities.GetContext().SaveChanges();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
