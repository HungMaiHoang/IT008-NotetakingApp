using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                //if (CurNote is NoteModel && CurNote != null)
                //{
                //    //PageHeadLine = Regex.Replace(PlainText.Length > 20 ? PlainText.Substring(0, 19) : PlainText, "\n", string.Empty);
                        
                //    DataAccess.Instance.UpdateRTFNote(CurNote.FileId, PageContent.Document);
                //    DataAccess.Instance.UpdateNote(CurNote);
                //}

                if (value == null)
                {
                    CheckRichtxt = true;
                }
                else CheckRichtxt = false;
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
                ListUnpinnedNote = new ObservableCollection<NoteModel>();
                ListPinnedNote = new ObservableCollection<NoteModel>();
                _listPinnedNote.CollectionChanged += listPinnedChange;
                CountNote = _listNote.Count();
                foreach (var note in _listNote)
                {
                    if (note.IsPinned)
                    {
                        ListPinnedNote.Add(note);
                    }
                    else
                    {
                        ListUnpinnedNote.Add(note);
                    }
                }
                OnPropertyChanged(nameof(ListNote));
            }
        }
        // Unpinned List Note
        private ObservableCollection<NoteModel> _listUnpinnedNote;
        public ObservableCollection<NoteModel> ListUnpinnedNote
        {
            get => _listUnpinnedNote;
            set
            {
                _listUnpinnedNote = value;
                OnPropertyChanged(nameof(ListUnpinnedNote));
            }
        }
        // Pinned List Note
        private ObservableCollection<NoteModel> _listPinnedNote;
        public ObservableCollection<NoteModel> ListPinnedNote
        {
            get => _listPinnedNote;
            set
            {
                _listPinnedNote = value;
                OnPropertyChanged(nameof(ListPinnedNote));
            }
        }
        // Unpinned List Box
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
        // Pinned List Box
        private ListBox _presentedPinnedListBox;
        public ListBox PresentedPinnedListBox
        {
            get => _presentedPinnedListBox;
            set
            {
                _presentedPinnedListBox = value;
                OnPropertyChanged(nameof(PresentedPinnedListBox));
            }
        }
        private bool _isListBox1Visible;
        public bool IsListBox1Visible
        {
            get => _isListBox1Visible;
            set
            {
                _isListBox1Visible = value;
                OnPropertyChanged(nameof(IsListBox1Visible));
            }
        }
        private string _filterListNote;
        public string FilterListNote
        {
            get => _filterListNote;
            set
            {
                _filterListNote = value;
                ChangeOnFilter();
                OnPropertyChanged(nameof(FilterListNote));
            }
        }

        private ObservableCollection<string> comboBoxItems = new ObservableCollection<string>() { "A->Z", "Z->A", "Last Edit", "First Edit" };
        public ObservableCollection<string> ComboBoxItems
        {
            get => comboBoxItems;
            set { comboBoxItems = value;
                OnPropertyChanged(nameof(ComboBoxItems));
            }
        }
        private int countNote;
        public int CountNote
        {
            get { return countNote; }
            set { countNote= value;
                OnPropertyChanged(nameof(CountNote));}

        }
        #region Page Things
        // Note Title
        private string _pageTitle;
        public string PageTitle
        {
            get => _pageTitle;
            set
            {
                if (CurNote is NoteModel && CurNote != null)
                {
                    _pageTitle = value;
                    CurNote.Title = value;

                    OnPropertyChanged(nameof(PageTitle));
                }
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
        // First 20 character of PlantText
        private string _pageHeadLine;
        public string PageHeadLine
        {
            get => _pageHeadLine;
            set
            {
                if (_pageHeadLine != value && CurNote != null)
                {
                    _pageHeadLine = value;
                    CurNote.HeadLine = value;
                    OnPropertyChanged(nameof(PageHeadLine));
                }
            }
        }
        #endregion


        private bool checkRichtxt;
        public bool CheckRichtxt
        {
            get { return checkRichtxt; }
            set
            {
                CheckToolbar = !value;
                checkRichtxt = value;
                OnPropertyChanged(nameof(CheckRichtxt));
            }
        }
        
        private bool checkToolbar;
        public bool CheckToolbar
        {
            get { return checkToolbar; }
            set
            {
                checkToolbar = value;
                OnPropertyChanged(nameof(CheckToolbar));
            }
        }
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
        public ICommand PinNoteCommand { get; set; }
        public ICommand UnpinNoteCommand { get; set; }

        public async void LoadPage(object obj)
        {
            try
            {
                CurNote = obj as NoteModel;
                PageTitle = CurNote.Title;

                // Load Content
                PageContent.Document = await DataAccess.Instance.LoadRTFNote(CurNote.FileId);
            }
            catch (Exception ex)
            {
            }
        }
        private void SavePage(object obj)
        {
            PageHeadLine = Regex.Replace(PlainText.Length > 20 ? PlainText.Substring(0, 19) : PlainText, "\n|\r", string.Empty);
            if (CurNote == null)
                return;

            DataAccess.Instance.UpdateRTFNote(CurNote.FileId, PageContent.Document);
            CurNote.Title = PageTitle;
            CurNote.LastEdited = DateTime.UtcNow;
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
            
            if (CurNote.IsPinned)
            {
                CurNote.IsPinned = false;
                ListPinnedNote.Remove(CurNote);
            }
            else
            {
                ListUnpinnedNote.Remove(CurNote);
            }
            listNoteChange(null,null);
        }
        private void NoteToArchived(object obj)
        {
            DataAccess.Instance.NoteToArchived(CurNote);
            
            ListNote.Remove(CurNote);
            
            if (CurNote.IsPinned)
            {
                CurNote.IsPinned = false;
                ListPinnedNote.Remove(CurNote);
            }
            else
            {
                ListUnpinnedNote.Remove(CurNote);
            }
            listNoteChange(null, null);
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
        private void PinNote(object obj)
        {
           // CurNote = obj as NoteModel;
            CurNote.IsPinned = true;
            ListPinnedNote.Add(CurNote);
            ListUnpinnedNote.Remove(CurNote);
        }
        private void UnpinNote(object obj)
        {
          //  CurNote = obj as NoteModel;
            CurNote.IsPinned = false;
            ListUnpinnedNote.Add(CurNote);
            ListPinnedNote.Remove(CurNote);
        }
        private void Test(object obj)
        {
            MessageBox.Show(PageHeadLine);
        }
        #endregion

        public NoteVM()
        {
            // Get database in ListNote
            List<NoteModel> listTemp = DataAccess.Instance.GetNoteEnable(UserHolder.CurUser);
            ListNote = new ObservableCollection<NoteModel>(listTemp);
            
            _listNote.CollectionChanged += listNoteChange;
            _listPinnedNote.CollectionChanged += listPinnedChange;
            _listUnpinnedNote.CollectionChanged += listUnpinnedChange;
            if (ListPinnedNote.Count > 0)
            {
                IsListBox1Visible = true;
            }
            else
            {
                IsListBox1Visible = false;
            }
            CountNote = ListNote.Count();
            CheckRichtxt = true;
            checkToolbar = false;
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
            PinNoteCommand = new RelayCommand(PinNote);
            UnpinNoteCommand = new RelayCommand(UnpinNote);
        }

        private void listUnpinnedChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            CountNote= ListNote.Count();
        }
        private void listNoteChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            CountNote = ListNote.Count();
        }
        private void listPinnedChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (ListPinnedNote.Count > 0)
            {
                IsListBox1Visible = true;
            }
            else
            {
                IsListBox1Visible = false;
            }
            CountNote = ListNote.Count();
        }
        private void ChangeOnFilter()
        {
            switch (FilterListNote)
            {
                case "A->Z":
                    ListNote = FilterList.AscedingTitle(ListNote);
                    break;
                case "Z->A":
                    ListNote = FilterList.DescendingTitle(ListNote);
                    break;
                case "First Edit":
                    ListNote = FilterList.AscedingLastEdit(ListNote);
                    break;
                case "Last Edit":
                    ListNote = FilterList.DescendingLastEdit(ListNote);
                    break;
                default:
                    break;
            }
        }
    }
}
        


