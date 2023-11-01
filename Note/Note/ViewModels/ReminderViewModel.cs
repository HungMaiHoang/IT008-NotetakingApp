using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.ViewModels
{
    internal class ReminderViewModel : BaseViewModel
    {
        public static string Name
        {
            get { return "Reminder"; }
        }
        public static string IconSource
        {
            get { return "Images/bell.png"; }
        }
        public string Content
        {
            get { return "This is Reminder"; }
            set => Content = value;
        }
    }
}
