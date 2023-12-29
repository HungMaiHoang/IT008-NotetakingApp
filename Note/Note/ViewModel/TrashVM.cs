using Note.Model;
using Note.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Note.ViewModel
{
    public class TrashVM :ViewModelBase
    {
        private static TrashVM _instance;
        public static TrashVM Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TrashVM();
                }
                return _instance;
            }
        }


        private ObservableCollection<NoteModel> _listnote;
        public ObservableCollection<NoteModel> ListNote 
        { 
            get => _listnote;
            set 
            {
                _listnote = value;
                OnPropertyChanged(nameof(ListNote));
            } 
        }
        private NoteModel _curMote;
        public NoteModel CurNote
        {
            get { return _curMote; } set { _curMote = value; OnPropertyChanged(nameof(CurNote)); }
        }
        public ICommand RestoreCommand { get; set; }
        public ICommand DeleteForeverCommand { get; set; }
        public TrashVM() {
            List<NoteModel> listTemp = DataAccess.Instance.GetNoteDisable();
            _listnote= new ObservableCollection<NoteModel>(listTemp);
            RestoreCommand = new RelayCommand(Restore);
            DeleteForeverCommand = new RelayCommand(DeleteForever);

        }
        private void Restore(object obj)
        {
            CurNote = obj as NoteModel;
            DataAccess.Instance.RestoreNote(CurNote);
            ListNote.Remove(CurNote);
        }
        private void DeleteForever(object obj)
        {
            CurNote = obj as NoteModel;
            DataAccess.Instance.DeleteNote(CurNote.Id);
            ListNote.Remove(CurNote);
        }
    }
}
   