using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace mongodb_tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MongoCRUD db = new MongoCRUD("AddressBook");
            db.InsertRecord("Users", new Person { FirstName = "Jeff", LastName = "Bezos" }); ;
            Console.ReadLine();
        }
    }

    public class Person
    {
        [BsonId]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class MongoCRUD
    {
        private IMongoDatabase db;
        
        public MongoCRUD(string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database); ;
        }

        public void InsertRecord<T>(string table, T record)
        {
            var collection = db.GetCollection<T>(table);
            collection.InsertOne(record);
        }
    }

}
