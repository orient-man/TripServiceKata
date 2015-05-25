using System;
using System.Collections.Generic;
using LegacyApp.Core.Models;

namespace LegacyApp.Core.Dao
{
    public class TripDao : ITripDao
    {
        public List<Trip> FindTripsByUser(User user)
        {
            return FindTripsByUserStatic(user);
        }

        public static List<Trip> FindTripsByUserStatic(User user)
        {
            // SQL query here
            throw new NotImplementedException();
        }
    }
}