

using System;
using System.Threading;

namespace Gol_Bucek
{
    class Gol_Bucek
    {
        const int SIZE_X = 100;
        const int SIZE_Y = 30;
        const int MAX_START_CELL_CNT = SIZE_X * SIZE_Y;
        const int NUMBER_OF_ROUNDS = 1000;

        
        static Random random = new Random();

        static void Main(string[] args)
        {
            bool[,] cells = new bool[SIZE_X, SIZE_Y];

            Console.SetWindowSize(SIZE_X + 1, SIZE_Y + 3);
            int actRound = 0;

            int numberOfStartCells = ReadNumberOfStartCells();
            BuildRandomStartCells(ref cells,numberOfStartCells);
        
            DrawCells(cells);

            Console.WriteLine("Eingabe-Taste drücken zum Starten des Lebens !");
            Console.ReadLine();

            //Jetzt Lebenszyklen berechnen
            do
            {
                Console.WriteLine("Anzahl Zellen:"+CalculateCellsOfNextRound(ref cells));
                DrawCells(cells);
                Thread.Sleep(100);
                actRound++;
            } while (actRound <= NUMBER_OF_ROUNDS);
        }

        /// <summary>
        /// Liest die Anzahl der Zellen ein, die am Beginn "belebt" werden sollen
        /// </summary>
        /// <returns></returns>
        private static int ReadNumberOfStartCells()
        {
            int numberOfLivingStartCells;
            Console.WriteLine("Wieviele Zellen belegen (Max:{0}) ?", MAX_START_CELL_CNT);
            numberOfLivingStartCells = Convert.ToInt32(Console.ReadLine());
            while ((numberOfLivingStartCells > (MAX_START_CELL_CNT)) || (numberOfLivingStartCells < 1))
            {
                Console.WriteLine("Ungültige Startzellennazahl !");
                Console.WriteLine("Wieviele Zellen belegen (Min: 1   Max:{0}) ?", MAX_START_CELL_CNT);
                numberOfLivingStartCells = Convert.ToInt32(Console.ReadLine());
            }
            return numberOfLivingStartCells;
        }


        /// <summary>
        /// Platziert zufällig eine bestimmte vom Benutzer einzugebene Anzahl von Zellen
        /// </summary>
        /// <param name="cells"></param>
        private static void BuildRandomStartCells(ref bool[,] cells, int numberOfLivingStartCells)
        {
            for (int i = 1; i <= numberOfLivingStartCells; i++)
            {
                FindAndSetSingleRandomCell(ref cells);
            }
        }

        /// <summary>
        /// Zeichnet das Spielfeld
        /// </summary>
        static void DrawCells(bool[,] zellen)
        {
            int x, y;

            Console.Clear();
            for (y = 0; y < SIZE_Y; y++)
            {
                for (x = 0; x < SIZE_X; x++)
                {
                    if (zellen[x, y])
                        Console.Write('X');
                    else
                        Console.Write(' ');
                        
                }
                Console.WriteLine();

            }
        }

        /// <summary>
        /// Versucht eine Zelle solange zufällig zu platzieren, bis eine freie
        /// Zelle gefunden wurde!
        /// </summary>
        static void FindAndSetSingleRandomCell(ref bool[,] cells)
        {
            int x, y;

            do
            {
                x = random.Next(0, SIZE_X);
                y = random.Next(0, SIZE_Y);
            } while (cells[x, y] == true);
            cells[x, y] = true;
        }

        
        /// <summary>
        /// Berechnet die Anzahl der Nachbarn einer bestimmten Stelle
        /// </summary>
        /// <param name="cells"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        static int CalculateNeighbourCnt(bool[,] cells, int x, int y)
        {
            int u, v;
            int anz = 0;

            for (u = Math.Max(0, x - 1); u <= Math.Min(SIZE_X-1 , x+ 1); u++)
            {
                for (v = Math.Max(0,y-1); v <= Math.Min(SIZE_Y-1, y+1); v++)
                {
                    if (!((u == x) && (v == y)))
                    {
                        if (cells[u,v]==true)
                          anz = anz + 1 ;
                    }
                }
            }
            return anz;
        }

        /// <summary>
        /// Berechnet den nächsten Zyklus
        /// </summary>
        /// <param name="cells"></param>
        /// <returns></returns>
        static int CalculateCellsOfNextRound(ref bool[,] cells)
        {
            bool[,] newCells = new bool[SIZE_X, SIZE_Y];
            int y, x;
            int numberOfNeighbours;
            int numberOfLivingCells=0;
            

            //Alle Zellen durchgehen und prüfen ob belebt oder unbelebt
            for (x = 0; x < SIZE_X; x++)
            {
                for (y = 0; y < SIZE_Y; y++)
                {
                    numberOfNeighbours = CalculateNeighbourCnt(cells, x, y);

                        if ((numberOfNeighbours == 3) || ((numberOfNeighbours == 2) && (cells[x, y])))
                        {

                            newCells[x, y] = true;
                            numberOfLivingCells++;
                        }

                }

            }

            cells = newCells;
            return numberOfLivingCells;
        }

    }
}
