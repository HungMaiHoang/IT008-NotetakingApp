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

        private SearchVM()
        {
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
