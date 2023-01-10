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
    /// Interaction logic for AmountUpdate.xaml
    /// </summary>
    public partial class AmountUpdate : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        int ID;
        BO.Cart cart = new BO.Cart();
        public AmountUpdate(Cart c,int id)
        {
            
            InitializeComponent();
            ID = id;
            cart = c;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

               // var product = (BO.ProductItem)NewOrderListView.SelectedItem;
                try
                {
                    int temp;
                    int.TryParse(TextBox.Text, out temp);
                    cart = bl?.Cart.UpdateProductQuantity(cart, ID, temp)!;
                    //MessageBox.Show("de !");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Close();
            }
           
        }




            //private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
            //{

            //}

        }
}
