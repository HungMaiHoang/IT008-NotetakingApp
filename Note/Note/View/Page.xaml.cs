using MongoDB.Bson;
using Note.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
    /// Interaction logic for Page.xaml
    /// </summary>
    public partial class Page : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(Page));
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); OnPropertyChanged(nameof(Title)); }
        }

        public Page()
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

        private void BoldButton(object sender, MouseButtonEventArgs e)
        {
            TextPointer selectionStart = TextBox.Selection.Start;
            TextPointer selectionEnd = TextBox.Selection.End;
            TextRange selectedTextRange = new TextRange(selectionStart, selectionEnd);
            object IsBold = selectedTextRange.GetPropertyValue(TextElement.FontWeightProperty);
            if (!IsBold.Equals(FontWeights.Bold))
            {
                ApplyFormattingText(TextElement.FontWeightProperty, FontWeights.Bold, selectionStart, selectionEnd);
            }
            else
            {
                ApplyFormattingText(TextElement.FontWeightProperty, FontWeights.Normal, selectionStart, selectionEnd);
            }
        }
        private void ItalicButton(object sender, MouseButtonEventArgs e)
        {
            TextPointer selectionStart = TextBox.Selection.Start;
            TextPointer selectionEnd = TextBox.Selection.End;
            TextRange selectedTextRange = new TextRange(selectionStart, selectionEnd);
            object IsBold = selectedTextRange.GetPropertyValue(TextElement.FontStyleProperty);
            if (!IsBold.Equals(FontStyles.Italic))
            {
                ApplyFormattingText(TextElement.FontStyleProperty, FontStyles.Italic, selectionStart, selectionEnd);
            }
            else
            {
                ApplyFormattingText(TextElement.FontStyleProperty, FontStyles.Normal, selectionStart, selectionEnd);
            }
        }
        private void UnderLineButton(object sender, MouseButtonEventArgs e)
        {
            TextPointer selectionStart = TextBox.Selection.Start;
            TextPointer selectionEnd = TextBox.Selection.End;
            TextRange selectedTextRange = new TextRange(selectionStart, selectionEnd);
            object IsUnderline = selectedTextRange.GetPropertyValue(Inline.TextDecorationsProperty);
            if (!IsUnderline.Equals(TextDecorations.Underline))
            {
                ApplyFormattingText(Inline.TextDecorationsProperty, TextDecorations.Underline, selectionStart, selectionEnd);
            }
            else
            {
                ApplyFormattingText(Inline.TextDecorationsProperty, null, selectionStart, selectionEnd);
            }
        }
        private void ApplyFormattingText(DependencyProperty property, object value, TextPointer selectionStart, TextPointer selectionEnd)
        {
            TextRange selectedTextRange = new TextRange(selectionStart, selectionEnd);
            if (!selectedTextRange.IsEmpty)
            {
                TextBox.Focus();
                selectedTextRange.ApplyPropertyValue(property, value);
                selectedTextRange.Select(selectionStart, selectionEnd);
            }
        }
        //private void SetParagraphAlignment(TextAlignment alignment)
        //{
        //    Paragraph paragraph = TextBox.Document.Blocks.FirstBlock as Paragraph;
        //    if (paragraph != null)
        //    {
        //        TextBox.Focus();
        //        paragraph.TextAlignment = alignment;
        //    }
        //}

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            string relativePath = "Note/Test.rtf";
            string fullPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), relativePath);
            TextRange range;
            FileStream stream;
            range = new TextRange(TextBox.Document.ContentStart, TextBox.Document.ContentEnd);
            //stream = new FileStream(fullPath, FileMode.OpenOrCreate);
            //range.Save(stream, System.Windows.DataFormats.Rtf);

            //da.CreateRTFNote(range);
        }
    }
}
