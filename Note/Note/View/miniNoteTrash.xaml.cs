using Note.ViewModel;
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
    /// Interaction logic for miniNote.xaml
    /// </summary>
    public partial class miniNoteTrash : UserControl, INotifyPropertyChanged
    {

        #region Dependency Property
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "miniNoteTrashTitle",
                typeof(string),
                typeof(miniNoteTrash),
                new PropertyMetadata("Unknow"));
        public string miniNoteTrashTitle
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty DateProperty =
            DependencyProperty.Register(
                "miniNoteTrashDate",
                typeof(DateTime),
                typeof(miniNoteTrash),
                new PropertyMetadata(DateTime.Now));
        //public DateTime miniNoteDate
        //{
        //    get { return (DateTime)GetValue(DateProperty); }
        //    set { SetValue(DateProperty, value); OnPropertyChanged(nameof(miniNoteDate)); }
        //}

        public DateTime miniNoteTrashDate
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
                OnPropertyChanged(nameof(miniNoteTrashDate));
            }
        }

        public static readonly DependencyProperty HeadLineProperty =
            DependencyProperty.Register(
                "miniNoteTrashHeadLine",
                typeof(string),
                typeof(miniNoteTrash),
                new PropertyMetadata(""));
        public string miniNoteTrashHeadLine
        {
            get { return (string)GetValue(HeadLineProperty); }
            set { SetValue(HeadLineProperty, value); OnPropertyChanged(nameof(miniNoteTrashHeadLine)); }
        }

        public static readonly DependencyProperty BtnCommandProperty =
            DependencyProperty.Register(
                "miniNoteTrashBtnCommand",
                typeof(ICommand),
                typeof(miniNoteTrash));
        public ICommand miniNoteTrashBtnCommand
        {
            get { return (ICommand)GetValue(BtnCommandProperty); }
            set { SetValue(BtnCommandProperty, value); OnPropertyChanged(nameof(miniNoteTrashBtnCommand)); }
        }
        #endregion

        public miniNoteTrash()
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
