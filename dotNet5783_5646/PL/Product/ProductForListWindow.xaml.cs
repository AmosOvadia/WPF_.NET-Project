
using BO;
using PL.Product;
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
    /// Interaction logic for ProductForListWindow.xaml
    /// </summary>
    public partial class ProductForListWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public ProductForListWindow()
        {
            InitializeComponent();

            
            ProductListView.ItemsSource = bl.Product.GetProducts(); // Take all the products from the logical layer and put them in the list
            ProductCategory.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProdactCategory)); //Get the category types from the logical layer
        }

        private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductListView.ItemsSource = bl.Product.GetProducts(a => a?.Category.ToString() == ProductCategory.SelectedItem.ToString());
        }

        private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        //Enter the window of adding a product
        private void Add_New_Product_Click(object sender, RoutedEventArgs e)
        {
            int? temp = null;
            new AddNewProduct(temp).Show();
            Close(); //Close the product list window
        }


        //to update the product
        private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var product = (BO.ProductForList)ProductListView.SelectedItem;
            if ((BO.ProductForList)ProductListView.SelectedItem != null)
            {
                new AddNewProduct(product.Id).Show();
                Close();// Close the product list window
            }          
        }
    }
}
