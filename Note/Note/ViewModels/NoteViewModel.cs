using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Note.ViewModels
{
    internal class NoteViewModel : BaseViewModel
    {
        public static string Name
        {
            get { return "Note"; }
        }
        public static string IconSource
        {
            get { return "Images/note_pen.png"; }
        }

        public string Content
        {
            get { return "This is Note"; }
            set => Content = value;
        }
    }
}
