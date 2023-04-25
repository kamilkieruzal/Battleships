namespace Battleships.Core.Structs
{
    public abstract class Field
    {
        public Field()
        {
        }

        public Field(FieldType fieldType)
        {
            FieldType = fieldType;
        }

        public FieldType FieldType { get; set; }
    }
}
