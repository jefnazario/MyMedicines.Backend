using System;
using MongoDB.Bson;

namespace AzureFunctions.MyMedicine
{
    public class Items<T> where T: class
    {
        public ObjectId id { get; set; }
        public string entity { get; set; }
        public T attributes { get; set; }
        public DateTime createdAt { get; set; }
        public bool isActive { get; set; }
    }
}

public interface IDomain {
    string name { get; set; }
}