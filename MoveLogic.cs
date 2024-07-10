using System.Collections.Generic;

namespace RockPaperScissorsGame
{
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
}
