using Note.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.ViewModel
{
    public class SearchVM:ViewModelBase
    {
        private static SearchVM _instance;
        public static SearchVM Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SearchVM();
                }
                return _instance;
            }
        }
    }
}
