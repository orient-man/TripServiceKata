using System.Collections.Generic;
using LegacyApp.Core.Dao;
using LegacyApp.Core.Models;

namespace LegacyApp.Core.Services
{
    public class TripService
    {
        public List<Trip> GetTripsByUser(User user, User loggedInUser)
        {
            if (loggedInUser == null)
                throw new UserNotLoggedInException();

            return user.IsFriendWith(loggedInUser)
                ? FindTripsByUser(user)
                : NoTrips();
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