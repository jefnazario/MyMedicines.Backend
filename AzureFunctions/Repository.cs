using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace AzureFunctions.MyMedicine
{
    public class Repository<T> where T: class, IDomain
    {
        internal IMongoDatabase _database;
        internal IMongoCollection<Items<T>> _collections;

         private static readonly Lazy<Repository<T>> Lazy = new Lazy<Repository<T>>(() => new Repository<T>());
        public static Repository<T> Instance => Lazy.Value;

        public Repository()
        {
            var client = new MongoClient(MongoDatabase.connectionString);
            _database = client.GetDatabase(MongoDatabase.name);
            _collections = _database.GetCollection<Items<T>>(MongoDatabase.documentName);
        }

        public async Task<IEnumerable<Items<T>>> FindMany(string itemType, string name)
        {
            var cursor = await _collections.FindAsync(x => 
                                                x.entity == itemType 
                                                && x.attributes.name.ToLowerInvariant().Contains(name.ToLowerInvariant()) 
                                            );
            var item = await cursor.ToListAsync();

            return item;
        }

        public async Task<IEnumerable<Items<T>>> FindMany(string itemType)
        {
            var cursor = await _collections.FindAsync(x => x.entity == itemType);
            var item = await cursor.ToListAsync();

            return item;
        }
    }
}