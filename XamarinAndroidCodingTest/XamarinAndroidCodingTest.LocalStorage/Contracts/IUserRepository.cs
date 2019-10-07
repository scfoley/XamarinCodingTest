using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinAndroidCodingTest.Entity;

namespace XamarinAndroidCodingTest.LocalStorage.Contracts
{
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves all users from local storage
        /// </summary>
        /// <returns></returns>
        List<User> GetUsers();

        /// <summary>
        /// Saves a user into local storage
        /// </summary>
        /// <param name="user"></param>
        void InsertUser(User user);
    }
}
