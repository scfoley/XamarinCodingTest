using System;
using System.IO;
using SQLite;
using XamarinAndroidCodingTest.LocalStorage.Contracts;

namespace XamarinAndroidCodingTest.LocalStorage.Repositories
{
    public class DatabaseRepository : IDatabaseRepository
    {
        private readonly SQLiteConnection database;

        public DatabaseRepository()
        {
            var databasePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal), 
                "temp.db3");

            database = new SQLiteConnection(databasePath);
        }

        public SQLiteConnection GetConnection()
        {
            return database;
        }
    }
}
