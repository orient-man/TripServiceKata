using System.Collections.Generic;
using LegacyApp.Core.Dao;
using LegacyApp.Core.Models;

namespace LegacyApp.Core.Services
{
    public class TripService
    {
        private readonly ITripDao tripDao;

        public TripService() : this(new TripDao())
        {
        }

        public TripService(ITripDao tripDao)
        {
            this.tripDao = tripDao;
        }

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
            return tripDao.FindTripsByUser(user);
        }

        private static List<Trip> NoTrips()
        {
            return new List<Trip>();
        }
    }
}