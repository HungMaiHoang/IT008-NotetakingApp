using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using MongoDB.Driver;
using Note.Model;
using Note.Utilities;
using Note.View;
namespace Note.ViewModel
{
    class NoteVM : ViewModelBase
    {
        // Singleton
        private static NoteVM _instance;
        public static NoteVM Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NoteVM();
                }
                return _instance;
            }
        }

        private NoteModel _curNote;
        public NoteModel CurNote
        {
            get => _curNote;
            set 
            { 
                _curNote = value;
                // Update note to database
                DataAccess.Instance.UpdateNote(CurNote);
                OnPropertyChanged(nameof(CurNote)); 
            }
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

        private string _pageTitle;
        public string PageTitle 
        { 
            get => _pageTitle;
            set
            {
                _pageTitle = value;
                CurNote.Title = value;
                OnPropertyChanged(nameof(PageTitle));
            }
        }

        private FlowDocument _pageContent;
        public FlowDocument PageContent
        {
            get => _pageContent;
            set
            {
                _pageContent = value;
                OnPropertyChanged(nameof(PageContent));
            }
        }

        public ICommand LoadPageCommand { get; set; }
        public ICommand TestCommand { get; set; }

        private void LoadPage(object obj)
        {
            try
            {
                CurNote = (obj as NoteModel);
                PageTitle = CurNote.Title;

                //TextRange temp = new TextRange(PageContent.ContentStart, PageContent.ContentEnd);
                //DataAccess.Instance.LoadRTFNote(CurNote.Id, temp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Test(object obj)
        {
            MessageBox.Show(PageContent);
        }

        private NoteVM()
        {

            // Load Notes from databse
            List<NoteModel> listTemp = DataAccess.Instance.GetAllNotes();
            ListNote = new ObservableCollection<NoteModel>(listTemp);

            LoadPageCommand = new RelayCommand(LoadPage);
            TestCommand = new RelayCommand(Test);
        }
        /// <summary>
        /// Select first note if has
        /// </summary>
        public void SelectFirstNoteIfHas()
        {
            if (ListNote.Count > 0)
            {
                CurNote = ListNote.First();
            }
        }
    }
}
