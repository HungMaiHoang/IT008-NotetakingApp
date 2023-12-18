using Note.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class miniNote : UserControl
    {

        #region Dependency Property
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(miniNote));
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty DateProperty = DependencyProperty.Register("Date", typeof(DateTime), typeof(miniNote));
        public DateTime Date
        {
            get { return (DateTime)GetValue(DateProperty); }
            set { SetValue(TitleProperty, value); }
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

        public static readonly DependencyProperty HeadLineProperty = DependencyProperty.Register("HeadLine", typeof(string), typeof(miniNote));
        public string HeadLine
        {
            get { return (string)GetValue(HeadLineProperty); }
            set { SetValue(HeadLineProperty, value); }
        }

        public static readonly DependencyProperty BtnCommandProperty = DependencyProperty.Register("BtnCommand", typeof(ICommand), typeof(miniNote));
        public ICommand BtnCommand
        {
            get { return (ICommand)GetValue(BtnCommandProperty); }
            set { SetValue(BtnCommandProperty, value); }
        }
        #endregion

        public miniNote()
        {
            InitializeComponent();
        }
    }
}
