using Note.Model;
using Note.Utilities;
using Note.View;
using Note.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;


namespace Note.ViewModel
{
    internal class InsertTableVM:ViewModelBase
    {
       
        public ICommand InsertTableCommand { get; set; }
        public InsertTableVM()
        {
            InsertTableCommand = new RelayCommand(InsertTable,CanInsertTable);
        }
        private void InsertTable(object obj)
        {

            var view = Application.Current.MainWindow?.FindChild<Notes>(null) ;
            if (view != null)
            {
                Grid grid1 = view.FindChild<Grid>(null);

                if (grid1 != null)
                {
                    // Lấy ra đối tượng Grid thứ hai từ Grid thứ nhất
                    Grid grid2 = grid1.FindChild<Grid>(null);

                    if (grid2 != null)
                    {
                        // Lấy ra đối tượng Grid thứ ba từ Grid thứ hai
                        Grid grid3 = grid2.FindChild<Grid>(null);

                        if (grid3 != null)
                        {
                            // Tìm RichTextBox trong Grid thứ ba
                            RichTextBox richTextBox = grid3.FindChild<RichTextBox>("richTextbox") as RichTextBox;

                            if (richTextBox != null)
                            {
                                var table = new Table();
                                richTextBox.BeginChange();
                                var gridLenghtConvertor = new GridLengthConverter();
                                table.Columns.Add(new TableColumn());
                                table.RowGroups.Add(new TableRowGroup());
                                for (int i = 0; i < 5; i++)
                                {
                                    TableRow row = new TableRow();
                                    for (int j = 0; j < 2; j++)
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
                        }
                    }

                }
            }
        }
        private bool CanInsertTable(object obj)
        {
            return true;
        }
    }
}
