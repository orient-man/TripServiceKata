using System;
using FluentAssertions;
using LegacyApp.Core.Dao;
using LegacyApp.Core.Models;
using LegacyApp.Core.Services;
using Ninject;
using Ninject.MockingKernel.Moq;
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
        private readonly MoqMockingKernel kernel = new MoqMockingKernel();

        [SetUp]
        public void SetUpEachTest()
        {
            kernel.Reset();
        }

        [Test]
        public void should_throw_an_exception_when_not_logged_in()
        {
            // arrange
            var service = kernel.Get<TripService>();

            // act
            Action act = () => service.GetTripsByUser(UnusedUser, Guest);

            // assert
            act.ShouldThrow<UserNotLoggedInException>();
        }

        [Test]
        public void should_not_return_any_trips_when_users_are_not_friends()
        {
            // arrange
            var service = kernel.Get<TripService>();
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
            kernel.GetMock<ITripDao>()
                .Setup(o => o.FindTripsByUser(friend))
                .Returns(friend.Trips);
            var service = kernel.Get<TripService>();

            // act & assert
            service.GetTripsByUser(friend, RegisteredUser).Should().HaveCount(2);
        }
    }
}