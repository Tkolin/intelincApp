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
    /// Логика взаимодействия для DeliverAddEditPage.xaml
    /// </summary>
    public partial class DeliverAddEditPage : Page
    {

        private Deliver deliver;
        private bool newDeliver = true;
        public DeliverAddEditPage()
        {
            InitializeComponent();
        }
        public DeliverAddEditPage(Deliver deliver)
        {
            InitializeComponent();
            this.deliver = deliver;
            newDeliver = false;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            itemBox.SelectedValuePath = "ID";
            itemBox.DisplayMemberPath = "Name";
            itemBox.ItemsSource = intelicBDEntities.GetContext().Items.ToList();

            supplerBox.SelectedValuePath = "ID";
            supplerBox.DisplayMemberPath = "Name";
            supplerBox.ItemsSource = intelicBDEntities.GetContext().Suppliers.ToList();


            if (deliver != null)
            {
                idBox.Text = deliver.Number.ToString();
                dateBox.SelectedDate = deliver.Date;
                countBox.Text = deliver.Count.ToString();
                supplerBox.SelectedItem = deliver.Supplier;
                itemBox.SelectedItem = deliver.Item;
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            if (deliver == null)
                deliver = new Deliver();
            else
                count = (int)deliver.Count;




            Item item = itemBox.SelectedItem as Item;
            int tBoxCountValue = Convert.ToInt32(countBox.Text);
            if ((item.Count - count + tBoxCountValue) > 0)
            {
                deliver.Date = dateBox.SelectedDate;
                deliver.Count = tBoxCountValue;
                deliver.Item = item;
                deliver.Supplier = supplerBox.SelectedItem as Supplier;

                if (newDeliver)
                {
                    intelicBDEntities.GetContext().Delivers.Add(deliver);
                }

                item.Count -= count;
                item.Count += deliver.Count;
                intelicBDEntities.GetContext().SaveChanges();
                NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("На складе нет товара!");
                deliver = null;
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
