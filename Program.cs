using System;
using System.Collections.Generic;

namespace RockPaperScissorsGame
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: dotnet run <move1> <move2> ... <moveN>");
                Console.WriteLine("Provide an odd number (>=3) of non-repeating moves.");
            }
            else
            {
                try
                {
                    List<string> moves = new List<string>(args);
                    RockPaperScissorsGame game = new RockPaperScissorsGame(moves);
                    game.Play();
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
