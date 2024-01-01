using Note.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Note.Utilities
{
    internal class UserHolder
    {
        private static UserModel _curUser;
        public static UserModel CurUser
        {
            get
            {
                if (_curUser == null)
                {
                    MessageBox.Show("No current user");
                    return null;
                }
                return _curUser;
            }
            set
            {
                _curUser = value;
            }
        }
        public static void CreateUser(UserModel user)
        {
            CurUser = user;
        }
    }
}
