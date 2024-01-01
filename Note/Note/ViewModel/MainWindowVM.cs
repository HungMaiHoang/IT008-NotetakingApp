using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.WireProtocol.Messages.Encoders.BinaryEncoders;
using Note.Model;
using Note.Utilities;
using Note.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace Note.ViewModel
{
    internal class MainWindowVM : ViewModelBase
    {
        private MainWindow MyView;


        private UserModel _curUser;
        public UserModel CurUser
        {
            get => _curUser;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurUser));
            }
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
        // Action Command
        public ICommand NewNoteCommand { get; set; }


        private void Home(object obj) => CurrentView = HomeView;
        private void Note(object obj)
        {


            // Delete bug Note
            //NoteModel temp = DataAccess.Instance.GetNoteWithId(new ObjectId("000000000000000000000000"));
            //if (temp != null)
            //{
            //    DataAccess.Instance.DeleteNote(temp);
            //}

            // Load Notes from database
            //List<NoteModel> listTemp = DataAccess.Instance.GetAllNotes();
            List<NoteModel> listTemp = DataAccess.Instance.GetNoteEnable();
            NotesView.ListNote.Clear();
            NotesView.ListNote = new ObservableCollection<NoteModel>(listTemp);

            CurrentView = NotesView;
            //NotesView.SelectFirstNoteIfHas();
        }
        private void Reminder(object obj) => CurrentView = RemindersView;
        private void Task(object obj) => CurrentView = TasksView;
        private void Trash(object obj) 
        {
            List<NoteModel> listTemp = DataAccess.Instance.GetNoteDisable();
            TrashView.ListNote.Clear();
            TrashView.ListNote = new ObservableCollection<NoteModel>(listTemp);
            CurrentView = TrashView;
        }
        private void Archived(object obj)
        {
            List<NoteModel> listTemp = DataAccess.Instance.GetNoteArchived();
            ArchivedView.ListNote.Clear();
            ArchivedView.ListNote = new ObservableCollection<NoteModel>(listTemp);
            CurrentView = ArchivedView;
        }
        private void NewNote(object obj)
        {
            NoteModel note = NoteModel.CreateNewNote();

            DataAccess.Instance.SetTimeTrashFSFile(note, DateTime.MaxValue);

            DataAccess.Instance.UpdateNote(note);
            DataAccess.Instance.CreateTTLIndexForNote("TimeTrashTTL", 15);

            DataAccess.Instance.CreateTTLIndexForFSFile("TimeTrashFSTTL", 15);

            //NotesView.ListNote.Add(note);
            // sua lai them vao dau danh sach
            NoteVM.Instance.ListNote.Insert(0,note);
            CurrentView = NotesView;
        }

        private void Search(object obj)
        {
            CurrentView = SearchView;
        }
        public MainWindowVM(MainWindow view, UserModel user)
        {
            MyView = view;
            CurUser = user;

            HomeView = new HomeVM();
            NotesView = NoteVM.Instance;
            RemindersView = new ReminderVM();
            TasksView = new TaskVM();
            TrashView = TrashVM.Instance;
            ArchivedView = ArchivedVM.Instance;
            SearchView = SearchVM.Instance;


            HomeCommand = new RelayCommand(Home);
            NotesCommand = new RelayCommand(Note);
            RemindersCommand = new RelayCommand(Reminder);
            TasksCommand = new RelayCommand(Task);
            NewNoteCommand = new RelayCommand(NewNote);
            TrashCommand = new RelayCommand(Trash);
            ArchivedCommand = new RelayCommand(Archived);
            SearchCommand = new RelayCommand(Search);
            // Startup view
            CurrentView = HomeView;
        }
    }
}
