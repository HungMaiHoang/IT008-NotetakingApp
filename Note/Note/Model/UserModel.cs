using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.Core.Authentication;
using Note.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
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
        private bool settingAdd;
        private string dayDelete;
        private byte[] image;
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
        [BsonElement("SettingAdd")]
        public bool SettingAdd
        {
            get => settingAdd;
            set
            {
                settingAdd = value;
                // Update to database
                DataAccess.Instance.UpdateUser(this);
                OnPropertyChanged(nameof(SettingAdd));
            }
        }
        [BsonElement("DayDelete")]
        public string DayDelete
        {
            get => dayDelete;
            set
            {
                dayDelete = value;
                // Update to database
                DataAccess.Instance.UpdateUser(this);
                OnPropertyChanged(nameof(DayDelete));
            }
        }

        [BsonElement("Image")]
        public byte[] Image
        {
            get => image;
            set
            {
                image = value;
                // Update to database
                DataAccess.Instance.UpdateUser(this);
                OnPropertyChanged(nameof(Image));
            }
        }
     

        public static UserModel CreateNewUser(string name, string username, string password)
        {
            return new UserModel
            {
                Id = ObjectId.GenerateNewId(),
                Name = name,
                UserName = username,
                Password = password,
                SettingAdd = true,
                DayDelete = "7",
                Image = File.ReadAllBytes("Images/defaultavatar.jpg")
            };
        }
    }
}
