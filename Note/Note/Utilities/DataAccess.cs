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
using System.Windows.Markup;

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
        public ObjectId CreateRTFNote(FlowDocument flowDocument)
        {
            string xamlString = XamlWriter.Save(flowDocument);
            byte[] xamlBytes = System.Text.Encoding.UTF8.GetBytes(xamlString);

            // GridFs update operation
            ObjectId fileId = gridFSBucket.UploadFromBytes(rtfDocumentName, xamlBytes);
            return fileId;
        }

        /// <summary>
        /// Update rtf file with Id in database
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="rtfContent"></param>
        public async void UpdateRTFNote(ObjectId fileId, FlowDocument flowDocument)
        {
            // Delete Old file
            gridFSBucket.Delete(fileId);

            string xamlString = XamlWriter.Save(flowDocument);
            byte[] xamlBytes = System.Text.Encoding.UTF8.GetBytes(xamlString);
            // GridFS update operation
            await Task.Run(() =>
            {
                gridFSBucket.UploadFromBytesAsync(fileId, rtfDocumentName, xamlBytes);
            });
        }

        /// <summary>
        /// Load rtf file with Id
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="rtfContent"></param>
        public FlowDocument LoadRTFNote(ObjectId fileId)
        {
            // GridFS download operation
            byte[] xamlBytes = gridFSBucket.DownloadAsBytes(fileId);

            string xamlString = System.Text.Encoding.UTF8.GetString(xamlBytes);
            FlowDocument flowDocument = (FlowDocument)XamlReader.Parse(xamlString);
            return flowDocument;
        }
        #endregion
    }
}
