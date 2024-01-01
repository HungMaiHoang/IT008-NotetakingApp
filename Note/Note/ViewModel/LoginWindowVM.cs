using Note.Utilities;
using Note.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Note.ViewModel
{
    internal class LoginWindowVM : ViewModelBase
    {
        private LoginWindow myView;
        private string _inputUserName;
        public string InputUserName 
        { 
            get => _inputUserName;
            set
            {
                _inputUserName = value;
                OnPropertyChanged(nameof(InputUserName));
            }
        }
        private string _inputUserPassword;
        public string InputUserPassword
        {
            get => _inputUserPassword;
            set
            {
                _inputUserName = value;
                OnPropertyChanged(nameof(InputUserPassword));
            }
        }

        public ICommand LoginCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        private void Login(object obj)
        {
            MainWindow view = new MainWindow();
            view.Show();
            myView.Close();
        }
        private void SignUp(object obj)
        {

        }

        public LoginWindowVM(LoginWindow view)
        {
            myView = view;

            LoginCommand = new RelayCommand(Login);
            SignUpCommand = new RelayCommand(SignUp);
        }
    }
}
