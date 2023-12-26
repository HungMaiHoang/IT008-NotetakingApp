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
    /// Interaction logic for InsertTableWindow.xaml
    /// </summary>
    public partial class InsertTableWindow : Window
    {
        private int Columns;
        private int Rows;
        public InsertTableWindow()
        {
            InitializeComponent();
            
        }
        public int GetColumns()
        {
            return Columns;
        }
        public int GetRows()
        {
            return Rows;
        }

        private void GetColumnsAndRows(object sender, RoutedEventArgs e)
        {
            Columns=Int32.Parse(NumberofColumns.Text);
            Rows = Int32.Parse(NumberofRows.Text);
            this.Close();
        }
    }
}
