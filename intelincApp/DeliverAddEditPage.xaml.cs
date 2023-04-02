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
        public DeliverAddEditPage()
        {
            InitializeComponent();
        }
        public DeliverAddEditPage(Deliver deliver)
        {
            InitializeComponent();
            this.deliver = deliver;
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
                count = deliver.Count;

            deliver.Date = dateBox.SelectedDate;
            deliver.Count =Convert.ToInt32( countBox.Text);
            deliver.Item = itemBox.SelectedItem as Item;
            deliver.Supplier = supplerBox.SelectedItem as Supplier;

           
           
           
            if (deliver.Number < 0)
            {
                intelicBDEntities.GetContext().Delivers.Add(deliver);
            }
            intelicBDEntities.GetContext().SaveChanges();
            deliver.Item.Count += count;
            intelicBDEntities.GetContext().SaveChanges();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
