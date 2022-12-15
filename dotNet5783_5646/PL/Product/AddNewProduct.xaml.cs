
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using BO;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for AddNewProduct.xaml
    /// </summary>
    public partial class AddNewProduct : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public AddNewProduct(int? id)
        {
            InitializeComponent();
            CategoryNewProduct.ItemsSource = Enum.GetValues(typeof(BO.Enums.ProdactCategory));
            if (id != null)
            {
                Add.Visibility = Visibility.Hidden;
                IdNewProduct.Text = id.ToString();
            }
            else
            {
                UpdateProduct.Visibility = Visibility.Hidden;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            int temp = 0;
            BO.Product product = new BO.Product();
            int.TryParse(IdNewProduct.Text, out temp);
            product.Id = temp;
            product.Name = NameNewProduct.Text;
            int.TryParse(PriceNewProduct.Text, out temp);
            product.Price = temp;
            product.Category = (BO.Enums.ProdactCategory?)CategoryNewProduct.SelectedItem;
            int.TryParse(InStockNewProduct.Text, out temp);
            product.InStock = temp;
            try
            {
                bl.Product.Add(product);
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
            int temp = 0;
            BO.Product product1 = new BO.Product();
            int.TryParse(IdNewProduct.Text, out temp);
            product1.Id = temp;
            product1.Name = NameNewProduct.Text;
            int.TryParse(PriceNewProduct.Text, out temp);
            product1.Price = temp;
            product1.Category = (BO.Enums.ProdactCategory?)CategoryNewProduct.SelectedItem;
            int.TryParse(InStockNewProduct.Text, out temp);
            product1.InStock = temp;
            try
            {
                bl.Product.Update(product1);
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
    }
    
}
