using BO;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace PL
{
    /// <summary>
    /// Interaction logic for OrderDetails.xaml
    /// </summary>
    /// <summary>
    /// Interaction logic for OrderDetails.xaml
    /// </summary>
    public partial class OrderDetails : Window
    {
        private Action<int> Action;

        BlApi.IBl? bl = BlApi.Factory.Get();
        public BO.Order? orderBinding
        {
            get { return (BO.Order)GetValue(orderProperty); }
            set { SetValue(orderProperty, value); }
        }
        public static readonly DependencyProperty orderProperty = DependencyProperty.Register(
        "orderBinding", typeof(BO.Order), typeof(OrderDetails), new PropertyMetadata(default(BO.Order)));

        public List<BO.OrderItem?> OrderItems
        {
            get { return (List<BO.OrderItem?>)GetValue(itemsProperty); }
            set { SetValue(itemsProperty, value); }
        }
        public static readonly DependencyProperty itemsProperty = DependencyProperty.Register(
        "OrderItems", typeof(List<BO.OrderItem?>), typeof(OrderDetails), new PropertyMetadata(default(List<BO.OrderItem?>)));




        public OrderDetails(BO.OrderForList order2, BO.OrderTracking order1,int i, Action<int> action)
        {
            orderBinding = new();
            OrderItems = new();
            InitializeComponent();
            this.Action = action;
            if (i == 0)
            {
                UpdateDelivery.Visibility = Visibility.Hidden;
                OrderDate.IsEnabled = false;
                ShipDate.IsEnabled = false;
                DelivoryDate.IsEnabled = false;
            }
            BO.Order? MyOrder = new BO.Order();
            if (order2.Status != null)
            {
                MyOrder = bl?.Order.GetOrder(order2.Id);
                //  GoBackToListOrderTracking.Visibility = Visibility.Hidden;
                //   this.DataContext = MyOrder;

                orderBinding = MyOrder;
            }
            if (order1.Status != null)
            {
                MyOrder = bl?.Order.GetOrder(order1.Id);

                // this.DataContext = MyOrder;
                orderBinding = MyOrder;
            }

            if (order2.Status == BO.Enums.OrderStatus.Delivered)
            {
                UpdateDelivery.Visibility = Visibility.Hidden;
                UpdateShip.Visibility= Visibility.Hidden;
            }
            if (order2.Status == BO.Enums.OrderStatus.Sent)
            {
                UpdateShip.Visibility = Visibility.Hidden;
            }
            if (order2.Status == BO.Enums.OrderStatus.Confirmed)
            {
                UpdateDelivery.Visibility = Visibility.Hidden;
            }
            //    ListUpdateOrder.ItemsSource
            //DataContext = MyOrder?.Items;
            //   OrderItems = MyOrder.Items!;

        }



        private void Status_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void UpdateBottun_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            int tempInt1 = 0;
            BO.Order? Order1 = new BO.Order();
            int.TryParse(ID.Text, out tempInt1);
            Order1.Id = tempInt1;

            try
            {
                bl?.Order.OrderDeliveryUpdate(Order1.Id);
                check = true;
            }
            catch (VariableIsNull ex)
            {

                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.InputError ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (check == true)
            {
                UpdateDelivery.Visibility = Visibility.Hidden;
                // new OrderForList().Show();

                Close();
            }
            Action?.Invoke(orderBinding.Id);

        }

        private void Name_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void ID_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void Adress_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void Price_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void GoBackToListOrderTracking_Click(object sender, RoutedEventArgs e)
        {
            //new Order.OrderTracking().Show();
            //Close();
        }

        private void GoBackToListOrder_Click(object sender, RoutedEventArgs e)
        {
           // new OrderForList().Show();
            Close();
        }

        private void ListUpdateOrder_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
           // new OrderForList().Show();
            Close();
        }

        private void UpdateShip_Click(object sender, RoutedEventArgs e)
        {

            bool check = false;
            int tempInt1 = 0;
            BO.Order? Order1 = new BO.Order();
            int.TryParse(ID.Text, out tempInt1);
            Order1.Id = tempInt1;

            try
            {
                bl?.Order.OrderShippingUpdate(Order1.Id);
                check = true;
            }
            catch (VariableIsNull ex)
            {

                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.InputError ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (check == true)
            {
                UpdateShip.Visibility = Visibility.Hidden;
               // new OrderForList().Show();
                Close();
            }
            Action?.Invoke(orderBinding.Id);

        }




    }
}