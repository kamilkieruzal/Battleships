namespace Battleships.Core.Utility
{
    public class CoordinatesParserException : Exception
    {
        public CoordinatesParserException() : base()
        {
        }

        public CoordinatesParserException(string message) : base(message)
        {
        }
    }
}
