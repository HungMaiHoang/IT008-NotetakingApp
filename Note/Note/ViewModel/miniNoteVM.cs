using Note.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Note.ViewModel
{
    internal class miniNoteVM : ViewModelBase
    {
        private string _title;
        private DateTime _date;
        private string _headLine;

        public string Title { get => _title; set { _title = value; OnPropertyChanged(nameof(Title)); } }
        public DateTime Date { get => _date; set { _date = value; OnPropertyChanged(nameof(Date)); } }
        public string Headline { get => _headLine; set { _headLine = value; OnPropertyChanged(nameof(Headline)); } }
    }
}
