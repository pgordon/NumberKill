using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NumberKill
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            InitializeBoard(10, 10);

            Application.Run(new Form1());
        }

        static void InitializeBoard(int height, int width)
        {
            BoardSquare [,] board = new BoardSquare[height, width];
            
            //place the Player
            PlayerThing player = new PlayerThing();
            board[0, 0] = new BoardSquare();
            board[0, 0].mThing = player;

            //place a handful of enemies
            PlaceEnemy(1, 5, board, height, width);
            PlaceEnemy(2, 3, board, height, width);
            PlaceEnemy(3, 2, board, height, width);
            PlaceEnemy(4, 1, board, height, width);
            PlaceEnemy(5, 1, board, height, width);

            //print board
            PrintBoard(board, height, width);
        }

        static void PlaceEnemy(int enemyType, int n, BoardSquare[,] board, int height, int width)
        {
            BoardSquare temp = new BoardSquare();
            Random random = new Random();

            for (int i = n; i >= 0; i--)
            {
                temp.mThing = new NumberThing(enemyType);
                board[random.Next(height), random.Next(width)] = temp;
            }
        }

        static void PrintBoard(BoardSquare[,] board, int height, int width)
        {
            BoardSquare bs;
            NumberThing nt;
            PlayerThing pt;
            int health = -1;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    bs = board[i, j];
                    if (bs != null)
                    {
                        nt = bs.mThing as NumberThing;
                        if (nt != null)
                            Console.Write(string.Format("{0} ", nt.mNumber));
                        else
                        {
                            pt = bs.mThing as PlayerThing;
                            if (pt != null) 
                            { 
                                Console.Write("P ");
                                health = pt.health;
                            }                        
                        }
                    }
                    else
                        Console.Write("0 ");
                }
                Console.Write("\n");
            }
            Console.Write(string.Format("Health: {0}", health));
        }
    }

    //TODO: have PlaceEnemy and PrintBoard be functions of the Board Object (make a Board object)

    class BoardSquare
    {
        public Thing mThing
        {
            get;
            set;
        }

        public BoardSquare()
        {

        }
    }

    class Thing
    {
        public Thing()
        {


        }
    }

    class PlayerThing : Thing
    {
        public int health
        {
            get;
            private set;
        }

        public PlayerThing()
        {
            health = 100;
        }
    }

    class NumberThing : Thing
    {
        public int mNumber
        {
            get;
            private set;
        }

        public NumberThing(int number)
        {
            mNumber = number;
        }
    }
}
