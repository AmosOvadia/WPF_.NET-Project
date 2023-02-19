using BO;
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
        /// Interaction logic for ProductForListWindow.xaml
        /// </summary>
        public partial class ProductForListWindow : Window
        {
            BlApi.IBl? bl = BlApi.Factory.Get();
        public ObservableCollection<BO.ProductForList?> products
        {
            get { return (ObservableCollection<BO.ProductForList?>)GetValue(productsProperty); }
            set { SetValue(productsProperty, value); }
        }
        public static readonly DependencyProperty productsProperty = DependencyProperty.Register(
        "products", typeof(ObservableCollection<BO.ProductForList?>), typeof(ProductForListWindow), new PropertyMetadata(default(List<BO.ProductForList?>)));

        public ProductForListWindow()
        {
            products = new ObservableCollection<BO.ProductForList?>(bl?.Product.GetProducts().ToList()!); // Take all the products from the logical layer and put them in the list

            InitializeComponent();

            //Get the category types from the logical layer
            for (int i = 0; i < 3; i++)
            {
                ProductCategory.Items.Add($"{(BO.Enums.ProdactCategory)i}");
            }
            ProductCategory.Items.Add("All");

        }

            private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
            if (ProductCategory.SelectedItem.ToString() != "All")
                {

                products = new ObservableCollection<BO.ProductForList?>(bl?.Product.GetProducts().Where(a => a?.Category.ToString() == ProductCategory.SelectedItem.ToString()).ToList())!;//bl?.Product.GetProducts(a => a?.Category.ToString() == ProductCategory.SelectedItem.ToString());
            }
                else
                {
                products = new ObservableCollection<BO.ProductForList?>(bl?.Product.GetProducts()!);
            }

        }

        private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {


            }

            //Enter the window of adding a product
            private void Add_New_Product_Click(object sender, RoutedEventArgs e)
            {
                int? temp = null;
                new AddNewProduct(temp, false,addToProducts).Show();
               // Close(); //Close the product list window
            }


            //to update the product
            private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
                if ((BO.ProductForList?)ProductListView.SelectedItem != null)
                {
                var product = (BO.ProductForList?)ProductListView.SelectedItem;
               
                new AddNewProduct(product.Id, false,UpdateToProducts).Show();
                   // Close();// Close the product list window
                }
            }


        private void UpdateToProducts(int productID)
        {
            var x = ProductListView.SelectedIndex;
            products[x] = (bl?.Product.GetProducts(a => a?.Id == productID).First());
        }


        private void addToProducts(int productID)
        {
            BO.ProductForList? p = (bl?.Product.GetProducts(a => a?.Id == productID)!).First();
            products.Add(p);
        }


    }
}