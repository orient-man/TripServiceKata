using System.Collections.Generic;
using LegacyApp.Core.Models;

namespace LegacyApp.Core.Dao
{
    public interface ITripDao
    {
        List<Trip> FindTripsByUser(User user);
    }
}