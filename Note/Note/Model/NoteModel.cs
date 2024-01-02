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
    public class NoteModel : ViewModelBase
    {
        private ObjectId id;
        private string title;
        private DateTime lastEdited;        
        private string headLine;
        private string status;
        private bool isPinned;
        private ObjectId userId;
        private ObjectId fileId;
        private DateTime timeTrash;
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
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(lastEdited, TimeZoneInfo.Local);
            }
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
        [BsonElement("Status")]
        public string Status
        {
            get => status;
            set
            {
                status = value;
                // Update to database
                DataAccess.Instance.UpdateNote(this);
                OnPropertyChanged(nameof(Status));
            }
        }
        [BsonElement("Is Pinned")]
        public bool IsPinned
        {
            get => isPinned;
            set
            {
                isPinned = value;
                // Update to database
                DataAccess.Instance.UpdateNote(this);
                OnPropertyChanged(nameof(IsPinned));
            }
        }
        [BsonElement("User Id")]
        public ObjectId UserId
        {
            get => userId;
            set
            {
                userId = value;
                // Update to database
                DataAccess.Instance.UpdateNote(this);
                OnPropertyChanged(nameof(UserId));
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
        [BsonElement("Time Trash")]
        public DateTime TimeTrash
        {
            get => timeTrash;
            set
            {
                timeTrash = value;
                //update to database
                DataAccess.Instance.UpdateNote(this);
                OnPropertyChanged(nameof(TimeTrash));
            }
        }
        public NoteModel()
        {

        }

        public static NoteModel CreateNewNote(UserModel user)
        {
            return new NoteModel {
                Id = ObjectId.GenerateNewId(),
                Title = "Title",
                LastEdited = DateTime.UtcNow,
                HeadLine = "",
                Status="enable",
                IsPinned=false,
                TimeTrash = DateTime.MaxValue,
                UserId = user.Id,
                FileId = createFileID()
            };
        }
        public static ObjectId createFileID()
        {
            FlowDocument doc = new FlowDocument();
            return DataAccess.Instance.CreateRTFNote(doc);
        }
    }
}
