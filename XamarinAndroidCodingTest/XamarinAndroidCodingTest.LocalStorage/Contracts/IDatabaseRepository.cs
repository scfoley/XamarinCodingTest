using SQLite;

namespace XamarinAndroidCodingTest.LocalStorage.Contracts
{
    public interface IDatabaseRepository
    {
        /// <summary>
        /// Used to retrieve a connection the the local database
        /// </summary>
        /// <returns></returns>
        SQLiteConnection GetConnection();
    }
}
