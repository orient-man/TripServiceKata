using System;
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

        [Test]
        public void should_throw_an_exception_when_not_logged_in()
        {
            // arrange
            var service = new TestingTripService();
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
            var service = new TestingTripService();
            loggedInUser = RegisteredUser;
            var friend = new User();
            friend.Friends.Add(AnotherUser);
            friend.Trips.Add(ToBrazil);

            // act & assert
            service.GetTripsByUser(friend).Should().BeEmpty();
        }

        class TestingTripService : TripService
        {
            protected override User GetLoggedInUser()
            {
                return loggedInUser;
            }
        }
    }
}