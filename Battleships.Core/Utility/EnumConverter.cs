using Battleships.Core.Structs;

namespace Battleships.Core.Utility
{
    public static class EnumConverter
    {
        public static ShipType ConvertToShipType(this FieldType fieldType)
        {
            return (ShipType)fieldType;
        }

        public static FieldType ConvertToFieldType(this ShipType shipType)
        {
            return (FieldType)shipType;
        }
    }
}
