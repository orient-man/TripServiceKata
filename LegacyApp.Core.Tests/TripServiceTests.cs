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
        private readonly User UnusedUser = null;
        private readonly User Guest = null;
        private readonly User RegisteredUser = new UserBuilder().Build();
        private readonly User AnotherUser = new UserBuilder().Build();
        private readonly Trip ToBrazil = new Trip();
        private readonly Trip ToLondon = new Trip();
        private TripService service;

        [SetUp]
        public void SetUpEachTest()
        {
            service = new TestingTripService();
        }

        [Test]
        public void should_throw_an_exception_when_not_logged_in()
        {
            // act
            Action act = () => service.GetTripsByUser(UnusedUser, Guest);

            // assert
            act.ShouldThrow<UserNotLoggedInException>();
        }

        [Test]
        public void should_not_return_any_trips_when_users_are_not_friends()
        {
            // arrange
            var friend =
                new UserBuilder().FriendsWith(AnotherUser).WithTrips(ToBrazil).Build();

            // act & assert
            service.GetTripsByUser(friend, RegisteredUser).Should().BeEmpty();
        }

        [Test]
        public void should_return_friend_trips_when_users_are_friends()
        {
            // arrange
            var friend = new UserBuilder()
                .FriendsWith(AnotherUser, RegisteredUser)
                .WithTrips(ToBrazil, ToLondon)
                .Build();

            // act & assert
            service.GetTripsByUser(friend, RegisteredUser).Should().HaveCount(2);
        }

        private class TestingTripService : TripService
        {
            protected override List<Trip> FindTripsByUser(User user)
            {
                return user.Trips;
            }
        }
    }
}