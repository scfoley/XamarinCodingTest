using SQLite;

namespace XamarinAndroidCodingTest.LocalStorage.Contracts
{
    public interface IDatabaseRepository
    {
        SQLiteConnection GetConnection();
    }
}
