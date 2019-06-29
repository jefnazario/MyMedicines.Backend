using System;
using MongoDB.Driver;

namespace AzureFunctions.MyMedicine
{
    public static class MongoDatabase
    {
        public static string connectionString = "mongodb://<dbuser>:<dbpassword>@ds060478.mlab.com:60478/mymedicineapps";
        public static string name = "mymedicineapps";
        public static string documentName = "Items";
    }
}