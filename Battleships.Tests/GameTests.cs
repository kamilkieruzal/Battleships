using Battleships.Core;
using Battleships.Core.Structs;
using Moq;

namespace Battleships.Tests
{
    [TestClass]
    public class GameTests
    {
        Game testObject;
        Mock<GameObjectsPool> gameObjectsPoolMock;

        [TestInitialize]
        public void Setup()
        {
            gameObjectsPoolMock = new Mock<GameObjectsPool>();
            gameObjectsPoolMock.Setup(x => x.TakeAllShips())
                .Returns(new List<Ship>
                {
                    GetShipWithCoords(0, 0, Direction.Horizonal, ShipType.Battleship)
                });

            testObject = new Game(gameObjectsPoolMock.Object);
        }

        [TestMethod]
        public void GameTests_TestIfHitsCorrectly()
        {
            Assert.AreEqual(FieldType.Battleship, testObject.GameArea[0][0].FieldType);

            testObject.Hit(new Coordinates(0, 0));

            Assert.AreEqual(FieldType.Hit, testObject.GameArea[0][0].FieldType);
        }

        [TestMethod]
        public void GameTests_TestIfDestroysCorrectly()
        {
            Assert.AreEqual(FieldType.Battleship, testObject.GameArea[0][0].FieldType);
            Assert.AreEqual(FieldType.Battleship, testObject.GameArea[1][0].FieldType);
            Assert.AreEqual(FieldType.Battleship, testObject.GameArea[2][0].FieldType);
            Assert.AreEqual(FieldType.Battleship, testObject.GameArea[3][0].FieldType);
            Assert.AreEqual(FieldType.Battleship, testObject.GameArea[4][0].FieldType);

            testObject.Hit(new Coordinates(0, 0));
            testObject.Hit(new Coordinates(1, 0));
            testObject.Hit(new Coordinates(2, 0));
            testObject.Hit(new Coordinates(3, 0));
            testObject.Hit(new Coordinates(4, 0));

            Assert.AreEqual(FieldType.Destroyed, testObject.GameArea[0][0].FieldType);
        }

        [TestMethod]
        public void GameTests_TestIfMissessCorrectly()
        {
            Assert.AreEqual(FieldType.Empty, testObject.GameArea[0][5].FieldType);

            testObject.Hit(new Coordinates(0, 5));

            Assert.AreEqual(FieldType.Empty, testObject.GameArea[0][5].FieldType);
        }

        private static Ship GetShipWithCoords(int x, int y, Direction direction, ShipType type)
        {
            var result = new Ship(type, direction);
            int shipLength = 0;

            switch (type)
            {
                case ShipType.Battleship:
                    shipLength = GameConsts.SizeOfBattleship;
                    break;
                case ShipType.Destroyer:
                    shipLength = GameConsts.SizeOfDestroyer;
                    break;
            }

            ShipField shipField = null;
            for (var i = 0; i < shipLength; i++)
            {
                switch (direction)
                {
                    case Direction.Horizonal:
                        shipField = new ShipField(new Coordinates(x + i, y), result);
                        break;
                    case Direction.Vertical:

                        shipField = new ShipField(new Coordinates(x, y + i), result);
                        break;
                }

                result.ShipFields.Add(shipField);
            }

            return result;
        }
    }
}