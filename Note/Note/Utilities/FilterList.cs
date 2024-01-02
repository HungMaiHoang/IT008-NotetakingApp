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
        public static ObservableCollection<NoteModel> AscedingLastEdit(List<NoteModel> list)
        {
            return new ObservableCollection<NoteModel>(list.OrderBy(x => x.LastEdited).ToList());
        }
        public static ObservableCollection<NoteModel> DescendingLastEdit(List<NoteModel> list)
        {
            return new ObservableCollection<NoteModel>(list.OrderByDescending(x => x.LastEdited).ToList());
        }
        public static ObservableCollection<NoteModel> AscedingTitle(List<NoteModel> list)
        {
            return new ObservableCollection<NoteModel>(list.OrderBy(x => x.Title).ToList());
        }
        public static ObservableCollection<NoteModel> DescendingTitle(List<NoteModel> list)
        {
            return new ObservableCollection<NoteModel>(list.OrderByDescending(x => x.Title).ToList());
        }
    }
}
