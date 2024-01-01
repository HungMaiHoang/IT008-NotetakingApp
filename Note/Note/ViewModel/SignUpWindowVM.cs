using Note.Model;
using Note.Utilities;
using Note.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Note.ViewModel
{
    internal class SignUpWindowVM : ViewModelBase
    {
        private SignUpWindow myView;
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }
        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }
        public ICommand SignUpCommand {  get; set; }
        public ICommand LoginCommand { get; set; }
        private async void SignUp(object obj)
        {
            if (Name != null && UserName != null && Password != null && ConfirmPassword != null)
            {
                if (Password.Equals(ConfirmPassword))
                {
                    if (await DataAccess.Instance.NotExistUserWithUsername(UserName))
                    {
                        string hash = PasswordHasher.Hash(Password);
                        UserModel usermodel = UserModel.CreateNewUser(Name, UserName, hash);
                        await DataAccess.Instance.UpdateUser(usermodel);
                    }
                    else
                    {
                        MessageBox.Show("This user name already exist!");
                    }
                }
                else
                {
                    MessageBox.Show("Password and Confirm Password must be the same!");
                }
            }
            else
            {
                MessageBox.Show("Information can not be null!");
            }
            
        }
        private void Login(object obj)
        {
            LoginWindow view = new LoginWindow();
            view.Show();
            myView.Close();
        }
        public SignUpWindowVM(SignUpWindow view)
        {
            myView = view;

            SignUpCommand = new RelayCommand(SignUp);
            LoginCommand = new RelayCommand(Login);
        }
    }
}
