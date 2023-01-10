using BO;
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
    /// Interaction logic for NewOrder.xaml
    /// </summary>
    public partial class NewOrder : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Cart cart = new Cart();


        public List<BO.ProductItem?> productItem = new List<ProductItem?>();

        public NewOrder()
        {
           
            InitializeComponent();
            productItem = bl?.Product.GetProductItems().ToList()!;

            //  DataContext = bl?.Product.GetProductItems();
            //Category.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProdactCategory)); //Get the category types from the logical layer
            for (int i = 0; i < 3; i++)
            {
                Category.Items.Add($"{(BO.Enums.ProdactCategory)i}");
            }
            Category.Items.Add("All");
        }

        private void NewOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //new AddNewProduct()
        }

        private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //NewOrderListView.ItemsSource = bl?.Product.GetProductItems(a => a?.Category.ToString() == Category.SelectedItem.ToString());
            


           // BlApi.IBl? bl = BlApi.Factory.Get();
            if (Category.SelectedItem.ToString() != "All")
            {
                NewOrderListView.ItemsSource = bl?.Product.GetProductItems(a => a?.Category.ToString() == Category.SelectedItem.ToString());
            }
            else
            {
                NewOrderListView.ItemsSource = bl?.Product.GetProductItems();
            }


        }

        private void NewOrderListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var ww = (BO.ProductItem)NewOrderListView.SelectedItem;
            var productWindow = new AddNewProduct(ww.Id, true);
            productWindow.ShowDialog();
            //// BO.Order order = bl.Order.GetOrder(ww.Id);
            //{
            //    new AddNewProduct(ww.Id,cart, true);
            //    //new OrderDetails(order).Show();
            //    Close();// Close the product list window
            //}
        }

        private void ToCartList_Click(object sender, RoutedEventArgs e)
        {
            new CartList(cart).Show();
            // Close();
        }



        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //BO.ProductItem product = new BO.ProductItem();
            //product = (ProductItem?)ProductItemListView.SelectedItem!;
            BO.ProductItem? product = new BO.ProductItem();
            product = (sender as Button)?.DataContext as BO.ProductItem;
            try
            {
             //   product!.Amount++;
                cart = bl?.Cart.Add(cart, (int)product?.Id!)!;
                MessageBox.Show("Added !");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private void Button_Click_3(object sender, RoutedEventArgs e)
        //{
        //    BO.ProductItem? product = new BO.ProductItem();
        //    product = (sender as Button)?.DataContext as BO.ProductItem;
        //    try
        //    {
                 
        //        cart = bl?.Cart.UpdateProductQuantity(cart, (int)product?.Id!, product!.Amount--)!;
        //        MessageBox.Show("de !");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }

        //}





        //private void TextBox_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Enter)
        //    {
                
        //        var product = (BO.ProductItem)NewOrderListView.SelectedItem;
        //        try
        //        {
                    
        //            cart = bl?.Cart.UpdateProductQuantity(cart, (int)product?.Id!, 1)!;
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









//namespace PL
//{
//    /// <summary>
//    /// Interaction logic for NewOrder.xaml
//    /// </summary>
//    public partial class NewOrder : Window
//    {
//        BlApi.IBl? bl = BlApi.Factory.Get();
//        public NewOrder()
//        {
//            InitializeComponent();
//            NewOrderListView.ItemsSource = bl?.Product.GetProductItems();
//            Category.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProdactCategory)); //Get the category types from the logical layer
//        }
//        private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
//        {
//            NewOrderListView.ItemsSource = bl?.Product.GetProductItems(a => a?.Category.ToString() == Category.SelectedItem.ToString());
//        }
//        private void NewOrderListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
//        {
//            var ww = (BO.ProductItem)NewOrderListView.SelectedItem;
//            // BO.Order order = bl.Order.GetOrder(ww.Id);
//            {
//               // new AddNewProduct(ww.Id, true);
//                //new OrderDetails(order).Show();
//                Close();// Close the product list window
//            }
//        }
//    }
//}