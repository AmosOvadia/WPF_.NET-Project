using BO;
using DO;
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
    /// Interaction logic for AddNewProduct.xaml
    /// </summary>
    public partial class AddNewProduct : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public BO.Product? product
        {
            get { return (BO.Product)GetValue(productProperty); }
            set { SetValue(productProperty, value); }
        }
        public static readonly DependencyProperty productProperty = DependencyProperty.Register(
        "product", typeof(BO.Product), typeof(AddNewProduct), new PropertyMetadata(default(BO.Product)));


        BO.Cart dataCart = new BO.Cart();

        int? ID = 0;
        public AddNewProduct(int? id, bool b)
        {
            ID = id;
            product = new();
            InitializeComponent();
            CategoryNewProduct.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProdactCategory));
            if (b == false)
            {
                BackToNewOrder.Visibility = Visibility.Hidden; 
                if (id != null)
                {
                    Add.Visibility = Visibility.Hidden;
                    //DataContext = bl.Product.GetProductById((int)id);
                }
                else
                {
                    UpdateProduct.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                BackProductForList.Visibility = Visibility.Hidden;
                Add.Visibility = Visibility.Hidden;
                UpdateProduct.Visibility = Visibility.Hidden;
            }
            if (id != null)
                product = bl?.Product.GetProductById((int)id);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
          
            try
            {
                bl?.Product.Add(product);
                check = true;
            }
            catch (BO.TheIDAlreadyExistsInTheDatabase ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.TheVariableIsLessThanTheNumberZero ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.OutOfStock ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.InputError ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.VariableIsNull ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (check == true)
            {
                Add.Visibility = Visibility.Hidden;
            }
        }


        private void BackToMain_Click(object sender, RoutedEventArgs e)
        {
            // new PL.Product..Show();

            new ProductForListWindow().Show();
            Close();
        }

        private void IdNewProduct_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void Update_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
        
            try
            {
                bl?.Product.Update(product);
                check = true;
            }
            catch (BO.TheIDAlreadyExistsInTheDatabase ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.TheVariableIsLessThanTheNumberZero ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.OutOfStock ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.InputError ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.VariableIsNull ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (check == true)
            {
                UpdateProduct.Visibility = Visibility.Hidden;
            }


        }

        private void InStockNewProduct_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BackToNewOrder_Click(object sender, RoutedEventArgs e)
        {
           // new NewOrder().Show();
            Close();
        }

      
    }
}








//namespace PL.UserWindows.CartAndProduct
//{
//    /// <summary>
//    /// Interaction logic for Product.xaml
//    /// </summary>
//    public partial class Product : Window
//    {
//        BlApi.IBl? bl = BlApi.Factory.Get();

//        public Product()
//        {
//            InitializeComponent();
//        }

//        BO.Cart dataCart = new BO.Cart();
//        BO.ProductItem dataProductItem = new BO.ProductItem();

//        public Product(ProductItem? ProductContent, BO.Cart cart, bool IsUptdat)
//        {
//            InitializeComponent();
//            productTextBlock.Text = ProductContent.ToString();


//            if (IsUptdat)
//            {
//                TextBoxValue.Text = ProductContent.Amount.ToString();
//            }
//            dataCart = cart;
//            dataProductItem = ProductContent;
//        }

//        private void ButtonBack_Click(object sender, RoutedEventArgs e)
//        {
//            this.Close();
//        }

//        private void ButtonConfirma_Click(object sender, RoutedEventArgs e)
//        {
//            if (string.IsNullOrEmpty(TextBoxValue.Text))
//            {
//                MessageBox.Show("ERROR No number entered");
//                return;
//            }

//            bl.Cart.AddProduct(dataCart, dataProductItem.ID);
//            bl.Cart.UpdateProductQuantity(dataCart, dataProductItem.ID, int.Parse(TextBoxValue.Text));

//            //MessageBox.Show(dataCart.ToString());

//            this.Close();
//        }
//    }
//}