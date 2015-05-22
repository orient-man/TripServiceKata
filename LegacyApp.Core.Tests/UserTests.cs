using FluentAssertions;
using LegacyApp.Core.Models;
using NUnit.Framework;

namespace LegacyApp.Core.Tests
{
    [TestFixture]
    public class UserTests
    {
        private readonly User Bob = new UserBuilder().WithName("Bob").Build();
        private readonly User Alice = new UserBuilder().WithName("Alice").Build();

        [Test]
        public void should_inform_when_users_are_not_friends()
        {
            // arrange
            var user = new UserBuilder().FriendsWith(Bob).Build();

            // act & assert
            user.IsFriendWith(Alice).Should().BeFalse();
        }

        [Test]
        public void should_inform_when_user_are_friends()
        {
            // arrange
            var user = new UserBuilder().FriendsWith(Alice, Bob).Build();

            // act & assert
            user.IsFriendWith(Alice).Should().BeTrue();
        }
    }
}