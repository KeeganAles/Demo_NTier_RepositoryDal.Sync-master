using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_NTier_DataAccessLayer
{
    public static class MongoDbDataSettings
    {
        public const string connectionString = "mongodb://Koof09:Password123@nmc-shard-00-00-zjzvr.mongodb.net:27017,nmc-shard-00-01-zjzvr.mongodb.net:27017,nmc-shard-00-02-zjzvr.mongodb.net:27017/test?ssl=true&replicaSet=NMC-shard-0&authSource=admin&retryWrites=true";

        public const string databaseName = "cit255";
        public const string personCollectionName = "person";
        public const string characterCollectionName = "person";
    }
}
