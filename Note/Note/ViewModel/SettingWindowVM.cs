using MongoDB.Driver;
using Note.Utilities;
using Note.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Note.ViewModel
{
    public class SettingWindowVM : ViewModelBase
    {
        private static SettingWindowVM _instance;
        public static SettingWindowVM Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SettingWindowVM();
                }
                return _instance;
            }
        }
        private bool _isCheckAddFirst;
        public bool IsCheckAddFirst
        {
            get { return _isCheckAddFirst; }
            set
            {
                _isCheckAddFirst = value;
                UserHolder.CurUser.SettingAdd = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> listDayDelete = new ObservableCollection<string>() { "3", "7", "14", "21", "30" };
        public ObservableCollection<string> ListDayDelete {
            get { return listDayDelete; }
            set { ListDayDelete = value; OnPropertyChanged();}
        }
                
        private string dayDelete;
        public string DayDelete
        {
            get { return dayDelete; }
            set { dayDelete = value;
                UserHolder.CurUser.DayDelete = value;
                //DataAccess.Instance.UpdateUser(UserHolder.CurUser);
            OnPropertyChanged(nameof(DayDelete));}
        }

        public ICommand ChangeAvatarCommand {  get; set; }
        public ICommand SignOutCommand { get; set; }    
        public ICommand ChangePasswordCommand { get; set; }
        public Action CloseAction {  get; set; }
        private string pathAvatar;
        public string PathAvatar
        {
            get { return pathAvatar; }
            set { pathAvatar = value;
            OnPropertyChanged();}
        }
        public SettingWindowVM()
        {
            _isCheckAddFirst = UserHolder.CurUser.SettingAdd;
            dayDelete = UserHolder.CurUser.DayDelete;
            ChangeAvatarCommand = new RelayCommand(ChangeAvatar);
            SignOutCommand = new RelayCommand(SignOut);
            ChangePasswordCommand = new RelayCommand(ChangePassword);
        }

        private void ChangePassword(object obj)
        {
            ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow();
            changePasswordWindow.ShowDialog();
        }

        private void SignOut(object obj)
        {
            System.Windows.Forms.Application.Restart();
            CloseAction.Invoke();
        }

        private void ChangeAvatar(object obj)
        {
            var ofd = new OpenFileDialog();
            var result = ofd.ShowDialog();
            if (result == DialogResult.OK) {
                pathAvatar = ofd.FileName;
            }
        }
    }
}
