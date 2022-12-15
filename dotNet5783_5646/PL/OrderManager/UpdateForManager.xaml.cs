
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

namespace PL.OrderManager
{
    /// <summary>
    /// Interaction logic for UpdateForManager.xaml
    /// </summary>
    public partial class UpdateForManager : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public UpdateForManager()
        {
            InitializeComponent();
            OrderId.Visibility = Visibility.Hidden;
            LabelOrderId.Visibility = Visibility.Hidden;

            ProductId.Visibility = Visibility.Hidden;
            LabelPruductId.Visibility = Visibility.Hidden;

            Amount.Visibility = Visibility.Hidden;
            LabelAmount.Visibility = Visibility.Hidden;

            UpdateForManager1.Visibility = Visibility.Hidden;
        }

        private void Password_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CheckPassword_Click(object sender, RoutedEventArgs e)
        {
            int temp;
            int.TryParse(Password.Text, out temp);

            if (temp == 6656545)
            {
                Password.Visibility = Visibility.Hidden;
                LabelPassword.Visibility = Visibility.Hidden;
                CheckPassword.Visibility = Visibility.Hidden;

                OrderId.Visibility = Visibility.Visible;
                LabelOrderId.Visibility = Visibility.Visible;

                ProductId.Visibility = Visibility.Visible;
                LabelPruductId.Visibility = Visibility.Visible;

                Amount.Visibility = Visibility.Visible;
                LabelAmount.Visibility = Visibility.Visible;

                UpdateForManager1.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void UpdateForManager1_Click(object sender, RoutedEventArgs e)
        {
            int temp1, temp2,temp3;

            int.TryParse(OrderId.Text, out temp1);
            int.TryParse(ProductId.Text, out temp2);
            int.TryParse(Amount.Text, out temp3);

            try
            {
                bl.Order.UpdateOrederManager(temp1, temp2, temp3);
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
            catch (DO.TheIdentityCardDoesNotExistInTheDatabase ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
