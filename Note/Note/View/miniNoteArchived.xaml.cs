using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Note.View
{
    /// <summary>
    /// Interaction logic for miniNoteArchived.xaml
    /// </summary>
    public partial class miniNoteArchived : UserControl, INotifyPropertyChanged
    {

        #region Dependency Property
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "miniNoteArchivedTitle",
                typeof(string),
                typeof(miniNoteArchived),
                new PropertyMetadata("Unknow"));
        public string miniNoteArchivedTitle
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty DateProperty =
            DependencyProperty.Register(
                "miniNoteArchivedDate",
                typeof(DateTime),
                typeof(miniNoteArchived),
                new PropertyMetadata(DateTime.Now));
        //public DateTime miniNoteDate
        //{
        //    get { return (DateTime)GetValue(DateProperty); }
        //    set { SetValue(DateProperty, value); OnPropertyChanged(nameof(miniNoteDate)); }
        //}

        public DateTime miniNoteArchivedDate
        {
            get
            {
                // Validate time needed

                return ((DateTime)GetValue(DateProperty)).Date;

                //DateTime dateTemp = (DateTime)GetValue(DateProperty);
                //string stringTemp = dateTemp.Date.ToString();
                //return stringTemp;
            }
            set
            {
                SetValue(DateProperty, value);
                OnPropertyChanged(nameof(miniNoteArchivedDate));
            }
        }

        public static readonly DependencyProperty HeadLineProperty =
            DependencyProperty.Register(
                "miniNoteArchivedHeadLine",
                typeof(string),
                typeof(miniNoteArchived),
                new PropertyMetadata(""));
        public string miniNoteArchivedHeadLine
        {
            get { return (string)GetValue(HeadLineProperty); }
            set { SetValue(HeadLineProperty, value); OnPropertyChanged(nameof(miniNoteArchivedHeadLine)); }
        }

        public static readonly DependencyProperty BtnCommandProperty =
            DependencyProperty.Register(
                "miniNoteArchivedBtnCommand",
                typeof(ICommand),
                typeof(miniNoteArchived));
        public ICommand miniNoteArchivedBtnCommand
        {
            get { return (ICommand)GetValue(BtnCommandProperty); }
            set { SetValue(BtnCommandProperty, value); OnPropertyChanged(nameof(miniNoteArchivedBtnCommand)); }
        }


        public static readonly DependencyProperty BtnRestoreCommandProperty =
            DependencyProperty.Register(
                "miniNoteArchivedBtnRestoreCommand",
                typeof(ICommand),
                typeof(miniNoteArchived));
        public ICommand miniNoteArchivedBtnRestoreCommand
        {
            get { return (ICommand)GetValue(BtnRestoreCommandProperty); }
            set { SetValue(BtnRestoreCommandProperty, value); OnPropertyChanged(nameof(miniNoteArchivedBtnRestoreCommand)); }
        }

        public static readonly DependencyProperty BtnDeleteForeverCommandProperty =
            DependencyProperty.Register(
                "miniNoteArchivedBtnDeleteForeverCommand",
                typeof(ICommand),
                typeof(miniNoteArchived));
        public ICommand miniNoteArchivedBtnDeleteForeverCommand
        {
            get { return (ICommand)GetValue(BtnDeleteForeverCommandProperty); }
            set { SetValue(BtnDeleteForeverCommandProperty, value); OnPropertyChanged(nameof(miniNoteArchivedBtnDeleteForeverCommand)); }
        }
        #endregion


        public miniNoteArchived()
        {
            InitializeComponent();
        }

        #region OnpropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
