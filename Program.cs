using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RockPaperScissorsGame
{
    public class HMACGenerator
    {
        private byte[] key;

        public HMACGenerator(int keyLength = 32)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                key = new byte[keyLength];
                rng.GetBytes(key);
            }
        }

        public string GenerateHMAC(string message)
        {
            using (var hmac = new HMACSHA256(key))
            {
                byte[] hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
                return BitConverter.ToString(hashValue).Replace("-", "").ToLower();
            }
        }

        public string GetKey()
        {
            return BitConverter.ToString(key).Replace("-", "").ToLower();
        }
    }

    public class MoveLogic
    {
        private List<string> moves;
        private int moveCount;

        public MoveLogic(List<string> moves)
        {
            this.moves = moves;
            this.moveCount = moves.Count;
        }

        public string DetermineWinner(string playerMove, string computerMove)
        {
            int playerIndex = moves.IndexOf(playerMove);
            int computerIndex = moves.IndexOf(computerMove);

            if (playerIndex == computerIndex)
            {
                return "Draw";
            }

            int halfCount = moveCount / 2;
            if ((computerIndex - playerIndex + moveCount) % moveCount <= halfCount)
            {
                return "Computer wins";
            }
            else
            {
                return "Player wins";
            }
        }
    }

    public class HelpTable
    {
        private List<string> moves;
        private int moveCount;

        public HelpTable(List<string> moves)
        {
            this.moves = moves;
            this.moveCount = moves.Count;
        }

        public void GenerateTable()
        {
            string[,] table = new string[moveCount + 1, moveCount + 1];

            table[0, 0] = "";
            for (int i = 0; i < moveCount; i++)
            {
                table[0, i + 1] = moves[i];
                table[i + 1, 0] = moves[i];
            }

            for (int i = 1; i <= moveCount; i++)
            {
                for (int j = 1; j <= moveCount; j++)
                {
                    if (i == j)
                    {
                        table[i, j] = "Draw";
                    }
                    else if ((j - i + moveCount) % moveCount <= moveCount / 2)
                    {
                        table[i, j] = "Win";
                    }
                    else
                    {
                        table[i, j] = "Lose";
                    }
                }
            }

            PrintTable(table);
        }

        private void PrintTable(string[,] table)
        {
            for (int i = 0; i <= moveCount; i++)
            {
                for (int j = 0; j <= moveCount; j++)
                {
                    Console.Write(table[i, j].PadRight(10));
                    if (j < moveCount) Console.Write(" | ");
                }
                Console.WriteLine();
                if (i < moveCount) Console.WriteLine(new string('-', 10 * (moveCount + 1)));
            }
        }
    }

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
                    List<string> moves = args.ToList();
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
