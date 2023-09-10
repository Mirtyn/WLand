namespace Wland
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MapGenerator mapGenerator = new MapGenerator();

            mapGenerator.GenerateWorld();
        }
    }

    internal class MapGenerator
    {
        private int mapXSize = 16;
        private int mapYSize = 16;

        private int minTrees = 20;
        private int maxTrees = 40;

        private List<Tree> trees = new List<Tree>();
        private Dictionary<int, Tile> map = new Dictionary<int, Tile>();

        private Player player = new Player();

        Random rnd = new Random();

        public void GenerateWorld()
        {
            player.XPos = rnd.Next(mapXSize);
            player.YPos = rnd.Next(mapYSize);

            int treesAmount = rnd.Next(minTrees, maxTrees + 1);

            for (int i = 0; i < treesAmount; i++)
            {
                Tree t = new Tree();

                t.XPos = rnd.Next(mapXSize);
                t.YPos = rnd.Next(mapYSize);

                bool addTree = true;

                for (int j = 0; j < trees.Count; j++)
                {
                    if (trees[j].XPos == t.XPos && trees[j].YPos == t.YPos)
                    {
                        // Already Tree there
                        addTree = false;
                    }
                }

                if (addTree)
                {
                    trees.Add(t);
                }
            }

            DrawMap();
        }

        private void DrawMap()
        {
            Console.WriteLine();
            Console.WriteLine();

            for (int y = 0; y < mapYSize; y++)
            {
                for (int x = 0; x < mapXSize; x++)
                {
                    if (player.XPos == x && player.YPos == y)
                    {
                        switch (player.Direction)
                        {
                            case Player._Direction.North:
                                Console.Write("^");
                                break;
                            case Player._Direction.East:
                                Console.Write(">");
                                break;
                            case Player._Direction.South:
                                Console.Write("µ");
                                break;
                            case Player._Direction.West:
                                Console.Write("<");
                                break;
                        }
                    }
                    else
                    {
                        bool setTree = false;

                        for (int t = 0; t < trees.Count; t++)
                        {
                            if (x == trees[t].XPos && y == trees[t].YPos)
                            {
                                setTree = true;
                            }
                        }

                        if (setTree)
                        {
                            Console.Write("|");
                        }
                        else
                        {
                            Console.Write("=");
                        }
                    }
                }

                Console.Write("\n");
            }

            PlayerInput();
        }

        private void PlayerInput()
        {
            bool quit = false;

            ConsoleKeyInfo key = Console.ReadKey();

            switch (key.Key)
            {
                case ConsoleKey.Z:
                    player.YPos--;
                    player.Direction = Player._Direction.North;
                    break;
                case ConsoleKey.S:
                    player.YPos++;
                    player.Direction = Player._Direction.South;
                    break;
                case ConsoleKey.Q:
                    player.XPos--;
                    player.Direction = Player._Direction.West;
                    break;
                case ConsoleKey.D:
                    player.XPos++;
                    player.Direction = Player._Direction.East;
                    break;
                case ConsoleKey.Escape:
                    quit = true;
                    break;
            }

            if (!quit)
            {
                DrawMap();
            }
        }

        //public bool TryMovePlayerInDirection(Player._Direction direction)
        //{
        //    if (direction == Player._Direction.North)
        //    {

        //    }
        //    else if (direction == Player._Direction.South)
        //    {

        //    }
        //    else if (direction == Player._Direction.West)
        //    {

        //    }
        //    else if (direction == Player._Direction.East)
        //    {

        //    }
        //    else { return false; }
        //}
    }

    internal class Tile
    {
        public int XPos;
        public int YPos;
    }

    internal class GrassTile : Tile
    {

    }

    internal class Object
    {
        public int XPos;
        public int YPos;
    }

    internal class Tree : Object
    {
        
    }

    internal class Player : Object
    {
        public enum _Direction
        {
            North,
            South,
            East,
            West
        }

        public _Direction Direction;
    }
}