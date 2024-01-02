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
    /// Interaction logic for SearchView.xaml
    /// </summary>
    public partial class SearchView : UserControl
    {
        public SearchView()
        {
            InitializeComponent();
            DataContext = SearchVM.Instance;
        }
        private async void TextBox_KeyEnterUpdate(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //TextBox tBox = (TextBox)sender;
                //DependencyProperty prop = TextBox.TextProperty;

                //BindingExpression binding = BindingOperations.GetBindingExpression(tBox, prop);
                //if (binding != null) { binding.UpdateSource(); }

                SearchVM view = SearchVM.Instance;
                view.ArchivedList = await view.SearchWithText(ArchivedVM.Instance.ListNote, (sender as TextBox).Text);
                if(view.ArchivedList.Count > 0)
                {   
                    view.IsListBox1Visible = true;
                }
                else view.IsListBox1Visible = false;
                view.NotesList = await view.SearchWithText(NoteVM.Instance.ListNote, (sender as TextBox).Text);
                if (view.NotesList.Count > 0)
                {
                    view.IsListBox2Visible = true;
                }
                else view.IsListBox2Visible = false;
                view.TrashList = await view.SearchWithText(TrashVM.Instance.ListNote, (sender as TextBox).Text);
                if (view.TrashList.Count > 0)
                {
                    view.IsListBox3Visible = true;
                }
                else view.IsListBox3Visible = false;

            }
        }

        private void ListBox_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            outerScrollViewer.ScrollToVerticalOffset(e.VerticalOffset);

        }

        private void OuterScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            double verticalOffset = e.VerticalOffset;
            double extentHeight = e.ExtentHeight;
            double viewportHeight = e.ViewportHeight;
        }

        private void ListBoxNotes_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            outerScrollViewer.ScrollToVerticalOffset(e.VerticalOffset);

        }
    }
}
