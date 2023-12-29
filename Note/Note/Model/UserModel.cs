using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.Core.Authentication;
using Note.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Model
{
    public class UserModel : ViewModelBase
    {
        private ObjectId id;
        private string name;
        private string userName;
        private string password;
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
        [BsonElement("Name")]
        public string Name
        {
            get => name;
            set
            {
                name = value;
                // Update to database
                DataAccess.Instance.UpdateUser(this);
                OnPropertyChanged(nameof(Name));
            }
        }
        [BsonElement("UserName")]
        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                // Update to database
                DataAccess.Instance.UpdateUser(this);
                OnPropertyChanged(nameof(UserName));
            }
        }
        [BsonElement("Password")]
        public string Password
        {
            get => password;
            set
            {
                password = value;
                // Update to database
                DataAccess.Instance.UpdateUser(this);
                OnPropertyChanged(nameof(Password));
            }
        }
        public static UserModel CreateNewUser()
        {
            return new UserModel
            {
                Id = ObjectId.GenerateNewId(),
                Name = "",
                UserName = "",
                Password = "",
            };
        }
    }
}
