using Note.Model;
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
                _inputUserPassword = value;
                OnPropertyChanged(nameof(InputUserPassword));
            }
        }

        public ICommand LoginCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        private async void Login(object obj)
        {
            // Blank User Name
            if (InputUserName == null)
            {
                MessageBox.Show("User Name can not be null!");
                return;
            }
            // Blank Password
            if (InputUserPassword == null)
            {
                MessageBox.Show("User Password can not be null!");
                return;
            }

            UserModel usermodel = await DataAccess.Instance.GetUserWithUsername(InputUserName);

            // Wrong username
            if (usermodel == null)
            {
                MessageBox.Show("User does not exist!");
                return;
            }

            // Verify password
            if (PasswordHasher.Verify(InputUserPassword, usermodel.Password))
            {
                MainWindow view = new MainWindow(usermodel);
                view.Show();
                myView.Close();
            }
            else
            {
                MessageBox.Show("Wrong password!");
            }
        }
        private void SignUp(object obj)
        {
            SignUpWindow view = new SignUpWindow();
            view.Show();
            myView.Close();
        }

        public LoginWindowVM(LoginWindow view)
        {
            myView = view;

            LoginCommand = new RelayCommand(Login);
            SignUpCommand = new RelayCommand(SignUp);
        }
    }
}
