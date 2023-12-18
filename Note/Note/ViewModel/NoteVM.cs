﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Note.Model;
using Note.Utilities;
using Note.View;
namespace Note.ViewModel
{
    class NoteVM : Utilities.ViewModelBase
    {
        public string Test = "Thins";
        public int intTest = -1;

        private NoteModel _curNote;
        public NoteModel CurNote
        {
            get => _curNote;
            set { CurNote = value; OnPropertyChanged(nameof(CurNote)); }
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


        private miniNoteVM _miniNote;
        public miniNoteVM MiniNote { get => _miniNote; set => _miniNote = value; }

        public ICommand PageCommand { get; set; }
        private void Page(object obj)
        {
            MessageBox.Show("Selected Page With Command");
        }

        //public ICommand PageCommand { get; set; }
        //private void Page(object obj) => CurNote = new PageVM();

        public NoteVM()
        {
            List<NoteModel> listTemp = DataAccess.Instance.GetAllNotes();
            ListNote = new ObservableCollection<NoteModel>(listTemp);

            PageCommand = new RelayCommand(Page);

            // Startup note if has
            //if (ListNote.Count > 0 )
            //{
            //    CurNote = ListNote[0];
            //}
        }
    }
}
