using LegacyApp.Core.Models;

namespace LegacyApp.Core.Dao
{
    public class UserDao
    {
        public static User GetByName(string name)
        {
            // stinking SQL query here
            return new User { Name = name };
        }
    }
}