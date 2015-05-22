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
            bool isFriend = false;

            if (HttpContext.Current.Request.IsAuthenticated)
            {
                loggedUser = UserDao.GetByName(
                    HttpContext.Current.User.Identity.Name);
            }

            if (loggedUser != null)
            {
                foreach (User friend in user.Friends)
                {
                    if (friend.Equals(loggedUser))
                    {
                        isFriend = true;
                        break;
                    }
                }

                if (isFriend)
                {
                    tripList = TripDao.FindTripsByUser(user);
                }
            }
            else
            {
                throw new UserNotLoggedInException();
            }

            return tripList;
        }
    }
}