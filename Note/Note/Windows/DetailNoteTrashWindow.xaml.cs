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
using System.Windows.Shapes;

namespace Note.Windows
{
    /// <summary>
    /// Interaction logic for DetailNoteTrashWindow.xaml
    /// </summary>
    public partial class DetailNoteTrashWindow : Window
    {
        public DetailNoteTrashWindow()
        {
            InitializeComponent();
            TrashVM.Instance.PageContent = richTextBox;
        }

        private void richTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            TrashVM.Instance.PageContent = sender as RichTextBox;
        }

        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (!IsMouseOverWindow(this, e.GetPosition(this)))
            //{
            //    // Đóng cửa sổ chính
            //    Close();
            //}

            //  Close();

        }
        private bool IsMouseOverWindow(Window window,Point point)
        {
            var rect = new Rect(window.Left, window.Top, window.Width, window.Height);
            return rect.Contains(point);
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
                Close();
        }
    }
}
