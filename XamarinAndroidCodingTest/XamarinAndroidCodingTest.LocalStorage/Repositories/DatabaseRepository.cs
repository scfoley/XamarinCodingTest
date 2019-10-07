using System;
using System.IO;
using SQLite;
using XamarinAndroidCodingTest.LocalStorage.Contracts;

namespace XamarinAndroidCodingTest.LocalStorage.Repositories
{
    public class DatabaseRepository : IDatabaseRepository
    {
        private readonly SQLiteConnection connection;

        public DatabaseRepository()
        {
            // Get database path
            var databasePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal), 
                "temp.db3");

            // Set database connection
            connection = new SQLiteConnection(databasePath);
        }

        public SQLiteConnection GetConnection()
        {
            return connection;
        }
    }
}
