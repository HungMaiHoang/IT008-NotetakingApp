using Note.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Utilities
{
    internal class FilterList
    {
        public static ObservableCollection<NoteModel> AscedingLastEdit(ObservableCollection<NoteModel> list)
        {
            return new ObservableCollection<NoteModel>(list.OrderBy(x => x.LastEdited).ToList());
        }
        public static ObservableCollection<NoteModel> DescendingLastEdit(ObservableCollection<NoteModel> list)
        {
            return new ObservableCollection<NoteModel>(list.OrderByDescending(x => x.LastEdited).ToList());
        }
        public static ObservableCollection<NoteModel> AscedingTitle(ObservableCollection<NoteModel> list)
        {
            return new ObservableCollection<NoteModel>(list.OrderBy(x => x.Title).ToList());
        }
        public static ObservableCollection<NoteModel> DescendingTitle(ObservableCollection<NoteModel> list)
        {
            return new ObservableCollection<NoteModel>(list.OrderByDescending(x => x.Title).ToList());
        }
    }
}
