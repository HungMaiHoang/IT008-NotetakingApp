using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Note.Utilities;
using System.Windows.Input;
namespace Note.ViewModel
{
    internal class NavigationVM : ViewModelBase
    {
        // Singleton
        private static NavigationVM _instance;
        public static NavigationVM Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NavigationVM();
                }
                return _instance;
            }
        }
        public NavigationVM()
        {
            
        }
    }
}
