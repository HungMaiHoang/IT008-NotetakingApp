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
    public class ArchivedVM : ViewModelBase
    {
        private static ArchivedVM _instance;
        public static ArchivedVM Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ArchivedVM();
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
        private ListBox _presentedListBox;
        public ListBox PresentedListBox
        {
            get => _presentedListBox;
            set
            {
                _presentedListBox = value;
                OnPropertyChanged(nameof(PresentedListBox));
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
        public ICommand LoadPageCommand { get; set; }
        public ArchivedVM()
        {
            List<NoteModel> listTemp = DataAccess.Instance.GetNoteArchived(UserHolder.CurUser);
            _listnote = new ObservableCollection<NoteModel>(listTemp);
            RestoreCommand = new RelayCommand(Restore);
            LoadPageCommand = new RelayCommand(LoadPage);
        }

        private async void LoadPage(object obj)
        {

            try
            {
                DetailNoteArchivedWindow detailNoteArchivedWindow = new DetailNoteArchivedWindow();
                CurNote = obj as NoteModel;
                // Load Content
                PageContent.Document = await DataAccess.Instance.LoadRTFNote(CurNote.FileId);
                detailNoteArchivedWindow.Show();
            }
            catch (Exception ex)
            {
            }

        }
        private async void Restore(object obj)
        {
            CurNote = obj as NoteModel;
            await DataAccess.Instance.RestoreNote(CurNote);
            ListNote.Remove(CurNote);
        }
    }
}
