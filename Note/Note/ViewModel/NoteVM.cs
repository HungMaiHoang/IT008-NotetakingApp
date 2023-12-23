using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MongoDB.Driver;
using Note.Model;
using Note.Utilities;
using Note.View;
namespace Note.ViewModel
{
    public class NoteVM : ViewModelBase
    {
        private static NoteVM instance;
        public static NoteVM Instance
        {
            get { return instance; }
        }
        private NoteModel _curNote;
        public NoteModel CurNote
        {
            get => _curNote;
            set 
            { 
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
                OnPropertyChanged(nameof(PageTitle));
            }
        }

        public ICommand LoadPageCommand { get; set; }
        public ICommand CreatePageCommand { get; set; }
        public ICommand SavePageCommand { get; set; }

        private void LoadPage(object obj)
        {
            CurNote = obj as NoteModel;
            PageTitle = (obj as NoteModel).Title;
        }

        private void CreatePage(object obj)
        {
            MessageBox.Show("Create Page");
        }

        private void SavePage(object obj)
        {
            try
            {
                MessageBox.Show(CurNote.Title);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

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
            instance = this;
            List<NoteModel> listTemp = DataAccess.Instance.GetAllNotes();
            ListNote = new ObservableCollection<NoteModel>(listTemp);

            model = new WordCounterModel();
            WordCountCommand = new RelayCommand(UpdateWordCount);
            CreatePageCommand = new RelayCommand(CreatePage);
            SavePageCommand = new RelayCommand(SavePage);
            LoadPageCommand = new RelayCommand(LoadPage);

            // Startup note if has
            //if (ListNote.Count > 0 )
            //{
            //    CurNote = ListNote[0];
            //}
        }
        private void UpdateWordCount(object obj)
        {
            WordCount = model.CountWords(Text);
            CurNote.LastEdited = DateTime.Now;
        }
        
    }
}
