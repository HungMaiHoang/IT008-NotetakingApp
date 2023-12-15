using MongoDB.Driver;
using Note.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.GridFS;
using System.Windows.Documents;
using System.IO;
using System.Windows;
using MongoDB.Bson;
using System.Windows.Controls;

namespace Note.Utilities
{
    internal class Data_Access
    {
        private const string ConnectionString = "mongodb://localhost:27017";
        private const string DatabaseName = "Note_taking";
        private const string NoteCollection = "Notes";

        private IMongoCollection<T> ConnectToMongo<T>(in string collection)
        {
            var client = new MongoClient(ConnectionString);
            var db = client.GetDatabase(DatabaseName);
            return db.GetCollection<T>(collection);
        }

        public async Task<List<Note_Model>> GetAllNotes()
        {
            var notesCollection = ConnectToMongo<Note_Model>(NoteCollection);
            var results = await notesCollection.FindAsync(_ => true);
            return results.ToList();
        }

        public Task CreateNote(Note_Model note)
        {
            var notesCollection = ConnectToMongo<Note_Model>(NoteCollection);
            var filter = Builders<Note_Model>.Filter.Eq("Id", note.Id);
            return notesCollection.ReplaceOneAsync(filter, note, new ReplaceOptions { IsUpsert = true });
        }

        public Task DeleteNote(Note_Model note)
        {
            var notesCollection = ConnectToMongo<Note_Model>(NoteCollection);
            return notesCollection.DeleteOneAsync(c => c.Id == note.Id);
        }

        ObjectId myId;

        public async void SaveNote(string filename ,TextRange rtfContent)
        {
            var client = new MongoClient(ConnectionString);
            var db = client.GetDatabase(DatabaseName);
            var gridFSBucket = new GridFSBucket(db);

            using (var rtfMemoryStream = new MemoryStream())
            {
                rtfContent.Save(rtfMemoryStream, DataFormats.Rtf);
                rtfMemoryStream.Seek(0, SeekOrigin.Begin);

                var fileId = await Task.Run(() =>
                {
                    return gridFSBucket.UploadFromStream(filename, rtfMemoryStream);
                });

                myId = fileId;
            }
        }

        public async void LoadNote(ObjectId fileId, RichTextBox rtb)
        {
            fileId = myId;

            var client = new MongoClient(ConnectionString);
            var db = client.GetDatabase(DatabaseName);
            var gridFSBucket = new GridFSBucket(db);

            var rtfMemoryStream = new MemoryStream();
            await gridFSBucket.DownloadToStreamAsync(fileId, rtfMemoryStream);

            rtfMemoryStream.Seek(0, SeekOrigin.Begin);
            var rtfRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            rtfRange.Load(rtfMemoryStream, DataFormats.Rtf);
        }
    }
}
