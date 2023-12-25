using Note.ViewModel;
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

namespace Note
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {            
            InitializeComponent();
            DataContext = new MainWindowVM(this);
        }

        private void btn_menutab_expanded_Menu_Click(object sender, RoutedEventArgs e)
        {
            expandedMenu.Visibility = Visibility.Collapsed;
            collapseMenu.Visibility = Visibility.Visible;
        }

        private void btn_menutab_collapsed_Menu_Click(object sender, RoutedEventArgs e)
        {
            collapseMenu.Visibility = Visibility.Collapsed;
            expandedMenu.Visibility = Visibility.Visible;
        }
    }
}
