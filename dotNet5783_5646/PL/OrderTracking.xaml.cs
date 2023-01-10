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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderTracking.xaml
    /// </summary>
    public partial class OrderTracking : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        int? ID;
        public OrderTracking(int? id)
        {
            InitializeComponent();

            OrderTackingText.Text = (bl?.Order.Order_tracking((int)id!))?.ToString();
            ID = id;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new PL.MainWindow().Show();
            Close();
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BO.OrderForList? order = new BO.OrderForList();
            BO.OerderTracking? order1 = new BO.OerderTracking();

            order1 = bl?.Order.Order_tracking((int)ID!);

            if (order1 != null)
            {
                new OrderDetails(order, order1,0).Show();
                Close();
            }
        }
    }
}