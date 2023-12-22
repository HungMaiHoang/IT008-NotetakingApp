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
    class NoteVM : ViewModelBase
    {
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

        public ICommand LoadPageCommand { get; set; }
        public ICommand SavePageCommand { get; set; }
        public ICommand SaveTitleCommand { get; set; }

        private void LoadPage(object obj)
        {
            try
            {
                CurNote = (obj as NoteModel);
                PageTitle = CurNote.Title;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public NoteVM()
        {

            // Get Notes from databse
            List<NoteModel> listTemp = DataAccess.Instance.GetAllNotes();
            ListNote = new ObservableCollection<NoteModel>(listTemp);

            LoadPageCommand = new RelayCommand(LoadPage);


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
