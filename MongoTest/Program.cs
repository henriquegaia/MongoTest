using System;
using MongoDB.Driver;

namespace mongodb_tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MongoCRUD db = new MongoCRUD("AddressBook");
            Console.ReadLine();
        }
    }

    public class MongoCRUD
    {
        private IMongoDatabase db;
        
        public MongoCRUD(string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database); ;
        }
    }

}
