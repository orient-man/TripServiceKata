using System;
using System.Collections.Generic;
using FluentAssertions;
using LegacyApp.Core.Models;
using LegacyApp.Core.Services;
using NUnit.Framework;

namespace LegacyApp.Core.Tests
{
    [TestFixture]
    public class TripServiceTests
    {
        private static User loggedInUser;
        private readonly User UnusedUser = null;
        private readonly User Guest = null;
        private readonly User RegisteredUser = new User { Name = "Alice" };
        private readonly User AnotherUser = new User { Name = "Bob" };
        private readonly Trip ToBrazil = new Trip();
        private readonly Trip ToLondon = new Trip();
        private TripService service;

        [SetUp]
        public void SetUpEachTest()
        {
            service = new TestingTripService();
            loggedInUser = RegisteredUser;
        }

        [Test]
        public void should_throw_an_exception_when_not_logged_in()
        {
            // arrange
            loggedInUser = Guest;

            // act
            Action act = () => service.GetTripsByUser(UnusedUser);

            // assert
            act.ShouldThrow<UserNotLoggedInException>();
        }

        [Test]
        public void should_not_return_any_trips_when_users_are_not_friends()
        {
            // arrange
            var friend = new User();
            friend.Friends.Add(AnotherUser);
            friend.Trips.Add(ToBrazil);

            // act & assert
            service.GetTripsByUser(friend).Should().BeEmpty();
        }

        [Test]
        public void should_return_friend_trips_when_users_are_friends()
        {
            // arrange
            var friend = new User();
            friend.Friends.Add(AnotherUser);
            friend.Friends.Add(RegisteredUser);
            friend.Trips.Add(ToBrazil);
            friend.Trips.Add(ToLondon);

            // act & assert
            service.GetTripsByUser(friend).Should().HaveCount(2);
        }


        private class TestingTripService : TripService
        {
            protected override List<Trip> FindTripsByUser(User user)
            {
                return user.Trips;
            }

            protected override User GetLoggedInUser()
            {
                return loggedInUser;
            }
        }
    }
}