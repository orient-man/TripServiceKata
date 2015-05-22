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

        class TestingTripService : TripService
        {
            protected override User GetLoggedInUser()
            {
                return loggedInUser;
            }
        }
    }
}