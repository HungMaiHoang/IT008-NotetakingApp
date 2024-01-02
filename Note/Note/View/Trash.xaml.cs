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

namespace Note.View
{
    /// <summary>
    /// Interaction logic for Trash.xaml
    /// </summary>
    public partial class Trash : UserControl
    {
        public Trash()
        {
            InitializeComponent();
            DataContext = TrashVM.Instance;
        }

        private void ListBox_Loaded(object sender, RoutedEventArgs e)
        {
            TrashVM.Instance.PresentedListBox = sender as ListBox;
        }
    }
}
