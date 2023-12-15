using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using Note.Model;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Utilities
{
    internal class DataAccess
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

        public async Task<List<NoteModel>> GetAllNotes()
        {
            var notesCollection = ConnectToMongo<NoteModel>(NoteCollection);
            var results = await notesCollection.FindAsync(_ => true);
            return results.ToList();
        }

        public Task CreateNote(NoteModel note)
        {
            var notesCollection = ConnectToMongo<NoteModel>(NoteCollection);
            var filter = Builders<NoteModel>.Filter.Eq("Id", note.Id);
            return notesCollection.ReplaceOneAsync(filter, note, new ReplaceOptions { IsUpsert = true });
        }

        public Task DeleteNote(NoteModel note)
        {
            var notesCollection = ConnectToMongo<NoteModel>(NoteCollection);
            return notesCollection.DeleteOneAsync(c => c.Id == note.Id);
        }
    }

}
