using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Note.Utilities;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Note.Model
{
    internal class NoteModel : ViewModelBase
    {
        
        private ObjectId id;
        private string title;
        private DateTime lastEdited;        
        private string headLine;
        private ObjectId fileId;

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get => id; set { id = value; OnPropertyChanged(nameof(Id)); } }

        [BsonElement("Title")]
        public string Title { get => title; set { title = value; OnPropertyChanged(nameof(Title)); } }
        [BsonElement("Last Edited")]
        public DateTime LastEdited { get => lastEdited; set { lastEdited = value; OnPropertyChanged(nameof(LastEdited)); } }
        [BsonElement("Head Line")]
        public string HeadLine { get => headLine; set { headLine = value; OnPropertyChanged(nameof(HeadLine)); } }
        [BsonElement("File Id")]
        public ObjectId FileId { get => fileId; set { fileId = value; OnPropertyChanged(nameof(FileId)); } }
        public NoteModel()
        {
            // Create base property
            Title = "Title";
            LastEdited = DateTime.Now;
            HeadLine = "";

            // Create empty .rtf
            RichTextBox rtb = new RichTextBox();
            TextRange rtfContent = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);

            FileId = DataAccess.Instance.CreateRTFNote(rtfContent);
        }
    }
}
