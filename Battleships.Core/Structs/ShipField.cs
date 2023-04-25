using Battleships.Core.Utility;

namespace Battleships.Core.Structs
{
    public class ShipField : Field
    {
        private readonly Coordinates coordinates;
        private readonly Ship ship;

        public ShipField(Coordinates coordinates, Ship ship) : base(ship.ShipType.ConvertToFieldType())
        {
            this.coordinates = coordinates;
            this.ship = ship;
        }

        public Coordinates Coordinates => coordinates;
        public Ship PartOf => ship;
    }
}
