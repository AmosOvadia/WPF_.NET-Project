using PL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for OrderForList.xaml
    /// </summary>
    public partial class OrderForList : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();


        public ObservableCollection<BO.OrderForList?> orderForList
        {
            get { return (ObservableCollection<BO.OrderForList?>)GetValue(OrderListProperty); }
            set { SetValue(OrderListProperty, value); }
        }

        //public List<BO.OrderForList?> orderForList
        //{
        //    get { return (List<BO.OrderForList?>)GetValue(OrderListProperty); }
        //    set { SetValue(OrderListProperty, value); }
        //}
        public static readonly DependencyProperty OrderListProperty = DependencyProperty.Register(
        "orderForList", typeof(ObservableCollection<BO.OrderForList?>), typeof(OrderForList), new PropertyMetadata(default(ObservableCollection<BO.OrderForList?>)));

        public OrderForList()
        {
            orderForList = new ObservableCollection<BO.OrderForList?>(bl?.Order.GetOrders()!); // Take all the products from the logical layer and put them in the list
            InitializeComponent();
    
        }

        private void OrderForList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }








        //to update the product
        private void OrderForList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var orderForList = (BO.OrderForList)ListOfOrders.SelectedItem;
            new OrderDetails(orderForList, bl.Order.Order_tracking(orderForList.Id),1,UpdateToOrders).Show();
          //  Close();// Close the product list window

        }





        private void UpdateToOrders(int orderID)
        {
            var x = ListOfOrders.SelectedIndex;
            orderForList[x] = (((bl?.Order.GetOrders(a => a?.Id == orderID).First())));
        }



        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new Product_Order().Show();
            Close();
        }
    }
}