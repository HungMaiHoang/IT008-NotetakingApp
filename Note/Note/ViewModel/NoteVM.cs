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
using System.Windows.Markup;
using System.Windows.Media;
using MongoDB.Bson;
using MongoDB.Driver;
using Note.Model;
using Note.Utilities;
using Note.View;
using Note.Windows;
namespace Note.ViewModel
{
    public class NoteVM : ViewModelBase
    {
        private Notes view;
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
        // Current Note in NoteVM
        private NoteModel _curNote;
        public NoteModel CurNote
        {
            get => _curNote;
            set
            {
                // Update note to database before change the value
                if (CurNote is NoteModel && CurNote != null)
                {
                    DataAccess.Instance.UpdateRTFNote(CurNote.FileId, PageContent.Document);
                    DataAccess.Instance.UpdateNote(CurNote);
                }

                _curNote = value;
                OnPropertyChanged(nameof(CurNote));
            }
        }
        // Current List Note in NoteVM
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

        #region Page Things
        // Note Title
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
        // Plain Text from Page Content
        private string _plainText;
        public string PlainText
        {
            get { return _plainText; }
            set
            {
                if (_plainText != value)
                {
                    _plainText = value;
                    OnPropertyChanged(nameof(PlainText));
                }
            }
        }
        #endregion

        #region Word Counting
        private WordCounterModel wordCounterModel;
        private int wordCount;
        public int WordCount
        {
            get { return wordCount; }
            set
            {
                if (wordCount != value)
                {
                    wordCount = value;
                    OnPropertyChanged(nameof(WordCount));
                }
            }
        }
        #endregion

        #region Command
        public ICommand LoadPageCommand { get; set; }
        public ICommand SavePageCommand { get; set; }
        public ICommand DeleteNoteCommand { get; set; }
        public ICommand NoteToTrashCommand { get; set; }
        public ICommand ShowInsertTableWindowCommand { get; set; }
        public ICommand WordCountCommand { get; }
        public ICommand TestCommand { get; set; }
        public ICommand NoteToArchivedCommand {  get; set; }

        private void LoadPage(object obj)
        {
            try
            {
                CurNote = obj as NoteModel;
                PageTitle = CurNote.Title;

                // Load Content
                PageContent.Document = DataAccess.Instance.LoadRTFNote(CurNote.FileId);
            }
            catch (Exception ex)
            {
            }
        }
        private void SavePage(object obj)
        {
            DataAccess.Instance.UpdateRTFNote(CurNote.FileId, PageContent.Document);
            CurNote.LastEdited = DateTime.Now;

        }
        private void DeleteNote(object obj)
        {
            if (CurNote is NoteModel && CurNote != null)
            {
                //DataAccess.Instance.DeleteNote(CurNote.Id);
                NoteToTrash(null);
                //ListNote.Remove(CurNote);
            }
        }
        private void NoteToTrash(object obj)
        {            
            DataAccess.Instance.NoteToTrash(CurNote);
            
            ListNote.Remove(CurNote);
        }
        private void NoteToArchived(object obj)
        {
            DataAccess.Instance.NoteToArchived(CurNote);
            ListNote.Remove(CurNote);
        }
        private void ShowInsertTAbleWindow(object obj)
        {
            InsertTableWindow insertTableWindow = new InsertTableWindow();
            insertTableWindow.ShowDialog();
        }
        private bool CanShowWindow(object obj)
        {
            return true;
        }
        private void UpdateWordCount(object obj)
        {
            //WordCount = wordCounterModel.CountWords(PlainText);
            WordCount = WordCouting.WordCount(PlainText);
           // CurNote.LastEdited = DateTime.Now;
        }
        private void Test(object obj)
        {
            MessageBox.Show(PlainText);
        }
        #endregion

        public NoteVM()
        {
            // Get database in ListNote
            //   List<NoteModel> listTemp = DataAccess.Instance.GetAllNotes();
            List<NoteModel> listTemp = DataAccess.Instance.GetNoteEnable();
            ListNote = new ObservableCollection<NoteModel>(listTemp);

            // Set up World Counter
            wordCounterModel = new WordCounterModel();

            // Set up Command
            WordCountCommand = new RelayCommand(UpdateWordCount);
            LoadPageCommand = new RelayCommand(LoadPage);
            SavePageCommand = new RelayCommand(SavePage);
            DeleteNoteCommand = new RelayCommand(DeleteNote);
            NoteToTrashCommand = new RelayCommand(NoteToTrash);
            ShowInsertTableWindowCommand = new RelayCommand(ShowInsertTAbleWindow, CanShowWindow);
            TestCommand = new RelayCommand(Test);
            NoteToArchivedCommand = new RelayCommand(NoteToArchived);
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
        


