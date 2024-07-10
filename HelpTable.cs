using System;
using System.Collections.Generic;
using ConsoleTables;

namespace RockPaperScissorsGame
{
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
            var table = new ConsoleTable(new string[] { "v PC/User >", }.Concat(moves).ToArray());

            for (int i = 0; i < moveCount; i++)
            {
                var row = new List<string> { moves[i] };
                for (int j = 0; j < moveCount; j++)
                {
                    if (i == j)
                    {
                        row.Add("Draw");
                    }
                    else if ((j - i + moveCount) % moveCount <= moveCount / 2)
                    {
                        row.Add("Win");
                    }
                    else
                    {
                        row.Add("Lose");
                    }
                }
                table.AddRow(row.ToArray());
            }

            table.Write(Format.Alternative);
        }
    }
}
