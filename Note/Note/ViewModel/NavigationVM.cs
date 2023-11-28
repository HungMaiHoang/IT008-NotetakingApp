using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Note.Utilities;
using System.Windows.Input;
namespace Note.ViewModel
{
    internal class NavigationVM : ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }
        private object noteView;
        public object NoteView
        {
            get { return noteView; }
            set { noteView = value; OnPropertyChanged();}
        }
        public ICommand PageCommand { get; set; }
        public ICommand HomeCommand { get; set; }
        public ICommand NotesCommand { get; set; }
        public ICommand RemindersCommand { get; set; }
        public ICommand TasksCommand { get; set; }

        private void Page(object obj) => NoteView = new PageVM();
        private void Home(object obj) => CurrentView = new HomeVM();
        private void Note(object obj) => CurrentView = new NoteVM();
        private void Reminder(object obj) => CurrentView = new ReminderVM();
        private void Task(object obj) => CurrentView = new TaskVM();

        public NavigationVM()
        {
            HomeCommand = new RelayCommand(Home);
            NotesCommand = new RelayCommand(Note);
            RemindersCommand = new RelayCommand(Reminder);
            TasksCommand = new RelayCommand(Task);
            PageCommand = new RelayCommand(Page);
            // startup page
            CurrentView = new HomeVM();
            
            
        }
    }
}
