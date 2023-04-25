using Battleships.Core;
using Battleships.Core.Structs;
using Battleships.Core.Utility;

var visualDict = new Dictionary<FieldType, char>
{
    { FieldType.Battleship, 'B' },
    { FieldType.Destroyer, 'D' },
    { FieldType.Destroyed, 'X' },
    { FieldType.Hit, 'H' },
    { FieldType.Empty, (char)4 }
};

var colorDict = new Dictionary<FieldType, ConsoleColor>
{
    { FieldType.Battleship, ConsoleColor.Green },
    { FieldType.Destroyer, ConsoleColor.Yellow },
    { FieldType.Destroyed, ConsoleColor.DarkRed },
    { FieldType.Hit, ConsoleColor.Red },
    { FieldType.Empty, ConsoleColor.Blue }
};

Console.WriteLine("1 - generate areas, 2 - debug game");
Console.WriteLine();

Game game;
switch (Console.ReadLine())
{
    case "1":
        while(true)
        {
            game = new Game(new GameObjectsPool());

            for (int i = 0; i < GameConsts.SizeOfArena; i++)
            {
                for (int j = 0; j < GameConsts.SizeOfArena; j++)
                {
                    Console.ForegroundColor = colorDict[game.GameArea[j][i].FieldType];
                    //Console.Write((char)4 + " ");
                    Console.Write(visualDict[game.GameArea[j][i].FieldType] + " ");
                }
                Console.Write('\n');
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
        }
        break;

    case "2":
        game = new Game(new GameObjectsPool());
        while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape) && game.AnyShipsLeft())
        {
            for (int i = 0; i < GameConsts.SizeOfArena; i++)
            {
                for (int j = 0; j < GameConsts.SizeOfArena; j++)
                {
                    Console.ForegroundColor = colorDict[game.GameArea[j][i].FieldType];
                    //Console.Write((char)4 + " ");
                    Console.Write(visualDict[game.GameArea[j][i].FieldType] + " ");
                }
                Console.Write('\n');
            }

            Console.ForegroundColor = ConsoleColor.White;
            var stringCoords = Console.ReadLine();
            if (stringCoords.ToLower() == "exit") Environment.Exit(0);


            Coordinates coords;
            try
            {
                coords = CoordinatesUtility.ParseStringCoordinates(stringCoords);
            }
            catch (CoordinatesParserException)
            {
                Console.WriteLine("Wrong coordinates");
                continue;
            }

            Console.WriteLine($"You hit {game.GameArea[coords.X][coords.Y].FieldType}");
            game.Hit(coords);
        }
        break;
}