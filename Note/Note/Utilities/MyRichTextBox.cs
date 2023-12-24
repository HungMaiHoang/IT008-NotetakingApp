using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace Note.Utilities
{
    internal class MyRichTextBox : DependencyObject
    {
        public static string GetDocument(DependencyObject obj)
        {
            return (string)obj.GetValue(DocumentProperty);
        }

        public static void SetDocument(DependencyObject obj, string value)
        {
            obj.SetValue(DocumentProperty, value);
        }

        public string Document
        {
            get { return GetDocument(this); }
            set { SetDocument(this, value); }
        }
        public static readonly DependencyProperty DocumentProperty =
            DependencyProperty.RegisterAttached(
                "Document",
                typeof(FlowDocument),
                typeof(MyRichTextBox),
                new FrameworkPropertyMetadata
                {
                    BindsTwoWayByDefault = true,
                    PropertyChangedCallback = (obj, e) =>
                    {
                        var richTextBox = (RichTextBox)obj;

                        // Parse the XAML to a document (or use XamlReader.Parse())
                        var rtf = GetDocument(richTextBox);
                        var doc = new FlowDocument();
                        var range = new TextRange(doc.ContentStart, doc.ContentEnd);

                        range.Load(new MemoryStream(Encoding.UTF8.GetBytes(rtf)), DataFormats.Rtf);

                        // Set the document
                        richTextBox.Document = doc;
                        range.Changed += (obj2, e2) =>
                        {
                            if (richTextBox.Document == doc)
                            {
                                MemoryStream buffer = new MemoryStream();
                                range.Save(buffer, DataFormats.Rtf);
                                SetDocument(richTextBox, Encoding.UTF8.GetString(buffer.ToArray()));
                            }
                        };
                    }
                });
    }
}
