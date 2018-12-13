using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using MongoDB.Driver;
using MongoDB.Bson;
using Demo_NTier_DomainLayer;

namespace Demo_NTier_DataAccessLayer
{
    public class MongoDBSimpleDataService : IDataService
    {

        static string _connectionString;

        /// <summary>
        /// read the mongoDb collection and load a list of character objects
        /// </summary>
        /// <returns>list of characters</returns>
        public IEnumerable<Person> ReadAll(out DalErrorCode statusCode)
        {
            List<Person> persons = new List<Person>();

            try
            {
                var client = new MongoClient(_connectionString);
                IMongoDatabase database = client.GetDatabase("cit255");
                IMongoCollection<Person> personList = database.GetCollection<Person>("person");

                persons = personList.Find(Builders<Person>.Filter.Empty).ToList();

                statusCode = DalErrorCode.GOOD;
            }
            catch (Exception)
            {
                statusCode = DalErrorCode.ERROR;
            }

            return persons;
        }

        /// <summary>
        /// write the current list of characters to the mongoDb collection
        /// </summary>
        /// <param name="characters">list of characters</param>
        public void WriteAll(IEnumerable<Person> persons, out DalErrorCode statusCode)
        {
            try
            {
                var client = new MongoClient(_connectionString);
                IMongoDatabase database = client.GetDatabase("cit255");
                IMongoCollection<Person> personList = database.GetCollection<Person>("person");

                //
                // delete all documents in the collection to reset the collection
                //
                personList.DeleteMany(Builders<Person>.Filter.Empty);

                personList.InsertMany(persons);

                statusCode = DalErrorCode.GOOD;
            }
            catch (Exception)
            {
                statusCode = DalErrorCode.ERROR;
            }
        }

        public MongoDBSimpleDataService()
        {
            _connectionString = MongoDbDataSettings.connectionString;
        }
    }
}
