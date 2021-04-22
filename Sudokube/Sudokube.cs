using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sudokube
{

    /*
     * Class: Sudokube
     * Description: Represents the entire 9x9x9 puzzle. Contains generation functions.
     *
     * Variables:
     * public Cell[,,] MainArray    - representation of the Sudokube puzzle
     *
     */
    class Sudokube
    {
        public Cell[,,] MainArray = new Cell[9, 9, 9];

        public Sudokube()
        {
            for (int z_axis = 0; z_axis < 9; z_axis++)
            {
                for (int y_axis = 0; y_axis < 9; y_axis++)
                {
                    for (int x_axis = 0; x_axis < 9; x_axis++)
                    {
                        this.MainArray[x_axis, y_axis, z_axis] = new Cell('0', false);
                    }
                    
                }
            }

            PuzzleGenerationSingle('1');
            PuzzleGenerationSingle('2');
            PuzzleGenerationSingle('3');

            PuzzleGenerationSingle('4');
            PuzzleGenerationSingle('5');
            PuzzleGenerationSingle('6');

            PuzzleGenerationSingle('7');
            PuzzleGenerationSingle('8');
            PuzzleGenerationSingle('9');

            // PuzzleGenerationLogical();
        }

        public Sudokube(int rand_nums)
        {
            // todo - generation function here
        }

        static void PrintRowTopBot(int index)
        {
            for (int col = 1; col <= 9; col++)
            {
                if (index == col)
                {
                    Console.Write("===");
                }
                else
                {
                    Console.Write("---");
                }
            }
            Console.Write("\n");
        }

        static void PrintRowMid(Cell[] rowCells, int index)
        {
            for (int col = 1; col <= 9; col++)
            {
                if (index == col)
                {
                    Console.Write((char)186);
                    Console.Write(rowCells[col-1].FillNumber);
                    Console.Write((char)186);
                }
                else
                {
                    Console.Write("|");
                    Console.Write(rowCells[col-1].FillNumber);
                    Console.Write("|");
                }
            }
        }

        public void PrintFullPuzzleZ() // full print along the z-axis
        {
            for (int z_axis = 0; z_axis < 9; z_axis++)
            {

                int z_axis_rep = z_axis + 1;
                Console.WriteLine(" z = " + z_axis_rep);
                PrintRowTopBot(-1);

                for (int y_axis = 0; y_axis < 9; y_axis++)
                {
                    Cell[] curr_row = new Cell[9];
                    for (int x_axis = 0; x_axis < 9; x_axis++)
                    {
                        Cell curr_cell = MainArray[x_axis, y_axis, z_axis];
                        curr_row[x_axis] = curr_cell;
                    }
                    //PrintRowTopBot(-1);
                    PrintRowMid(curr_row, -1);
                    Console.Write("\n");
                    PrintRowTopBot(-1);
                }
                Console.Write("\n");
            }
        }

        void PuzzleGenerationSingle(char num)
        {
            for (int z_axis = 0; z_axis < 9; z_axis++)
            {
                for (int y_axis = 0; y_axis < 9; y_axis++)
                {
                    for (int x_axis = 0; x_axis < 9; x_axis++)
                        {
                        NumberPlace(num, x_axis, y_axis, z_axis);
                    }
                }
            }
        }


        void PuzzleGenerationLogical()
        {
            int cell_count = 0;
            for (int x_axis = 0; x_axis < 9; x_axis++)
            {
                for (int y_axis = 0; y_axis < 9; y_axis++)
                {
                    for (int z_axis = 0; z_axis < 9; z_axis++)
                    {
                        List<char> PossibleNum = new List<char>()
                        {
                            '1', '2', '3',
                            '4', '5', '6',
                            '7', '8', '9'
                        };

                        foreach (char taken in this.MainArray[x_axis, y_axis, z_axis].TakenNum)
                        {
                            PossibleNum.Remove(taken);
                        }

                        bool success = false;

                        foreach (char possibility in PossibleNum)
                        {
                            success = NumberPlace(possibility, x_axis, y_axis, z_axis);
                            if (success)
                            {
                                break;
                            }
                        }

                        if (!success)
                        {
                            Console.WriteLine("Something terrible has happened.");
                            Thread.Sleep(20000);
                            Environment.Exit(0);
                        }

                    }
                }
            }
        }

        // places a number in the specified cell, then updates other cells
        bool NumberPlace(char num, int x_axis, int y_axis, int z_axis)
        {
            var target = MainArray[x_axis, y_axis, z_axis];
            if (target.FillNumber == '0' && !(target.TakenNum.Contains(num)))
            {
                target.FillNumber = num;
                UpdateAxis(num, x_axis, y_axis,z_axis);
                UpdateSquare(num, x_axis, y_axis, z_axis);
                return true;
            }
            else
            {
                return false;
            }
        }
        // updates cells along the 3 affected axis
        void UpdateAxis(char num, int x_axis, int y_axis, int z_axis)
        {
            for (int idx = 0; idx < 9; idx++)
            {
                MainArray[idx, y_axis, z_axis].UpdateTaken(num);
                MainArray[x_axis, idx, z_axis].UpdateTaken(num);
                MainArray[x_axis, y_axis, idx].UpdateTaken(num);
            }
        }

        void UpdateSquare(char num, int x_axis, int y_axis, int z_axis)
        {
            int x_rep = SquareFind(x_axis);
            int y_rep = SquareFind(y_axis);
            int z_rep = SquareFind(z_axis);

            UpdateXSquare(num, x_axis, y_rep, z_rep);
            UpdateYSquare(num, x_rep, y_axis, z_rep);
            UpdateZSquare(num, x_rep, y_rep, z_axis);
        }

        // helper function to determine square coords of a certain axis
        int SquareFind(int axis)
        {
            if (axis < 3)
            {
                return 0;
            }
            else if (axis > 5)
            {
                return 6;
            }
            else
            {
                return 3;
            }
        }

        void UpdateXSquare(char num, int x_axis, int y_rep, int z_rep)
        {
            int y_edge = y_rep + 3;
            int z_edge = z_rep + 3;
            for (int y_iter = y_rep; y_iter < y_edge; y_iter++)
            {
                for (int z_iter = z_rep; z_iter < z_edge; z_iter++)
                {
                    MainArray[x_axis, y_iter, z_iter].UpdateTaken(num);
                }
            }
        }

        void UpdateYSquare(char num, int x_rep, int y_axis, int z_rep)
        {
            int x_edge = x_rep + 3;
            int z_edge = z_rep + 3;
            for (int x_iter = x_rep; x_iter < x_edge; x_iter++)
            {
                for (int z_iter = z_rep; z_iter < z_edge; z_iter++)
                {
                    MainArray[x_iter, y_axis, z_iter].UpdateTaken(num);
                }
            }
        }

        void UpdateZSquare(char num, int x_rep, int y_rep, int z_axis)
        {
            int x_edge = x_rep + 3;
            int y_edge = y_rep + 3;
            for (int x_iter = x_rep; x_iter < x_edge; x_iter++)
            {
                for (int y_iter = y_rep; y_iter < y_edge; y_iter++)
                {
                    MainArray[x_iter, y_iter, z_axis].UpdateTaken(num);
                }
            }
        }



    }
}
