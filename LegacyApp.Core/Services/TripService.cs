using System.Collections.Generic;
using LegacyApp.Core.Dao;
using LegacyApp.Core.Models;

namespace LegacyApp.Core.Services
{
    public class TripService
    {
        private readonly ITripDao tripDao;

        public TripService(ITripDao tripDao)
        {
            this.tripDao = tripDao;
        }

        public List<Trip> GetFriendTrips(User friend, User loggedInUser)
        {
            if (loggedInUser == null)
                throw new UserNotLoggedInException();

            return friend.IsFriendWith(loggedInUser)
                ? tripDao.FindTripsByUser(friend)
                : NoTrips();
        }

        private static List<Trip> NoTrips()
        {
            return new List<Trip>();
        }
    }
}