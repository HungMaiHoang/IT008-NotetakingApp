using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using MongoDB.Driver;
using Note.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows;
using System.Security.Permissions;
using Note.ViewModel;
using Note.View;

namespace Note.Utilities
{
    internal class DataAccess
    {
        // Singleton
        private static DataAccess _instance;
        public static DataAccess Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DataAccess();
                }
                return _instance;
            }
        }

        private const string ConnectionString = "mongodb://localhost:27017";
        private const string DatabaseName = "Note_taking";
        private const string NoteCollection = "Notes";
        private const string rtfDocumentName = "document.rtf";

        private MongoClient client;
        private IMongoDatabase database;
        private GridFSBucket gridFSBucket;
        private DataAccess()
        {
            client = new MongoClient(ConnectionString);
            database = client.GetDatabase(DatabaseName);
            gridFSBucket = new GridFSBucket(database);
        }

        private IMongoCollection<T> ConnectToMongo<T>(in string collection)
        {
            return database.GetCollection<T>(collection);
        }

        public List<NoteModel> GetAllNotes()
        {
            try
            {
                var notesCollection = ConnectToMongo<NoteModel>(NoteCollection);
                var results = notesCollection.Find(_ => true);

                //MessageBox.Show(results.Count().ToString());
                return results.ToList();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public NoteModel GetNoteWithId(ObjectId id)
        {
            var notesCollection = ConnectToMongo<NoteModel>(NoteCollection);
            return notesCollection.Find(p => p.Id == id).FirstOrDefault();
        }

        public Task InsertNote(NoteModel note)
        {
            var notesCollection = ConnectToMongo<NoteModel>(NoteCollection);
            return notesCollection.InsertOneAsync(note);
        }

        public Task UpdateNote(NoteModel note)
        {
            var notesCollection = ConnectToMongo<NoteModel>(NoteCollection);
            var filter = Builders<NoteModel>.Filter.Eq("Id", note.Id);
            return notesCollection.ReplaceOneAsync(filter, note, new ReplaceOptions { IsUpsert = true });
        }
        public Task NoteToTrash(NoteModel note)
        {
            var notesCollection = ConnectToMongo<NoteModel>(NoteCollection);
            var filter = Builders<NoteModel>.Filter.Eq("Id", note.Id);
            var update = Builders<NoteModel>.Update.Set("Status","disable");
            return notesCollection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
        }

        //public Task DeleteNote(NoteModel note)
        //{
        //    //var notesCollection = ConnectToMongo<NoteModel>(NoteCollection);

        //    //gridFSBucket.DeleteAsync(note.FileId);

        //    //return notesCollection.DeleteOneAsync(c => c.Id == note.Id);
        //}
        public Task DeleteNote(ObjectId id)
        {
            var notesCollection = ConnectToMongo<NoteModel>(NoteCollection);

            NoteModel temp = GetNoteWithId(id);
            if (temp != null)
            {
                MessageBox.Show("deleting");
                gridFSBucket.DeleteAsync(temp.FileId);
            }

            return notesCollection.DeleteOneAsync(c => c.Id == id);
        }

        #region GridFS
        /// <summary>
        /// Please do not use this outside NoteModel/Create new rtf file in database
        /// </summary>
        /// <param name="rtfContent"></param>
        public ObjectId CreateRTFNote(TextRange rtfContent)
        {

            using (var rtfMemoryStream = new MemoryStream())
            {
                rtfContent.Save(rtfMemoryStream, DataFormats.Rtf);
                rtfMemoryStream.Seek(0, SeekOrigin.Begin);

                // GridFs update operation
                var fileId = gridFSBucket.UploadFromStream(rtfDocumentName, rtfMemoryStream);
                return fileId;
            }
        }

        /// <summary>
        /// Update rtf file with Id in database
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="rtfContent"></param>
        public async void UpdateRTFNote(ObjectId fileId, TextRange rtfContent)
        {
            // Delete Old file
            gridFSBucket.Delete(fileId);

            using (var rtfMemoryStream = new MemoryStream())
            {
                rtfContent.Save(rtfMemoryStream, DataFormats.Rtf);
                rtfMemoryStream.Seek(0, SeekOrigin.Begin);
                
                // GridFS update operation
                await Task.Run(() =>
                {
                    gridFSBucket.UploadFromStream(fileId, rtfDocumentName, rtfMemoryStream);
                });
            }
        }

        /// <summary>
        /// Load rtf file with Id
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="rtfContent"></param>
        public async void LoadRTFNote(ObjectId fileId, TextRange rtfContent)
        {
            // GridFS download operation
            var rtfMemoryStream = new MemoryStream();
                await gridFSBucket.DownloadToStreamAsync(fileId, rtfMemoryStream);

            // Set RTF Content
            rtfMemoryStream.Seek(0, SeekOrigin.Begin);
            rtfContent.Load(rtfMemoryStream, DataFormats.Rtf);
        }
        #endregion
    }
}
