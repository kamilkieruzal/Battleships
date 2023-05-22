using Battleships.Core.Structs;
using Battleships.Core.Utility;

namespace Battleships.Core
{
    public class Game
    {
        private readonly GameObjectsPool gameObjectsPool;
        private readonly Field[][] gameArea;

        public Game(GameObjectsPool gameObjectsPool)
        {
            this.gameObjectsPool = gameObjectsPool;

            gameArea = new Field[GameConsts.SizeOfArena][];
            for (var i = 0; i < GameConsts.SizeOfArena; i++)
                gameArea[i] = new Field[GameConsts.SizeOfArena];

            InitializeGameArea();
        }

        public Field[][] GameArea => gameArea;

        public Field Hit(Coordinates coords)
        {
            if (gameArea[coords.X][coords.Y].FieldType != FieldType.Empty)
                gameArea[coords.X][coords.Y].FieldType = FieldType.Hit;

            if (gameArea[coords.X][coords.Y] is ShipField s)
            {
                if(s.PartOf.ShipFields.All(x => gameArea[x.Coordinates.X][x.Coordinates.Y].FieldType == FieldType.Hit))
                    foreach (var field in s.PartOf.ShipFields)
                        gameArea[field.Coordinates.X][field.Coordinates.Y].FieldType = FieldType.Destroyed;
            }

            return gameArea[coords.X][coords.Y];
        }

        public bool AnyShipsLeft()
        {
            for (var i = 0; i < GameConsts.SizeOfArena; i++)
                for (var j = 0; j < GameConsts.SizeOfArena; j++)
                    if (gameArea[i][j].FieldType != FieldType.Destroyed && gameArea[i][j].FieldType != FieldType.Empty) return true;
            return false;
        }

        private void InitializeGameArea()
        {
            for (var i = 0; i < GameConsts.SizeOfArena; i++)
                for (var j = 0; j < GameConsts.SizeOfArena; j++)
                    gameArea[i][j] = new Sea();

            var random = new Random();

            foreach (var ship in gameObjectsPool.TakeAllShips())
            {
                if (random.Next(2) % 2 == 0)
                    ship.Rotate();

                int x = 0, y = 0;

                var placedCorrectly = false;
                do
                {
                    switch (ship.Direction)
                    {
                        case Direction.Horizonal:
                            x = random.Next(GameConsts.SizeOfArena - ship.Length) + 1;
                            y = random.Next(GameConsts.SizeOfArena);
                            break;
                        case Direction.Vertical:
                            x = random.Next(GameConsts.SizeOfArena);
                            y = random.Next(GameConsts.SizeOfArena - ship.Length) + 1;
                            break;
                    }
                    placedCorrectly = TryPlaceShip(new Coordinates(x, y), ship);
                } 
                while (!placedCorrectly);
            }
        }

        private bool TryPlaceShip(Coordinates coords, Ship ship)
        {
            var shipFields = new List<ShipField>();
            Coordinates newShipCoordinates;
            ShipField shipField;

            if (ship.ShipFields.Any())
            {
                shipFields = ship.ShipFields.ToList();
            }
            else
            {
                for (var i = 0; i < ship.Length; i++)
                {
                    switch (ship.Direction)
                    {
                        case Direction.Horizonal:
                            newShipCoordinates = new Coordinates(coords.X + i, coords.Y);
                            if (!CanPlaceShip(newShipCoordinates, ship)) return false;

                            shipField = new ShipField(newShipCoordinates, ship);
                            shipFields.Add(shipField);
                            break;
                        case Direction.Vertical:
                            newShipCoordinates = new Coordinates(coords.X, coords.Y + i);
                            if (!CanPlaceShip(newShipCoordinates, ship)) return false;

                            shipField = new ShipField(newShipCoordinates, ship);
                            shipFields.Add(shipField);
                            break;
                    }
                }
            }

            foreach (var field in shipFields)
                gameArea[field.Coordinates.X][field.Coordinates.Y] = field;

            ship.ShipFields = shipFields;
            return true;
        }

        private bool CanPlaceShip(Coordinates coords, Ship ship)
        {
            if (ship.Direction == Direction.Horizonal)
            {
                for (var i = 0; i < ship.Length; i++)
                    if (!IsFieldWithSourroundingsEmpty(new Coordinates(coords.X + i, coords.Y)))
                        return false;
            }
            else
            {
                for (var i = 0; i < ship.Length; i++)
                    if (!IsFieldWithSourroundingsEmpty(new Coordinates(coords.X, coords.Y + i)))
                        return false;
            }

            return true;
        }

        private bool IsFieldWithSourroundingsEmpty(Coordinates coords)
        {
            foreach (var coordinates in coords.AddSourrondingCoordinates())
            {
                if (gameArea.ElementAtOrDefault(coordinates.X)?.ElementAtOrDefault(coordinates.Y) != null)
                    if (gameArea[coordinates.X][coordinates.Y].FieldType != FieldType.Empty)
                        return false;
            }
            return true;
        }
    }
}
