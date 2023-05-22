using Battleships.Core.Structs;
using Battleships.Core.Utility;

namespace Battleships.Tests
{
    [TestClass]
    public class CoordinatesUtilityTests
    {
        [TestMethod]
        public void CoordinatesUtilityTests_AddSourrondingCoordinates()
        {
            var coordinates = new Coordinates(5, 5);

            var result = coordinates.AddSourrondingCoordinates();

            Assert.AreEqual(9, result.Count);

            Assert.AreEqual(new Coordinates(5, 5), result[0]);
            Assert.AreEqual(new Coordinates(5, 6), result[1]);
            Assert.AreEqual(new Coordinates(5, 4), result[2]);
            Assert.AreEqual(new Coordinates(6, 5), result[3]);
            Assert.AreEqual(new Coordinates(4, 5), result[4]);
            Assert.AreEqual(new Coordinates(4, 6), result[5]);
            Assert.AreEqual(new Coordinates(6, 4), result[6]);
            Assert.AreEqual(new Coordinates(6, 6), result[7]);
            Assert.AreEqual(new Coordinates(4, 4), result[8]);
        }
    }
}