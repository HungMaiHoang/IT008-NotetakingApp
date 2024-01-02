using Note.Model;
using Note.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Note.ViewModel
{
    public class ChangePasswordVM: ViewModelBase
    {
        private static ChangePasswordVM _instance;
        public static ChangePasswordVM Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ChangePasswordVM();
                }
                return _instance;
            }
        }

        private string _currentPassword;
        public string CurrentPassword
        {
            get { return _currentPassword; }
            set
            {
                _currentPassword = value;
                OnPropertyChanged(nameof(CurrentPassword));
            }
        }
        private string _newPassword;
        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
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

        public ICommand ChangePasswordCommand { get; set; }
        public ChangePasswordVM() {
            ChangePasswordCommand = new RelayCommand(ChangePassword);
        }

        public Action CloseAction { get; set; }
        private async void ChangePassword(object obj)
        {
            // Blank Password
            if (CurrentPassword == null)
            {
                MessageBox.Show("User Password can not be null!");
                return;
            } 
            if (NewPassword == null)
            {
                MessageBox.Show("User New Password can not be null!");
                return;
            } 
            if (ConfirmPassword == null)
            {
                MessageBox.Show("User Confirm Password can not be null!");
                return;
            }

            if(ConfirmPassword != NewPassword)
            {
                MessageBox.Show("Wrong Confirm Password!");
                return;
            }
            if(CurrentPassword==NewPassword)
            {
                MessageBox.Show("New Password must be different from Current Password!");
                return;
            }
            UserModel usermodel = await DataAccess.Instance.GetUserWithUsername(UserHolder.CurUser.UserName);

            // Wrong username
            if (usermodel == null)
            {
                MessageBox.Show("User does not exist!");
                return;
            }

            // Verify password
            if (PasswordHasher.Verify(CurrentPassword, usermodel.Password))
            {
                UserHolder.CreateUser(usermodel);
                string hash = PasswordHasher.Hash(NewPassword);
                UserHolder.CurUser.Password = hash;
                CloseAction?.Invoke();
            }
            else
            {
                MessageBox.Show("Wrong password!");
            }
        }
    }
}
