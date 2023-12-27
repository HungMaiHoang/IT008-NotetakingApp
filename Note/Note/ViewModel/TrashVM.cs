using Note.Model;
using Note.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public TrashVM() {
            List<NoteModel> listTemp = DataAccess.Instance.GetNoteDisable();
            _listnote= new ObservableCollection<NoteModel>(listTemp);

        }
    }
}
   