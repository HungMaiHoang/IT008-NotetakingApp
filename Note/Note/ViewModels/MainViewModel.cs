using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {       
        public ObservableCollection<BaseViewModel> ViewDetail { get; private set; }

        public MainViewModel()
        {
            ViewDetail = new ObservableCollection<BaseViewModel>()
            {
                new NoteViewModel(),
                new ReminderViewModel()
            };
        }
    }
}
