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
        [Test]
        public void should_throw_an_exception_when_not_logged_in()
        {
            // arrange
            var service = new TestingTripService();

            // act
            Action act = () => service.GetTripsByUser(null);

            // assert
            act.ShouldThrow<UserNotLoggedInException>();
        }

        class TestingTripService : TripService
        {
            protected override User GetLoggedInUser()
            {
                return null;
            }
        }
    }
}