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
    /// Interaction logic for Product_Order.xaml
    /// </summary>
    public partial class Product_Order : Window
    {
        public Product_Order()
        {
            InitializeComponent();
        }

        private void ToProduct_Click(object sender, RoutedEventArgs e)
        {
            new ProductForListWindow().Show();
            Close(); //Close the main window
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            new OrderForList().Show();
            Close(); //Close the main window
        }
    }
}



//namespace PL
//{
//    /// <summary>
//    /// Interaction logic for Product_Order.xaml
//    /// </summary>
//    public partial class Product_Order : Window
//    {
//        public Product_Order()
//        {
//            InitializeComponent();
//        }

//        private void ToProduct_Click(object sender, RoutedEventArgs e)
//        {
//            //new ProductForListWindow().Show();
//            Close(); //Close the main window
//        }

//        private void Order_Click(object sender, RoutedEventArgs e)
//        {
//            new OrderForList().Show();
//            Close(); //Close the main window
//        }


//    }
//}

