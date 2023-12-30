using MaterialDesignThemes.Wpf;
using Note.Model;
using Note.Utilities;
using Note.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Note.ViewModel
{
    public class TrashVM : ViewModelBase
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
            get { return _curMote; }
            set { _curMote = value; OnPropertyChanged(nameof(CurNote)); }
        }

        // Content from RichTextBox
        private RichTextBox _pageContent;
        public RichTextBox PageContent
        {
            get => _pageContent;
            set
            {
                _pageContent = value;
                OnPropertyChanged(nameof(PageContent));
            }
        }
        public ICommand RestoreCommand { get; set; }
        public ICommand DeleteForeverCommand { get; set; }
        public ICommand LoadPageCommand { get; set; }
        public ICommand ClearTrashCommand { get; set; }
        public TrashVM()
        {
            List<NoteModel> listTemp = DataAccess.Instance.GetNoteDisable();
            _listnote = new ObservableCollection<NoteModel>(listTemp);
            RestoreCommand = new RelayCommand(Restore);
            DeleteForeverCommand = new RelayCommand(DeleteForever);
            LoadPageCommand = new RelayCommand(LoadPage);
            ClearTrashCommand = new RelayCommand(ClearTrash);
        }

        private void ClearTrash(object obj)
        {
            foreach (var item in ListNote)
            {
                DataAccess.Instance.DeleteNote(item.Id);
            }
            ListNote.Clear();
        }

        private void LoadPage(object obj)
        {

            try
            {
                DetailNoteTrashWindow detailNoteTrashWindow = new DetailNoteTrashWindow();
                CurNote = obj as NoteModel;
                // Load Content
                PageContent.Document = DataAccess.Instance.LoadRTFNote(CurNote.FileId);
                detailNoteTrashWindow.Show();
            }
            catch (Exception ex)
            {
            }

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
