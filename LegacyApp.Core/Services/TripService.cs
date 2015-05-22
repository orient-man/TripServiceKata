using System.Collections.Generic;
using System.Web;
using LegacyApp.Core.Dao;
using LegacyApp.Core.Models;

namespace LegacyApp.Core.Services
{
    public class TripService
    {
        public List<Trip> GetTripsByUser(User user)
        {
            var loggedUser = GetLoggedInUser();
            if (loggedUser == null)
                throw new UserNotLoggedInException();

            return user.IsFriendWith(loggedUser)
                ? FindTripsByUser(user)
                : NoTrips();
        }

        protected virtual User GetLoggedInUser()
        {
            User loggedUser = null;
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                loggedUser = UserDao.GetByName(
                    HttpContext.Current.User.Identity.Name);
            }
            return loggedUser;
        }

        protected virtual List<Trip> FindTripsByUser(User user)
        {
            return TripDao.FindTripsByUser(user);
        }

        private static List<Trip> NoTrips()
        {
            return new List<Trip>();
        }
    }
}