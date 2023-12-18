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
    public partial class miniNote : UserControl, INotifyPropertyChanged
    {

        #region Dependency Property
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("miniNoteTitle", typeof(string), typeof(miniNote));
        public string miniNoteTitle
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty DateProperty = DependencyProperty.Register("miniNoteDate", typeof(DateTime), typeof(miniNote));
        public DateTime miniNoteDate
        {
            get { return (DateTime)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); OnPropertyChanged(nameof(miniNoteDate)); }
        }

        //public string Date
        //{
        //    get
        //    {
        //        DateTime dateTemp = (DateTime)GetValue(DateProperty);
        //        string stringTemp = dateTemp.Date.ToString();
        //        return stringTemp;
        //    }
        //    set 
        //    { 
        //        SetValue(DateProperty, value); 
        //    }
        //}

        public static readonly DependencyProperty HeadLineProperty = DependencyProperty.Register("miniNoteHeadLine", typeof(string), typeof(miniNote));
        public string miniNoteHeadLine
        {
            get { return (string)GetValue(HeadLineProperty); }
            set { SetValue(HeadLineProperty, value); OnPropertyChanged(nameof(miniNoteHeadLine)); }
        }

        public static readonly DependencyProperty BtnCommandProperty = DependencyProperty.Register("miniNoteBtnCommand", typeof(ICommand), typeof(miniNote));
        public ICommand miniNoteBtnCommand
        {
            get { return (ICommand)GetValue(BtnCommandProperty); }
            set { SetValue(BtnCommandProperty, value); OnPropertyChanged(nameof(miniNoteBtnCommand)); }
        }

        public static readonly DependencyProperty TestingProperty = DependencyProperty.Register("Testing", typeof(string), typeof(miniNote));
        public string Testing
        {
            get { return (string)GetValue(TestingProperty); }
            set { SetValue(TestingProperty, value); OnPropertyChanged(nameof(Testing)); }
        }
        #endregion

        public miniNote()
        {
            InitializeComponent();
            DataContext = this;
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
