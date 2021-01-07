using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpChess;

namespace Tests
{
    [TestClass]
    public class UserActionTests
    {
        [TestMethod]
        public void FlipVertically_UpAndDown()
        {
            var data = new (UserAction, UserAction)[] {
                // (userAction, expected)
                (UserAction.Up, UserAction.Down),
                (UserAction.Down, UserAction.Up),
            };

            foreach (var (userAction, expected) in data)
            {
                var actual = userAction.FlipVertically();

                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void FlipVertically_NotUpOrDown()
        {
            var userActions = Enum.GetValues(typeof(UserAction));
            var query = from UserAction userAction in userActions select userAction;
            var filteredUserActions = query
                .Where(userAction => userAction != UserAction.Up
                && userAction != UserAction.Down);

            foreach (var userAction in filteredUserActions)
            {
                var expected = userAction;

                var actual = userAction.FlipVertically();

                Assert.AreEqual(expected, actual);
            }
        }
    }
}
