using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using XamarinAndroidCodingTest.Entity;
using XamarinAndroidCodingTest.LocalStorage.Contracts;

namespace XamarinAndroidCodingTest.LocalStorage.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SQLiteConnection _databaseConnection;

        public UserRepository(IDatabaseRepository databaseRepository)
        {
            _databaseConnection = databaseRepository.GetConnection();
            _databaseConnection.CreateTable<User>();
        }

        public bool UserNameExists(string username)
        {
            var matchingUser = _databaseConnection.Table<User>()
                .Where(user => user.UserName == username)
                .FirstOrDefault();

            return matchingUser != null;
        }

        public List<User> GetUsers()
        {
            return _databaseConnection.Query<User>("select * from User");
        }

        public void InsertUser(User user)
        {
            var newUserId = _databaseConnection.Insert(user);
            user.Id = newUserId;
        }
    }
}
