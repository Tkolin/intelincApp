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
        private bool newSale = true;
        public SellerAddEditPage()
        {
            InitializeComponent();
        }
        public SellerAddEditPage(Sale sale)
        {
            InitializeComponent();
            this.sale = sale;
            newSale = false;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            itemBox.SelectedValuePath = "ID";
            itemBox.DisplayMemberPath = "Name";
            if(sale != null)
                itemBox.ItemsSource = intelicBDEntities.GetContext().Items.ToList();
            else
                itemBox.ItemsSource = intelicBDEntities.GetContext().Items.Where(i=>i.Count > 0).ToList();

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
                count = (int)sale.Count;


            Item item = itemBox.SelectedItem as Item;
            int tBoxCountValue = Convert.ToInt32(countBox.Text);

            if ((item.Count + count - tBoxCountValue) >= 0)
            {

                sale.Date = dateBox.SelectedDate;
                sale.Count = tBoxCountValue;
                sale.Item = item;

                if (newSale)
                {
                    intelicBDEntities.GetContext().Sales.Add(sale);
                }

                item.Count += count;
                item.Count -= sale.Count;
                intelicBDEntities.GetContext().SaveChanges();
                NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("На складе нет товара!");
                sale = null;
            }
             
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
