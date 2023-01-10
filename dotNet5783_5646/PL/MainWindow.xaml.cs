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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            new Product_Order().Show();
        }

        private void NewOrder_Click(object sender, RoutedEventArgs e)
        {
            new NewOrder().Show();
        }

        private void OrderForMangar_Click(object sender, RoutedEventArgs e)
        {
           new UpdateForManager().Show();

        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int? ID;
                int Temp;
                int.TryParse(TextBox.Text, out Temp);
                ID = Temp;
                try
                {
                    new OrderTracking(ID).Show();
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
