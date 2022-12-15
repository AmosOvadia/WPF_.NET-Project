using PL.OrderManager;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //button to see the list of products, and the option to add or update them
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ProductForListWindow().Show();
            Close(); //Close the main window
        }

        //Admin login
        private void For_Manager_Click(object sender, RoutedEventArgs e)
        {
            new UpdateForManager().Show();
            Close(); //Close the main window

        }
    }
}
