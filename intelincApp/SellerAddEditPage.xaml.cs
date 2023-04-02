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
    /// Логика взаимодействия для SellerAddEditPage.xaml
    /// </summary>
    public partial class SellerAddEditPage : Page
    {
        private Sale sale;

        public SellerAddEditPage()
        {
            InitializeComponent();
        }
        public SellerAddEditPage(Sale sale)
        {
            InitializeComponent();
            this.sale = sale;
            
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            itemBox.SelectedValuePath = "ID";
            itemBox.DisplayMemberPath = "Name";
            itemBox.ItemsSource = intelicBDEntities.GetContext().Items.ToList();



            if (sale != null)
            {
                idBox.Text = sale.Number.ToString();
                dateBox.SelectedDate = sale.Date;
                countBox.Text = sale.Count.ToString();            
                itemBox.SelectedItem = sale.Item;
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            if (sale == null)
                sale = new Sale();
            else
                count = sale.Count;

            sale.Date = dateBox.SelectedDate;
            sale.Count = Convert.ToInt32(countBox.Text);
            sale.Item = itemBox.SelectedItem as Item;

            if((sale.Item.Count + count) < sale.Count)
            {
                countBox.Text = null;
                MessageBox.Show("На складе не хватает тавара!");
                return;
            }

            if (sale.Number < 0)
            {
                intelicBDEntities.GetContext().Sales.Add(sale);
            }
            intelicBDEntities.GetContext().SaveChanges();
            sale.Item.Count -= count;
            intelicBDEntities.GetContext().SaveChanges();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void nowDateBtn_Click(object sender, RoutedEventArgs e)
        {
            dateBox.SelectedDate = DateTime.Now;    
        }
    }
}
