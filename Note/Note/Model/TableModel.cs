using Note.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace Note.Model
{
    public partial class TableModel:ViewModelBase
    {
        private int numberofColumns;
        private int numberofRows;
        public TableModel() { }
        public int NumberofColumns { get => numberofColumns; set { numberofColumns = value; OnPropertyChanged(nameof(NumberofColumns)); } }
        public int NumberofRows { get => numberofRows; set { numberofRows = value; OnPropertyChanged(nameof(NumberofRows)); } }

    }
}
