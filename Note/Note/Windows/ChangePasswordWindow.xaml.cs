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
using System.Windows.Shapes;

namespace Note.Windows
{
    /// <summary>
    /// Interaction logic for RenameWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        public ChangePasswordWindow()
        {
            InitializeComponent();
            DataContext = ChangePasswordVM.Instance;
            ChangePasswordVM.Instance.CloseAction = ()=> Close();
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            (DataContext as ChangePasswordVM).CurrentPassword = (sender as PasswordBox).Password;
        }

        private void txtNewPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            (DataContext as ChangePasswordVM).NewPassword = (sender as PasswordBox).Password;
        }

        private void txtConfirmPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            (DataContext as ChangePasswordVM).ConfirmPassword = (sender as PasswordBox).Password;
        }
    }
}
