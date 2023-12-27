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

        public ICommand LoadPageCommand { get; set; }
        public ICommand SavePageCommand { get; set; }
        public ICommand DeleteNoteCommand { get; set; }
        public ICommand TestCommand { get; set; }
        public ICommand NoteToTrashCommand { get; set; }

        public ICommand ShowInsertTableWindowCommand { get; set; }
        
        private void LoadPage(object obj)
        {
            try
            {
                CurNote = (obj as NoteModel);
                PageTitle = CurNote.Title;

                // Load Content
                PageContent.Document = DataAccess.Instance.LoadRTFNote(CurNote.FileId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SavePage(object obj)
        {
            DataAccess.Instance.UpdateRTFNote(CurNote.FileId, PageContent.Document);
        }
        private void DeleteNote(object obj)
        {
            if (CurNote is NoteModel && CurNote != null)
            {
                DataAccess.Instance.DeleteNote(CurNote.Id);
                ListNote.Remove(CurNote);
            }
        }
        private void Test(object obj)
        {
            MessageBox.Show(Text);
        }
        // count word
        private WordCounterModel model;
        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                if (text != value)
                {
                    text = value;
                    OnPropertyChanged(nameof(Text));
                }
            }
        }
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
        public ICommand WordCountCommand { get;}
        public NoteVM()
        {
            //instance = this;
            List<NoteModel> listTemp = DataAccess.Instance.GetAllNotes();
            ListNote = new ObservableCollection<NoteModel>(listTemp);

            model = new WordCounterModel();
            WordCountCommand = new RelayCommand(UpdateWordCount);
            LoadPageCommand = new RelayCommand(LoadPage);
            SavePageCommand = new RelayCommand(SavePage);
            DeleteNoteCommand = new RelayCommand(DeleteNote);
            NoteToTrashCommand = new RelayCommand(NoteToTrash);
            TestCommand = new RelayCommand(Test);
            ShowInsertTableWindowCommand = new RelayCommand(ShowInsertTAbleWindow, CanShowWindow);

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
        private void ShowInsertTAbleWindow(object obj)
        {
            InsertTableWindow insertTableWindow = new InsertTableWindow();
            insertTableWindow.ShowDialog();
            
        }

        // Phương thức tìm kiếm đối tượng con trong cấu trúc Visual Tree
        


        private bool CanShowWindow(object obj)
        {
            return true;
        }
        private void UpdateWordCount(object obj)
        {
            WordCount = model.CountWords(Text);
            CurNote.LastEdited = DateTime.Now;
        }
        private void NoteToTrash(object obj)
        {
            DataAccess.Instance.NoteToTrash(CurNote);
            ListNote.Remove(CurNote);
        }

    }
}
        


