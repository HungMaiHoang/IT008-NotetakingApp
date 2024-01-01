using Note.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Note.ViewModel
{
    internal class LoginWindowVM : ViewModelBase
    {
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

        private void Login(object obj)
        {
            
        }

        public LoginWindowVM()
        {
            LoginCommand = new RelayCommand(Login);
        }
    }
}
