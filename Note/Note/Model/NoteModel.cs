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
        public ObjectId Id 
        { 
            get => id; 
            set 
            { 
                id = value; 
                OnPropertyChanged(nameof(Id)); 
            } 
        }

        [BsonElement("Title")]
        public string Title 
        { 
            get => title; 
            set 
            { 
                title = value; 
                // Update to database
                DataAccess.Instance.UpdateNote(this);
                OnPropertyChanged(nameof(Title)); 
            }
        }
        [BsonElement("Last Edited")]
        public DateTime LastEdited 
        { 
            get => lastEdited; 
            set 
            { 
                lastEdited = value;
                // Update to database
                DataAccess.Instance.UpdateNote(this);
                OnPropertyChanged(nameof(LastEdited)); 
            } 
        }
        [BsonElement("Head Line")]
        public string HeadLine 
        { 
            get => headLine; 
            set 
            { 
                headLine = value;
                // Update to database
                DataAccess.Instance.UpdateNote(this);
                OnPropertyChanged(nameof(HeadLine)); 
            } 
        }
        [BsonElement("File Id")]
        public ObjectId FileId 
        { 
            get => fileId; 
            set 
            { 
                fileId = value;
                // Update to database
                DataAccess.Instance.UpdateNote(this);
                OnPropertyChanged(nameof(FileId)); 
            } 
        }

        public NoteModel()
        {
            // Create base property
            //Id = ObjectId.GenerateNewId();
            //Title = "Title";
            //LastEdited = DateTime.Now;
            //HeadLine = "";
            //FlowDocument doc = new FlowDocument();
            //TextRange rtfContent = new TextRange(doc.ContentStart, doc.ContentEnd);

            //FileId = DataAccess.Instance.CreateRTFNote(rtfContent);
        }

        public static NoteModel CreateNewNote()
        {
            return new NoteModel {
                Id = ObjectId.GenerateNewId(),
                Title = "Title",
                LastEdited = DateTime.Now,
                HeadLine = "",
                FileId = createFileID()
            };
        }
        public static ObjectId createFileID()
        {
            FlowDocument doc = new FlowDocument();
            TextRange rtfContent = new TextRange(doc.ContentStart, doc.ContentEnd);
            rtfContent.Text = "Start Texting....";
            return DataAccess.Instance.CreateRTFNote(rtfContent);
        }
    }
}
