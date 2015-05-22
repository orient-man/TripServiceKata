using System;
using System.Collections.Generic;
using LegacyApp.Core.Models;

namespace LegacyApp.Core.Tests
{
    public class UserBuilder
    {
        private string _name;
        private readonly List<User> _friends = new List<User>();
        private readonly List<Trip> _trips = new List<Trip>();

        public UserBuilder()
        {
            WithName(Guid.NewGuid().ToString());
        }

        public UserBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public UserBuilder FriendsWith(params User[] friends)
        {
            _friends.AddRange(friends);
            return this;
        }

        public UserBuilder WithTrips(params Trip[] trips)
        {
            _trips.AddRange(trips);
            return this;
        }

        public User Build()
        {
            var user = new User { Name = _name };
            user.Trips.AddRange(_trips);
            user.Friends.AddRange(_friends);
            return user;
        }
    }
}