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
using static System.Collections.Specialized.BitVector32;

namespace PL
{
    /// <summary>
    /// Interaction logic for AmountUpdate.xaml
    /// </summary>
    public partial class AmountUpdate : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<BO.OrderItem> _cartItems;
        public ObservableCollection<BO.OrderItem> cartItems
        {
            get { return _cartItems; }
            set
            {
                _cartItems = value;
                OnPropertyChanged(nameof(cartItems));
            }
        }

        private Action<Cart,int> Action;










        int ID;
        BO.Cart cart = new BO.Cart();
        public AmountUpdate(Cart c,int id,Action<Cart,int> active)
        {
            
            InitializeComponent();
            this.Action = active;
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
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                Close();
                Action?.Invoke(cart, ID);

            }

        }




            //private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
            //{

            //}

        }
}
