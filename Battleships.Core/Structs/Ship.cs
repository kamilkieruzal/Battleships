namespace Battleships.Core.Structs
{
    public class Ship
    {
        public Ship(ShipType shipType)
        {
            ShipType = shipType;

            switch (shipType)
            {
                case ShipType.Battleship:
                    Direction = Direction.Horizonal;
                    Length = GameConsts.SizeOfBattleship;

                    break;
                case ShipType.Destroyer:
                    Direction = Direction.Horizonal;
                    Length = GameConsts.SizeOfDestroyer;

                    break;
                default:
                    throw new ArgumentException("Not implemented ship type");
            }
        }


        public ShipType ShipType { get; }
        public int Length { get; }
        public Direction Direction { get; private set; }
        public bool Destroyed { get; set; }
        public IList<ShipField> ShipFields { get; set; }

        public void Rotate()
        {
            Direction = Direction == Direction.Horizonal ? 
                Direction.Vertical : Direction.Horizonal;
        }
    }
}
