//using MaterialDesignThemes.Wpf;
using Note.Utilities;
using Note.ViewModel;
using Note.Windows;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Notes.xaml
    /// </summary>
    /// 

    public partial class Notes : UserControl
    {
        private FontFamily _selectedFont;
        public FontFamily SelectedFont
        {
            get { return _selectedFont; }
            set
            {
                _selectedFont = value;

            }
        }

        public Notes()
        {
            InitializeComponent();
            DataContext = NoteVM.Instance;
        }
        private void Bold(object sender, RoutedEventArgs e)
        {
            ApplyFormattingText(TextElement.FontWeightProperty, FontWeights.Bold);
        }
        private void UnBold(object sender, RoutedEventArgs e)
        {
            ApplyFormattingText(TextElement.FontWeightProperty, FontWeights.Normal);
        }
        private void Italic(object sender, RoutedEventArgs e)
        {
            ApplyFormattingText(TextElement.FontStyleProperty, FontStyles.Italic);
        }
        private void UnItalic(object sender, RoutedEventArgs e)
        {
            ApplyFormattingText(TextElement.FontStyleProperty, FontStyles.Normal);
        }
        private void UnderLine(object sender, RoutedEventArgs e)
        {
            ApplyFormattingText(Inline.TextDecorationsProperty, TextDecorations.Underline);
        }
        private void UnUnderLine(object sender, RoutedEventArgs e)
        {
            ApplyFormattingText(Inline.TextDecorationsProperty, null);
        }
        private void ApplyFormattingText(DependencyProperty property, object value)
        {
            TextPointer selectionStart = richTextBox.Selection.Start;
            TextPointer selectionEnd = richTextBox.Selection.End;
            TextRange selectedTextRange = new TextRange(selectionStart, selectionEnd);
            if (!selectedTextRange.IsEmpty)
            {
                richTextBox.Focus();
                selectedTextRange.ApplyPropertyValue(property, value);
                selectedTextRange.Select(selectionStart, selectionEnd);
            }
        }
        private void SetSelectionAlignment(TextAlignment alignment)
        {
            if (richTextBox != null)
            {

                TextPointer start = richTextBox.Selection.Start;
                TextPointer end = richTextBox.Selection.End;

                if (start != null && end != null)
                {
                    richTextBox.Focus();
                    TextRange selectedText = new TextRange(start, end);

                    // Create a paragraph to get the existing text formatting.
                    Paragraph paragraph = new Paragraph();
                    paragraph.TextAlignment = alignment;

                    // Apply the new formatting to the selected text.
                    selectedText.ApplyPropertyValue(Paragraph.TextAlignmentProperty, alignment);
                }
            }
        }

        private void AlignLeft(object sender, RoutedEventArgs e)
        {
            SetSelectionAlignment(TextAlignment.Left);
        }

        private void AlignCenter(object sender, RoutedEventArgs e)
        {
            SetSelectionAlignment(TextAlignment.Center);
        }

        private void AlignRight(object sender, RoutedEventArgs e)
        {
            SetSelectionAlignment(TextAlignment.Right);
        }
        private void AlignJustify(object sender, RoutedEventArgs e)
        {
            SetSelectionAlignment(TextAlignment.Justify);
        }
        private void UnselectedAlign(object sender, RoutedEventArgs e)
        {
            //if (LeftAlign.IsSelected == false && CenterAlign.IsSelected == false && RightAlign.IsSelected == false && JustifyAlign.IsSelected == false)
            //{
            //    SetSelectionAlignment(TextAlignment.Left);
            //    LeftAlign.IsSelected = true;
            //}
        }
        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            UpdateListBoxSelection();
        }

        private void UpdateListBoxSelection()
        {
            var selectedText = new TextRange(richTextBox.Selection.Start, richTextBox.Selection.End);
            Boldbutton.IsSelected = selectedText.GetPropertyValue(TextElement.FontWeightProperty).Equals(FontWeights.Bold);
            Italicbutton.IsSelected = selectedText.GetPropertyValue(TextElement.FontStyleProperty).Equals(FontStyles.Italic);
            if(selectedText.GetPropertyValue(Inline.TextDecorationsProperty)!=null)
            {
            Underlinebutton.IsSelected = selectedText.GetPropertyValue(Inline.TextDecorationsProperty).Equals(TextDecorations.Underline);
            }
            LeftAlign.IsSelected = selectedText.GetPropertyValue(Paragraph.TextAlignmentProperty).Equals(TextAlignment.Left);
            CenterAlign.IsSelected = selectedText.GetPropertyValue(Paragraph.TextAlignmentProperty).Equals(TextAlignment.Center);
            RightAlign.IsSelected = selectedText.GetPropertyValue(Paragraph.TextAlignmentProperty).Equals(TextAlignment.Right);
            JustifyAlign.IsSelected = selectedText.GetPropertyValue(Paragraph.TextAlignmentProperty).Equals(TextAlignment.Justify);
        }

        private void TextBox_KeyEnterUpdate(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox tBox = (TextBox)sender;
                DependencyProperty prop = TextBox.TextProperty;

                BindingExpression binding = BindingOperations.GetBindingExpression(tBox, prop);
                if (binding != null) { binding.UpdateSource(); }
            }
        }

        /// <summary>
        /// Get RichTextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void richTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            NoteVM.Instance.PageContent = sender as RichTextBox;
        }

        private void btnAddBulletList_Click(object sender, RoutedEventArgs e)
        {
            AddBulletList();
        }
        private void AddBulletList()
        {
            // Tạo danh sách đánh dấu mới
            List bulletList = new List();
            bulletList.ListItems.Add(new ListItem(new Paragraph(new Run(" "))));

            // Thêm danh sách vào FlowDocument hiện tại
            FlowDocument flowDocument = richTextBox.Document;
            if (flowDocument != null)
            {
                Paragraph existingParagraph = flowDocument.Blocks.FirstBlock as Paragraph;

                // Nếu có nội dung, thêm danh sách vào cuối
                flowDocument.Blocks.Add(bulletList);
            }
        }

        private void TextColor_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog colorDialog = new System.Windows.Forms.ColorDialog();
            colorDialog.ShowDialog();
            System.Drawing.Color selectedColor = colorDialog.Color;
            Color TextColor = Color.FromArgb(selectedColor.A, selectedColor.R, selectedColor.G, selectedColor.B);
            ApplyFormattingText(TextElement.ForegroundProperty, new SolidColorBrush(TextColor));
        }

        private void InsertImageButton_Click(object sender, RoutedEventArgs e)
        {

            // Mở hộp thoại chọn tệp tin ảnh
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                // Lấy đường dẫn tệp tin ảnh
                string imagePath = openFileDialog.FileName;

                // Tạo đối tượng Image từ đường dẫn ảnh
                Image image = new Image();
                BitmapImage bitmap = new BitmapImage(new Uri(imagePath));
                image.Source = bitmap;

                // Tạo đối tượng InlineUIContainer để chứa ảnh
                InlineUIContainer container = new InlineUIContainer(image);

                // Tạo đối tượng Paragraph và chèn InlineUIContainer vào đó
                Paragraph paragraph = new Paragraph(container);

                // Chèn Paragraph vào FlowDocument của RichTextBox
                richTextBox.Document.Blocks.Add(paragraph);
            }
        }

        private void InsertTable(object sender, RoutedEventArgs e)
        {
            InsertTableWindow insertTableWindow = new InsertTableWindow();
            insertTableWindow.ShowDialog();
            int Columns = insertTableWindow.GetColumns();
            int Rows = insertTableWindow.GetRows();
            Table table = new Table();
            richTextBox.BeginChange();
            var gridLenghtConvertor = new GridLengthConverter();
            table.Columns.Add(new TableColumn());
            table.RowGroups.Add(new TableRowGroup());
            for (int i = 0; i < Rows; i++)
            {
                TableRow row = new TableRow();
                for (int j = 0; j < Columns; j++)
                {
                    row.Cells.Add(new TableCell(new Paragraph(new Run($"Cell {i + 1}")))
                    {
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(1)
                    });
                }
                table.RowGroups[0].Rows.Add(row);
            }

            // Set table border properties
            table.BorderThickness = new Thickness(1);
            table.BorderBrush = Brushes.Black;

            // Add the table to the RichTextBox
            richTextBox.Document.Blocks.Add(table);

            richTextBox.EndChange();
        }

        private void RemoveTable(object sender, RoutedEventArgs e)
        {
            //TextRange selectedTextRange = new TextRange(richTextBox.Selection.Start, richTextBox.Selection.End);
            //Table table = new Table();

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //NoteVM.Instance.Text = richTextBox.Document.ContentStart.GetTextInRun(LogicalDirection.Forward);
            TextRange myTR = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            NoteVM.Instance.PlainText = myTR.Text;
            richTextBox.IsDocumentEnabled = true;
            List<Run> runsToAddHyperlink = new List<Run>();

            TextRange textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);

            foreach (Run run in richTextBox.Document.Blocks.OfType<Paragraph>().SelectMany(p => p.Inlines.OfType<Run>()))
            {
                if (Uri.IsWellFormedUriString(run.Text, UriKind.Absolute))
                {
                    runsToAddHyperlink.Add(run);
                }
            }

            foreach (Run run in runsToAddHyperlink)
            {
                Hyperlink hyperlink = new Hyperlink(run.ContentStart, run.ContentEnd)
                {
                    NavigateUri = new Uri(run.Text),
                    TargetName = "_blank",
                    IsEnabled = true

                };

                hyperlink.RequestNavigate += Hyperlink_RequestNavigate;
            }
        }
            private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
            {

                try
                {

                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(e.Uri.AbsoluteUri));
                    e.Handled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening link: {ex.Message}");
                }
            }

       
        private void FontSize_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (FontSizeBox.Text != null && Double.TryParse(FontSizeBox.Text, out double fontSize))
                {        
                    richTextBox.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, fontSize);
                }
                e.Handled = true;
            }
        }

        private void FontSizeBox_DropDownClosed(object sender, EventArgs e)
        {
            if (FontSizeBox.Text != null && Double.TryParse(FontSizeBox.Text, out double fontSize))
            {
                richTextBox.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, fontSize);
            }
        }

        private void FontBox_DropDownClosed(object sender, EventArgs e)
        {
            FontFamily selectedFont = FontBox.SelectedItem as FontFamily;
            if (selectedFont != null)
            {
               
                richTextBox.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, selectedFont);
            }
        }
    }
}
