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
using System.Windows.Media;
using MongoDB.Bson;
using MongoDB.Driver;
using Note.Model;
using Note.Utilities;
using Note.View;
using Note.Windows;
namespace Note.ViewModel
{
    class NoteVM : ViewModelBase
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
                _curNote = value;
                //Update note to database
                //if (CurNote is NoteModel)
                //{
                //    DataAccess.Instance.UpdateNote(CurNote);
                //}
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

        private TextRange _pageContent;
        public TextRange PageContent
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
        public ICommand ShowInsertTableWindowCommand { get; set; }
        private void LoadPage(object obj)
        {
            try
            {
                CurNote = (obj as NoteModel);
                PageTitle = CurNote.Title;

                //MessageBox.Show(PageContent.ToString());

                PageContent.ClearAllProperties();
                DataAccess.Instance.LoadRTFNote(CurNote.FileId, PageContent);


                //TextRange temp = new TextRange(PageContent.ContentStart, PageContent.ContentEnd);
                //DataAccess.Instance.LoadRTFNote(CurNote.Id, temp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SavePage(object obj)
        {
            DataAccess.Instance.UpdateRTFNote(CurNote.FileId, PageContent);
        }
        private void DeleteNote(object obj)
        {
            //if (CurNote is NoteModel && CurNote != null)
            //{
            //    ListNote.Remove(CurNote);
            //    DataAccess.Instance.DeleteNote(CurNote);
            //}
        }
        private void Test(object obj)
        {
            //PageContent.Text = "Testing";
            DataAccess.Instance.GetAllNotes();
        }

        private NoteVM()
        {
            // Load Notes from databse
            List<NoteModel> listTemp = DataAccess.Instance.GetAllNotes();
            ListNote = new ObservableCollection<NoteModel>(listTemp);

            LoadPageCommand = new RelayCommand(LoadPage);
            SavePageCommand = new RelayCommand(SavePage);
            DeleteNoteCommand = new RelayCommand(DeleteNote);
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
    }
}
        


