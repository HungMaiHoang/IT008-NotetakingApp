using Note.Model;
using Note.Utilities;
using Note.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Note.ViewModel
{
    internal class MainWindowVM : ViewModelBase
    {
        private MainWindow MyView;

        private ViewModelBase _currentView;
        public ViewModelBase CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        private BlankVM BlankView;
        private HomeVM HomeView;
        private NoteVM NotesView;
        private ReminderVM RemindersView;
        private TaskVM TasksView;

        // View Navigate Command
        public ICommand HomeCommand { get; set; }
        public ICommand NotesCommand { get; set; }
        public ICommand RemindersCommand { get; set; }
        public ICommand TasksCommand { get; set; }

        // Action Command
        public ICommand NewNoteCommand { get; set; }


        private void Home(object obj) => CurrentView = HomeView;
        private void Note(object obj)
        {
            CurrentView = NotesView;

            // Load Notes from databse
            List<NoteModel> listTemp = DataAccess.Instance.GetAllNotes();
            NotesView.ListNote = new ObservableCollection<NoteModel>(listTemp);

            NotesView.SelectFirstNoteIfHas();
        }
        private void Reminder(object obj) => CurrentView = RemindersView;
        private void Task(object obj) => CurrentView = TasksView;
        private void NewNote(object obj)
        {
            NoteModel note = new NoteModel();

            DataAccess.Instance.InsertNote(note);

            NotesView.ListNote.Add(note);

            CurrentView = NotesView;

            // Reload view
            //CurrentView = BlankView;
            //System.Threading.Tasks.Task.Delay(1).ContinueWith(_ =>
            //{
            //    CurrentView = NotesView;
            //});
        }

        public MainWindowVM(MainWindow view)
        {
            MyView = view;

            BlankView = new BlankVM();
            HomeView = new HomeVM();
            NotesView = NoteVM.Instance;
            RemindersView = new ReminderVM();
            TasksView = new TaskVM();

            HomeCommand = new RelayCommand(Home);
            NotesCommand = new RelayCommand(Note);
            RemindersCommand = new RelayCommand(Reminder);
            TasksCommand = new RelayCommand(Task);
            NewNoteCommand = new RelayCommand(NewNote);

            // Startup view
            CurrentView = BlankView;
        }
    }
}
