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
    internal class SearchVM : ViewModelBase
    {
        // Singleton
        private SearchVM _instance;
        public SearchVM Instance
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

        private ObservableCollection<NoteModel> _notesList;
        public ObservableCollection<NoteModel> NotesList 
        { 
            get => _notesList;
            set
            {
                _notesList = value;
                OnPropertyChanged(nameof(NotesList));
            }
        }

        private ObservableCollection<NoteModel> _trashList;
        public ObservableCollection<NoteModel> TrashList 
        { 
            get => _trashList;
            set
            { 
                _trashList = value;
                OnPropertyChanged(nameof(TrashList));
            }
        }

        private ObservableCollection<NoteModel> _archivedList;
        public ObservableCollection<NoteModel> ArchivedList 
        { 
            get => _archivedList;
            set
            {
                _archivedList = value;
                OnPropertyChanged(nameof(ArchivedList));
            }
        }

        private SearchVM()
        {
        }
    }
}
