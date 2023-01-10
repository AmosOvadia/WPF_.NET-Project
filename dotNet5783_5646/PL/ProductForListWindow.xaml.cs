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
        /// Interaction logic for ProductForListWindow.xaml
        /// </summary>
        public partial class ProductForListWindow : Window
        {
            BlApi.IBl? bl = BlApi.Factory.Get();
            public List<BO.ProductForList?> productForList = new List<ProductForList?> ();

            public ProductForListWindow()
            {
                InitializeComponent();

                productForList = bl?.Product.GetProducts().ToList()!; // Take all the products from the logical layer and put them in the list
            //Get the category types from the logical layer
            for (int i = 0; i < 3; i++)
            {
                ProductCategory.Items.Add($"{(BO.Enums.ProdactCategory)i}");
            }
            ProductCategory.Items.Add("All");

        }

            private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                //ProductListView.ItemsSource = bl?.Product.GetProducts(a => a?.Category.ToString() == ProductCategory.SelectedItem.ToString());

                if (ProductCategory.SelectedItem.ToString() != "All")
                {
                    ProductListView.ItemsSource = bl?.Product.GetProductItems(a => a?.Category.ToString() == ProductCategory.SelectedItem.ToString());
                }
                else
                {
                    ProductListView.ItemsSource = bl?.Product.GetProductItems();
                }

            }

        private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {


            }

            //Enter the window of adding a product
            private void Add_New_Product_Click(object sender, RoutedEventArgs e)
            {
                int? temp = null;
                new AddNewProduct(temp, false).Show();
                Close(); //Close the product list window
            }


            //to update the product
            private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
                var product = (BO.ProductItem?)ProductListView.SelectedItem;
                if ((BO.ProductItem?)ProductListView.SelectedItem != null)
                {
                    new AddNewProduct(product.Id, false).Show();
                    Close();// Close the product list window
                }
            }
        }
    }