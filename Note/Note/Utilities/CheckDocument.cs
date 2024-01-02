using MongoDB.Bson;
using MongoDB.Driver;
using Note.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Note.Utilities
{
    public class CheckDocument
    {
         public void CheckAndDeleteDocuments(object state, EventArgs e)
        {
            // Thiết lập kết nối đến MongoDB
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoDB.Driver.MongoClient(connectionString);
            var database = client.GetDatabase("Note_taking");
            var collection = database.GetCollection<NoteModel>("Notes");

            // Lấy ngày và giờ hiện tại
            var currentDateTime = DateTime.UtcNow;

            // Tìm và xóa các tài liệu có thuộc tính datetime ít hơn ngày hiện tại
            var filter = Builders<NoteModel>.Filter.Lt("TimeTrash", currentDateTime.AddSeconds(-int.Parse(UserHolder.CurUser.DayDelete) * 24*60*60));
            var result = collection.Find(filter).ToList();
            foreach ( var item in result )
            {
                DataAccess.Instance.DeleteNote(item.Id);
            }
        }
    }
}
