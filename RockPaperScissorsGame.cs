using System;
using System.Collections.Generic;

namespace RockPaperScissorsGame
{
    public class RockPaperScissorsGame
    {
        private List<string> moves;
        private HMACGenerator hmacGen;
        private MoveLogic moveLogic;
        private string computerMove;
        private string hmac;

        public RockPaperScissorsGame(List<string> moves)
        {
            if (moves.Count < 3 || moves.Count % 2 == 0 || moves.Distinct().Count() != moves.Count)
            {
                throw new ArgumentException("Invalid moves. Provide an odd number (>=3) of non-repeating strings.");
            }

            this.moves = moves;
            this.hmacGen = new HMACGenerator();
            this.moveLogic = new MoveLogic(moves);
            Random rand = new Random();
            this.computerMove = moves[rand.Next(moves.Count)];
            this.hmac = hmacGen.GenerateHMAC(computerMove);
        }

        public void DisplayMoves()
        {
            Console.WriteLine("Available moves:");
            for (int i = 0; i < moves.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {moves[i]}");
            }
            Console.WriteLine("0 - exit");
            Console.WriteLine("? - help");
        }

        public void Play()
        {
            Console.WriteLine($"HMAC: {hmac}");
            while (true)
            {
                DisplayMoves();
                Console.Write("Enter your move: ");
                string userInput = Console.ReadLine().Trim();

                if (userInput == "0")
                {
                    Console.WriteLine("Exiting the game.");
                    break;
                }
                else if (userInput == "?")
                {
                    HelpTable helpTable = new HelpTable(moves);
                    helpTable.GenerateTable();
                    continue;
                }

                if (int.TryParse(userInput, out int moveIndex) && moveIndex >= 1 && moveIndex <= moves.Count)
                {
                    string userMove = moves[moveIndex - 1];
                    Console.WriteLine($"Your move: {userMove}");
                    Console.WriteLine($"Computer move: {computerMove}");

                    string result = moveLogic.DetermineWinner(userMove, computerMove);
                    Console.WriteLine(result);
                    Console.WriteLine($"HMAC key: {hmacGen.GetKey()}");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input, try again.");
                }
            }
        }
    }
}
