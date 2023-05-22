using Battleships.Core.Structs;
using Battleships.Core.Utility;

namespace Battleships.Core
{
    public class GameObjectsPool
    {
        private IList<Ship> ships;

        public GameObjectsPool()
        {
            ships = new List<Ship>();

            for (var i = 0; i < GameConsts.NumberOfBattleships; i++)
                ships.Add(new Ship(ShipType.Battleship));
            for (var i = 0; i < GameConsts.NumberOfDestroyers; i++)
                ships.Add(new Ship(ShipType.Destroyer));
        }

        public virtual Ship TakeShip(ShipType? shipType = null)
        {
            var result = shipType == null ? 
                ships.FirstOrDefault() : 
                ships.FirstOrDefault(s => s.ShipType == shipType);

            if (result == null)
                throw new NoMoreShipsException();

            ships.Remove(result);

            return result;
        }

        public virtual IList<Ship> TakeAllShips()
        {
            var result = ships.TakeWhile(x => ships.Any()).ToList();
            ships.Clear();

            return result;
        }
    }
}
