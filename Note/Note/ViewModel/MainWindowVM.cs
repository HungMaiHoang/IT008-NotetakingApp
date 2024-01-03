using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.WireProtocol.Messages.Encoders.BinaryEncoders;
using Note.Model;
using Note.Utilities;
using Note.View;
using Note.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Note.ViewModel
{
    internal class MainWindowVM : ViewModelBase
    {
        private UserModel _curUser;
        public UserModel CurUser
        {
            get => _curUser;
            set
            {
                _curUser = value;
                OnPropertyChanged(nameof(CurUser));
            }
        }
        private MainWindow MyView;
        private static MainWindowVM instance;
        public static MainWindowVM Instance
        {
            get { return instance; }
            set { instance = value; }
        }


        private ViewModelBase _currentView;
        public ViewModelBase CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(nameof(CurrentView)); }
        }

        private HomeVM HomeView;
        private NoteVM NotesView;
        private ReminderVM RemindersView;
        private TaskVM TasksView;
        private TrashVM TrashView;
        private ArchivedVM ArchivedView;
        private SearchVM SearchView;
        // View Navigate Command
        public ICommand HomeCommand { get; set; }
        public ICommand NotesCommand { get; set; }
        public ICommand RemindersCommand { get; set; }
        public ICommand TasksCommand { get; set; }
        public ICommand TrashCommand { get; set; }
        public ICommand ArchivedCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand SettingCommand { get; set; }
        // Action Command
        public ICommand NewNoteCommand { get; set; }


        private void Home(object obj) => CurrentView = HomeView;
        private void Note(object obj)
        {
            // Load Notes from database
            List<NoteModel> listTemp = DataAccess.Instance.GetNoteEnable(CurUser);
            NotesView.ListNote.Clear();
            NotesView.ListNote = new ObservableCollection<NoteModel>(listTemp);
            NoteVM.Instance.CountNote = listTemp.Count;
            CurrentView = NotesView;
        }
        private void Reminder(object obj) => CurrentView = RemindersView;
        private void Task(object obj) => CurrentView = TasksView;
        private void Trash(object obj) 
        {
            List<NoteModel> listTemp = DataAccess.Instance.GetNoteDisable(CurUser);
            TrashView.ListNote.Clear();
            TrashView.ListNote = new ObservableCollection<NoteModel>(listTemp);
            CurrentView = TrashView;
        }
        private void Archived(object obj)
        {
            List<NoteModel> listTemp = DataAccess.Instance.GetNoteArchived(CurUser);
            ArchivedView.ListNote.Clear();
            ArchivedView.ListNote = new ObservableCollection<NoteModel>(listTemp);
            CurrentView = ArchivedView;
        }
        private void NewNote(object obj)
        {
            NoteModel note = NoteModel.CreateNewNote(CurUser);

            // await DataAccess.Instance.SetTimeTrashFSFile(note, DateTime.MaxValue);
            DataAccess.Instance.UpdateNote(note);
            //       DataAccess.Instance.AddPropertyToGridFSFilesCollection("TimetoTrash", DateTime.MaxValue.ToString());
            //       DataAccess.Instance.CreateTTLIndexForNote("TimeTrashTTL", 15);

            //     DataAccess.Instance.CreateTTLIndexForFSFile("timetrash_index", 15);
            //NotesView.ListNote.Add(note);
            // sua lai them vao dau danh sach
            //if (note.IsPinned)
            //{
            //    NoteVM.Instance.ListPinnedNote.Insert(0, note);
            //}
            //else
            //{
            //    NoteVM.Instance.ListUnpinnedNote.Insert(0, note);
            //}

            if (SettingWindowVM.Instance.IsCheckAddFirst)
            {
                NoteVM.Instance.ListNote.Insert(0, note);
                if (note.IsPinned)
                {
                    NoteVM.Instance.ListPinnedNote.Insert(0, note);
                }
                else
                {
                    NoteVM.Instance.ListUnpinnedNote.Insert(0, note);
                }
            }
            else
            {
                NoteVM.Instance.ListNote.Add(note);
                if (note.IsPinned)
                {
                    NoteVM.Instance.ListPinnedNote.Add(note);
                }
                else
                {
                    NoteVM.Instance.ListUnpinnedNote.Add(note);
                }
            }
            NoteVM.Instance.CountNote=NoteVM.Instance.ListNote.Count();
                CurrentView = NotesView;
      //      CreateTrigger createTrigger = new CreateTrigger();
        //    createTrigger.CreateTriggerDelete();
        }



        private void Search(object obj)
        {
            SearchVM view = SearchVM.Instance;
            CurrentView = SearchView;
            view.SearchText = string.Empty;
            view.NotesList?.Clear();
            view.TrashList?.Clear();
            view.ArchivedList?.Clear();
            view.IsListBox1Visible = false;
            view.IsListBox2Visible = false;
            view.IsListBox3Visible = false;
        }
        private void Settings(object obj)
        {
            SettingWindow settingWindow = new SettingWindow();
            settingWindow.ShowDialog();
        }

        private BitmapImage bitmapImage;
        public BitmapImage BitmapImage
        {
            get => bitmapImage;
            set
            {
                bitmapImage = value;
                OnPropertyChanged(nameof(BitmapImage));
            }
        }
        private string nameUser;
        public string NameUser
        {
            get { return nameUser; } 
            set { nameUser = value; OnPropertyChanged(nameof(NameUser)); } 
        }
        public MainWindowVM(MainWindow view)
        {
            CurUser = UserHolder.CurUser;
            Instance = this;
            MyView = view;

            HomeView = new HomeVM();
            NotesView = NoteVM.Instance;
            RemindersView = new ReminderVM();
            TasksView = new TaskVM();
            TrashView = TrashVM.Instance;
            ArchivedView = ArchivedVM.Instance;
            SearchView = SearchVM.Instance;
            BitmapImage = DataAccess.Instance.ByteArrayToBitmapImage(UserHolder.CurUser.Image);
            nameUser = UserHolder.CurUser.Name;
            HomeCommand = new RelayCommand(Home);
            NotesCommand = new RelayCommand(Note);
            RemindersCommand = new RelayCommand(Reminder);
            TasksCommand = new RelayCommand(Task);
            NewNoteCommand = new RelayCommand(NewNote);
            TrashCommand = new RelayCommand(Trash);
            ArchivedCommand = new RelayCommand(Archived);
            SearchCommand = new RelayCommand(Search);
            SettingCommand = new RelayCommand(Settings);
            // Startup view
            CurrentView = HomeView;
        }
    }
}
