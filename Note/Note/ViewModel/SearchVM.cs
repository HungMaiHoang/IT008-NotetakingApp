using Note.Model;
using System.Collections.ObjectModel;
﻿using Note.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;
using Note.View;

namespace Note.ViewModel
{
    public class SearchVM:ViewModelBase
    {
        private static SearchVM _instance;
        public static SearchVM Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SearchVM();
                }
                return _instance;
            }
        }

  
        private string _searchText;
        public string SearchText
        {
            get => _searchText; 
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
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


        private NoteModel _curNoteArchived;
        public NoteModel CurNoteArchived
        {
            get => _curNoteArchived;
            set
            {
                _curNoteArchived = value;
                OnPropertyChanged(nameof(CurNoteArchived));
            }
        }

        private NoteModel _curNoteTrash;
        public NoteModel CurNoteTrash
        {
            get => _curNoteTrash;
            set
            {
                _curNoteTrash = value;
                OnPropertyChanged(nameof(CurNoteTrash));
            }
        }
        private ObservableCollection<NoteModel> _notesList;
        public ObservableCollection<NoteModel> NotesList 
        { 
            get => _notesList;
            set
            {
                _notesList = value;
                OnPropertyChanged(nameof(NotesList));

            }
        }

        private ObservableCollection<NoteModel> _trashList;
        public ObservableCollection<NoteModel> TrashList 
        { 
            get => _trashList;
            set
            { 
                _trashList = value;
                OnPropertyChanged(nameof(TrashList));

            }
        }

        private ObservableCollection<NoteModel> _archivedList;
        public ObservableCollection<NoteModel> ArchivedList 
        { 
            get => _archivedList;
            set
            {
                _archivedList = value;
                OnPropertyChanged(nameof(ArchivedList));
              
            }
        }

        private bool _isListBox1Visible;

        public bool IsListBox1Visible
        {
            get { return _isListBox1Visible; }
            set
            {
                if (_isListBox1Visible != value)
                {
                    _isListBox1Visible = value;
                    OnPropertyChanged(nameof(IsListBox1Visible));
                }
            }
        }
        private bool _isListBox2Visible;

        public bool IsListBox2Visible
        {
            get { return _isListBox2Visible; }
            set
            {
                if (_isListBox2Visible != value)
                {
                    _isListBox2Visible = value;
                    OnPropertyChanged(nameof(IsListBox2Visible));
                }
            }
        }
        private bool _isListBox3Visible;

        public bool IsListBox3Visible
        {
            get { return _isListBox3Visible; }
            set
            {
                if (_isListBox3Visible != value)
                {
                    _isListBox3Visible = value;
                    OnPropertyChanged(nameof(IsListBox3Visible));
                }
            }
        }

 
        public ICommand GoToArchivedCommand {  get; set; }
        public ICommand GoToNoteCommand { get; set; }
        public ICommand GoToTrashCommand {  get; set; }
        private SearchVM()
        {
            GoToArchivedCommand = new RelayCommand(GoToArchived);
            GoToNoteCommand = new RelayCommand(GoToNote);
            GoToTrashCommand = new RelayCommand(GoToTrash);
        }

        private async void GoToNote(object obj)
        {
            CurNote = obj as NoteModel;
            MainWindowVM.Instance.CurrentView = NoteVM.Instance;

            await Task.Delay(1);

            if (CurNote.IsPinned)
            {
                NoteVM.Instance.PresentedListBox.SelectedIndex = NoteVM.Instance.ListPinnedNote.IndexOf(CurNote);
            }
            else
            {
                NoteVM.Instance.PresentedPinnedListBox.SelectedIndex = NoteVM.Instance.ListUnpinnedNote.IndexOf(CurNote);
            }
            
            NoteVM.Instance.LoadPage(CurNote);
            NoteVM.Instance.CurNote = CurNote;
        }

        private async void GoToArchived(object obj)
        {
            CurNoteArchived = obj as NoteModel;
            MainWindowVM.Instance.CurrentView = ArchivedVM.Instance;

            await Task.Delay(1);
            try
            {
                ArchivedVM.Instance.PresentedListBox.SelectedIndex = ArchivedVM.Instance.ListNote.IndexOf(CurNote);
                ArchivedVM.Instance.CurNote = CurNoteArchived;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private async void GoToTrash(object obj)
        {
            CurNoteTrash = obj as NoteModel;
            MainWindowVM.Instance.CurrentView = TrashVM.Instance;

            await Task.Delay(1);
            try
            {
                TrashVM.Instance.PresentedListBox.SelectedIndex = TrashVM.Instance.ListNote.IndexOf(CurNote);
                TrashVM.Instance.CurNote = CurNoteTrash;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public async Task<ObservableCollection<NoteModel>> SearchWithText(ObservableCollection<NoteModel> list, string text)
        {
            ObservableCollection<NoteModel> res = new ObservableCollection<NoteModel>();

            Regex regex = new Regex(text, RegexOptions.IgnoreCase);
            FlowDocument myDoc;

            foreach (var item in list)
            {
                if (regex.IsMatch(item.Title) || regex.IsMatch(item.HeadLine))
                {
                    res.Add(item);
                }
                else
                {
                    myDoc = await DataAccess.Instance.LoadRTFNote(item.FileId);
                    TextRange myTR = new TextRange(myDoc.ContentStart, myDoc.ContentEnd);
                    if (regex.IsMatch(myTR.Text))
                    {
                        res.Add(item);
                    }
                }
            }
            return res;
        }
    }
}
