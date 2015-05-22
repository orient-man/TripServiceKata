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
            List<Trip> tripList = new List<Trip>();
            User loggedUser = null;

            loggedUser = GetLoggedInUser();

            if (loggedUser != null)
            {
                if (user.IsFriendWith(loggedUser))
                {
                    tripList = FindTripsByUser(user);
                }
            }
            else
            {
                throw new UserNotLoggedInException();
            }

            return tripList;
        }

        protected virtual List<Trip> FindTripsByUser(User user)
        {
            return TripDao.FindTripsByUser(user);
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
    }
}