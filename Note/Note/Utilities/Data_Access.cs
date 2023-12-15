using MongoDB.Driver;
using Note.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
