using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_NTier_DomainLayer;
using MongoDB.Driver;

namespace Demo_NTier_DataAccessLayer
{
    public class MongoDbPersonRepository : IPersonRepository
    {
        List<Person> _persons;

        public MongoDbPersonRepository()
        {
            _persons = new List<Person>();
        }

        public IEnumerable<Person> GetAll(out DalErrorCode dalErrorCode)
        {
            try
            {
                var personCollection = GetPersonCollection();

                _persons = personCollection.FindSync(Builders<Person>.Filter.Empty).ToList();

                dalErrorCode = DalErrorCode.GOOD;
            }
            catch (Exception)
            {
                dalErrorCode = DalErrorCode.ERROR;
            }

            return _persons;
        }

        public Person GetById(int id, out DalErrorCode dalErrorCode)
        {
            Person person = null;

            try
            {
                var personCollection = GetPersonCollection();

                person = personCollection.Find(Builders<Person>.Filter.Eq("Id", id)).SingleOrDefault() as Person;

                dalErrorCode = DalErrorCode.GOOD;
            }
            catch (Exception)
            {
                dalErrorCode = DalErrorCode.ERROR;
            }

            return person;
        }

        public void Insert(Person person, out DalErrorCode dalErrorCode)
        {
            try
            {
                var personCollection = GetPersonCollection();

                personCollection.InsertOne(person);

                dalErrorCode = DalErrorCode.GOOD;
            }
            catch (Exception)
            {
                dalErrorCode = DalErrorCode.ERROR;
            }
        }

        public void Update(Person person, out DalErrorCode dalErrorCode)
        {
            try
            {
                var personCollection = GetPersonCollection();

                //characterList.Find(Builders<Character>.Filter.Eq("id", character.Id)). UpdateOne;
                personCollection.FindOneAndReplace(Builders<Person>.Filter.Eq("Id", person.Id), person);

                dalErrorCode = DalErrorCode.GOOD;
            }
            catch (Exception)
            {
                dalErrorCode = DalErrorCode.ERROR;
            }
        }

        public void Delete(int id, out DalErrorCode dalErrorCode)
        {
            try
            {
                var personCollection = GetPersonCollection();

                var result = personCollection.DeleteOne(Builders<Person>.Filter.Eq("Id", id));

                dalErrorCode = DalErrorCode.GOOD;
            }
            catch (Exception)
            {
                dalErrorCode = DalErrorCode.ERROR;
            }
        }

        private IMongoCollection<Person> GetPersonCollection()
        {
            var client = new MongoClient(MongoDbDataSettings.connectionString);
            IMongoDatabase database = client.GetDatabase(MongoDbDataSettings.databaseName);
            IMongoCollection<Person> personCollection = database.GetCollection<Person>(MongoDbDataSettings.personCollectionName);

            return personCollection;
        }

        public void Dispose()
        {
            _persons = null;
        }
    }
}
