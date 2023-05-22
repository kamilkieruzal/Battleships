using Battleships.Core.Structs;

namespace Battleships.Core.Utility
{
    public static class CoordinatesUtility
    {
        public static Coordinates ParseStringCoordinates(string coords)
        {
            if (coords.Length > 3 || coords.Length < 2)
                throw StringParserException();

            var column = coords.Substring(0, 1);
            var row = coords.Split(column)[1];

            var columnNumber = ChangeCharacterToNumber(column[0]);
            var gameAreaRange = Enumerable.Range(1, GameConsts.SizeOfArena);

            if (!int.TryParse(row, out int rowNumber))
                throw StringParserException();

            if (!gameAreaRange.Contains(columnNumber) || !gameAreaRange.Contains(rowNumber))
                throw StringParserException();

            return new Coordinates(columnNumber - 1, rowNumber - 1);
        }

        public static string ParseIntCoordinates(Coordinates coords)
        {
            var gameAreaRange = Enumerable.Range(1, GameConsts.SizeOfArena);

            if (!gameAreaRange.Contains(coords.X) || !gameAreaRange.Contains(coords.Y))
                throw IntParserException();

            return $"{ChangeNumberToCharacter(coords.X + 1)}{coords.Y + 1}";
        }

        public static IList<Coordinates> AddSourrondingCoordinates(this Coordinates coords)
        {
            var x = coords.X;
            var y = coords.Y;

            return new List<Coordinates>
            {
                coords,
                new Coordinates(x, y + 1),
                new Coordinates(x, y - 1),
                new Coordinates(x + 1, y),
                new Coordinates(x - 1, y),
                new Coordinates(x - 1, y + 1),
                new Coordinates(x + 1, y - 1),
                new Coordinates(x + 1, y + 1),
                new Coordinates(x - 1, y - 1),
            };
        }

        private static int ChangeCharacterToNumber(char letter) => char.ToUpper(letter) - 64;
        private static char ChangeNumberToCharacter(int number) => (char)(64 + number);
        private static CoordinatesParserException StringParserException() =>
            new($"Provided position must be 2-3 character string, where first char is letter from a to j and second is a number from 1 to {GameConsts.SizeOfArena}");
        private static CoordinatesParserException IntParserException() =>
            new($"Provided columnNumber and rowNumber must be in range from 1 to {GameConsts.SizeOfArena}");
    }
}
