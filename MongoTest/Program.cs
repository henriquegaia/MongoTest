using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace mongodb_tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("starting ...");
            string database = "AddressBook";
            MongoCRUD db = new MongoCRUD(database);
            var address = new Address { Street = "Gold", City = "PH", State = null, ZipCode = "1234" };

            db.InsertRecord("Users", new Person
            {
                FirstName = "Jeff",
                LastName = "Bezos",
                PrimaryAddress = address
            });

            var allRecords = db.LoadRecords<Person>("Users");

            foreach (var record in allRecords)
            {
                Console.WriteLine(record.FirstName);
            }

            Console.ReadLine();
        }
    }

    public class Person
    {
        //[BsonId]
        //public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address PrimaryAddress { get; set; }
    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
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

        public List<T> LoadRecords<T>(string table)
        {
            var collection = db.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList();
        }
    }

}
