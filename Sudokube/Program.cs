using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sudokube
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Sudokube Suite.");

            bool main_loop = true;
            string user_input;

            while (main_loop)
            {
                user_input = UserPrompt();
                main_loop = InputCheck(user_input);
            }

            Thread.Sleep(2000);
            Environment.Exit(0);

        }

        static string UserPrompt()
        {
            Console.WriteLine("What would you like to do? Please enter the corresponding number.\n");
            Console.WriteLine("1 - Rules");
            Console.WriteLine("2 - Sudokube Puzzle");
            Console.WriteLine("3 - ReadMe");
            Console.WriteLine("4 - Quit");

            string input = Console.ReadLine();

            return input;
        }

        static bool InputCheck(string input)
        {
            switch (input)
            {
                case "1":
                    Console.WriteLine("Not yet implemented.");
                //TODO - print a rulesheet
                    return true;
                case "2":
                    PuzzleBegin();
                    return true;
                case "3":
                    Console.WriteLine("Not yet implemented.");
                //todo - print readme
                    return true;
                case "4":
                    Console.WriteLine("Thank you for trying out the Sudokube Suite.");
                    return false;
                default:
                    Console.WriteLine("Input does not match any given option.\n" +
                                      "Please try again.");
                    return true;
            }
        }

        static void PuzzleBegin()
        {
            Sudokube user_puzzle = new Sudokube();



            user_puzzle.PrintFullPuzzleZ();
        }
    }
}
