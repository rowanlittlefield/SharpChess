using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpChess;

namespace Tests
{
    [TestClass]
    public class PieceColorTests
    {
        [TestMethod]
        public void GetOpposingColor()
        {
            var data = new (PieceColor, PieceColor)[] {
                // (color, expected)
                (PieceColor.White, PieceColor.Black),
                (PieceColor.Black, PieceColor.White),
                (PieceColor.Null, PieceColor.Null),
            };

            foreach (var (color, expected) in data)
            {
                var actual = color.GetOpposingColor();

                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void ToToken()
        {
            var data = new (PieceColor, string)[] {
                // (color, expected)
                (PieceColor.White, "w"),
                (PieceColor.Black, "b"),
                (PieceColor.Null, " "),
            };

            foreach (var (color, expected) in data)
            {
                var actual = color.ToToken();

                Assert.AreEqual(expected, actual);
            }
        }
    }
}
