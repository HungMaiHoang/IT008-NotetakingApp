using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Note.Model;
using Note.Utilities;
namespace Note.ViewModel
{
    class NoteVM : Utilities.ViewModelBase
    {
        private NoteModel _curNote;
        public NoteModel CurNote
        {
            get => _curNote;
            set { CurNote = value; OnPropertyChanged(nameof(CurNote)); }
        }

        private ObservableCollection<NoteModel> _listNote;
        public ObservableCollection<NoteModel> ListNote 
        { 
            get => _listNote;
            set
            {
                _listNote = value;
                OnPropertyChanged(nameof(ListNote));
            }
        }

        //public ICommand PageCommand { get; set; }
        //private void Page(object obj) => CurNote = new PageVM();

        public NoteVM()
        {
            List<NoteModel> listTemp = DataAccess.Instance.GetAllNotes();
            ListNote = new ObservableCollection<NoteModel>(listTemp);
        }
    }
}
