using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for CartList.xaml
    /// </summary>
    public partial class CartList : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //private ObservableCollection<BO.OrderItem> _cartItems;
        //public ObservableCollection<BO.OrderItem> cartItems
        //{
        //    get { return _cartItems; }
        //    set
        //    {
        //        _cartItems = value;
        //        OnPropertyChanged(nameof(cartItems));
        //    }
        //}

        private BO.Cart _cart;
        public BO.Cart cart
        {
            get { return _cart; }
            set
            {
                _cart = value;
                OnPropertyChanged(nameof(cart));
            }
        }



        BO.Cart dataCart = new BO.Cart();


        public ObservableCollection<BO.OrderItem?> cartItems
        {
            get { return (ObservableCollection<BO.OrderItem?>)GetValue(cartItemsProperty); }
            set { SetValue(cartItemsProperty, value); }
        }

      
        public static readonly DependencyProperty cartItemsProperty = DependencyProperty.Register(
        "cartItemsProperty", typeof(ObservableCollection<BO.OrderItem?>), typeof(CartList), new PropertyMetadata(default(ObservableCollection<BO.OrderItem?>)));



        public double PriceP
        {
            get { return (double)GetValue(priceProperty); }
            set { SetValue(priceProperty, value); }
        }
        public static readonly DependencyProperty priceProperty = DependencyProperty.Register(
        "PriceP", typeof(double), typeof(CartList), new PropertyMetadata(default(double)));



        //public List<BO.OrderItem?> cartItems
        //{
        //    get { return (List<BO.OrderItem?>)GetValue(cartProperty); }
        //    set { SetValue(cartProperty, value); }
        //}
        //public static readonly DependencyProperty cartProperty = DependencyProperty.Register(
        //"cartItems", typeof(List<BO.OrderItem?>), typeof(CartList), new PropertyMetadata(default(List<BO.OrderItem?>)));

        //public BO.Cart? cart
        //{
        //    get { return (BO.Cart?)GetValue(cartPropertyTotal); }
        //    set { SetValue(cartPropertyTotal, value); }
        //}
        //public static readonly DependencyProperty cartPropertyTotal = DependencyProperty.Register(
        //"cart", typeof(BO.Cart), typeof(CartList), new PropertyMetadata(default(BO.Cart?)));




        //  BO.Cart dataCart = new BO.Cart();
        public CartList(BO.Cart? cart1, Action<int> action)
        {
            cart = new();
            cartItems = new();
            cartItems = new ObservableCollection<BO.OrderItem>(cart1.Items!.ToList())!; 
            PriceP = new();
            InitializeComponent();

            cart = cart1;
            PriceP = cart1.TotalPrice;
            dataCart = cart1;
        }

        public CartList()
        {
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Source = cartItems;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            dataCart.CustomerName = Name.Text;
            dataCart.CustomerAdress = Adress.Text;
            dataCart.CustomerEmail = Email.Text;
            try
            {
                bl?.Cart.MakeAnOrder(dataCart);             
                Confirmation.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("Succeded","",MessageBoxButton.OK,MessageBoxImage.Information);
            Close();
        }



        private void UpdateAmount_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.OrderItem? p = (BO.OrderItem?)CartListView.SelectedItem;
           // OrderItem? p = (OrderItem?)CartListView.SelectedItem;
            new AmountUpdate(dataCart,p.ProductId, UpdateProduct).Show();
            //Close();
        }


        private void UpdateProduct(Cart c,int id)
        {
            var x = CartListView.SelectedIndex;
           // var y = c.Items.FirstOrDefault(x => x?.ProductId == id);
          // int x = c.Items.IndexOf(y);
            cartItems[x] = c.Items[x]!;
            PriceP = c.TotalPrice;
           // cartItems = new ObservableCollection<BO.OrderItem>(c.Items.ToList()!);
        }


        //private void TextBox_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Enter)
        //    {
        //        BO.ProductItem? p = (BO.ProductItem?)CartListView.SelectedItem;
        //        BO.ProductItem? product = new BO.ProductItem();
        //        product = (sender as Button)?.DataContext as BO.ProductItem;


        //        try
        //        {

        //            dataCart = bl?.Cart.UpdateProductQuantity(dataCart, (int)product?.Id!,product.Amount)!;
        //            MessageBox.Show("de !");
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }
        //    }

        //}
        //private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{

        //}




    }
}

