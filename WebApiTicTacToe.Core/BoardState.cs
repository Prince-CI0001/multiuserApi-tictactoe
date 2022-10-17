using System.Runtime.CompilerServices;
using TicTacToe.Contracts;
using TicTacToe.Data;
using WebApiTicTacToe.Contracts;

namespace TicTacToe.Core
{
    public class BoardState : IBoardState
    {
        
        private readonly IGameRepository _gameRepo;

        public BoardState(IGameRepository gameRepo)
        {
            _gameRepo = gameRepo ?? throw new ArgumentNullException(nameof(gameRepo));
        }
        private char player = 'O', opponent = 'X';
        private char Computer = 'O';
        private char Human = 'X';
        static char[,] boardMatrix = new char[,] {
                                            { '_','_','_' },
                                            { '_','_','_' },
                                            { '_','_','_' }
        };


        public void UpdateGameState(Guid playerId,Guid gameId)
        {
            String str = "";
            for(int i = 0; i < 3; i++)
            {
                for(int j=0;j<3;j++)
                {
                    str += boardMatrix[i, j];
                }
                _gameRepo.UpdateGame(str,playerId,gameId);
            }
        }
        public string ExecuteMove(string location,Guid playerId,Guid gameId)
        {
            int r = location[0] - '0';
            int c = location[1] - '0';
            boardMatrix[r, c] = Human;
            UpdateGameState(playerId, gameId);
            if (isWinner(Human))
            {
                _gameRepo.Winner("Human", playerId, gameId);
            }


            Step bestMove = findBestMove(boardMatrix);
            if (bestMove.row == -1 && bestMove.column == -1)
            {
                return "";                
            }
            boardMatrix[bestMove.row, bestMove.column] = Computer;
            UpdateGameState(playerId,gameId);
            if (isWinner(Computer))
            {
                _gameRepo.Winner("Computer", playerId, gameId);
            }
            var compIndex = bestMove.row.ToString() + bestMove.column.ToString();
            return compIndex;

        }
        private Boolean isMovesLeft(char[,] board)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (board[i, j] == '_')
                        return true;
            return false;
        }

        private int evaluate(char[,] b)
        {
            // Checking for Rows for X or O victory.
            for (int row = 0; row < 3; row++)
            {
                if (b[row, 0] == b[row, 1] &&
                    b[row, 1] == b[row, 2])
                {
                    if (b[row, 0] == player)
                        return +10;
                    else if (b[row, 0] == opponent)
                        return -10;
                }
            }

            // Checking for Columns for X or O victory.
            for (int col = 0; col < 3; col++)
            {
                if (b[0, col] == b[1, col] &&
                    b[1, col] == b[2, col])
                {
                    if (b[0, col] == player)
                        return +10;

                    else if (b[0, col] == opponent)
                        return -10;
                }
            }

            // Checking for Diagonals for X or O victory.
            if (b[0, 0] == b[1, 1] && b[1, 1] == b[2, 2])
            {
                if (b[0, 0] == player)
                    return +10;
                else if (b[0, 0] == opponent)
                    return -10;
            }

            if (b[0, 2] == b[1, 1] && b[1, 1] == b[2, 0])
            {
                if (b[0, 2] == player)
                    return +10;
                else if (b[0, 2] == opponent)
                    return -10;
            }

            // Else if none of them have won then return 0
            return 0;
        }

        // This is the minimax function. It considers all
        // the possible ways the game can go and returns
        // the value of the board
        private int minimax(char[,] board,
                           int depth, Boolean isMax)
        {
            int score = evaluate(board);

            // If Maximizer has won the game
            // return his/her evaluated score
            if (score == 10)
                return score;

            // If Minimizer has won the game
            // return his/her evaluated score
            if (score == -10)
                return score;

            // If there are no more moves and
            // no winner then it is a tie
            if (isMovesLeft(board) == false)
                return 0;

            // If this maximizer's move
            if (isMax)
            {
                int best = -1000;

                // Traverse all cells
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        // Check if cell is empty
                        if (board[i, j] == '_')
                        {
                            // Make the move
                            board[i, j] = player;

                            // Call minimax recursively and choose
                            // the maximum value
                            best = Math.Max(best, minimax(board,
                                            depth + 1, !isMax));

                            // Undo the move
                            board[i, j] = '_';
                        }
                    }
                }
                return best;
            }

            // If this minimizer's move
            else
            {
                int best = 1000;

                // Traverse all cells
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        // Check if cell is empty
                        if (board[i, j] == '_')
                        {
                            // Make the move
                            board[i, j] = opponent;

                            // Call minimax recursively and choose
                            // the minimum value
                            best = Math.Min(best, minimax(board,
                                            depth + 1, !isMax));

                            // Undo the move
                            board[i, j] = '_';
                        }
                    }
                }
                return best;
            }
        }

        // This will return the best possible
        // move for the player
        private Step findBestMove(char[,] board)
        {
            int bestVal = -1000;
            Step bestMove = new Step();
            bestMove.row = -1;
            bestMove.column = -1;

            // Traverse all cells, evaluate minimax function
            // for all empty cells. And return the cell
            // with optimal value.
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Check if cell is empty
                    if (board[i, j] == '_')
                    {
                        // Make the move
                        board[i, j] = player;

                        // compute evaluation function for this
                        // move.
                        int moveVal = minimax(board, 0, false);

                        // Undo the move
                        board[i, j] = '_';

                        // If the value of the current move is
                        // more than the best value, then update
                        // best/
                        if (moveVal > bestVal)
                        {
                            bestMove.row = i;
                            bestMove.column = j;
                            bestVal = moveVal;
                        }
                    }
                }
            }

            //Console.Write("The value of the best Move " +
            //                    "is : {0}\n\n", bestVal);

            return bestMove;
        }
        public void EmptyMatrix()
        {
            for(int i=0;i<3;i++)
            {
                for(int j=0;j<3;j++)
                {
                    boardMatrix[i, j] = '_';
                }
            }
        }




        public bool isWinner(char ch){
            // Check rows and columns
            for (int i = 0; i < 3; i++)
            {
                int row_count = 0;
                int col_count = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (boardMatrix[i,j] == ch) row_count++;
                    if (boardMatrix[j,i] == ch) col_count++;
                }
                if (row_count == 3 || col_count == 3) return true;
                row_count = 0;
                col_count = 0;
            }

            // Check diagonals
            int count = 0;
            int anti_count = 0;
            for (int i = 0; i < 3; i++)
            {
                if (boardMatrix[i,i] == ch) count++;
                if (boardMatrix[i,3 - i - 1] == ch) anti_count++;
                if (count == 3 || anti_count == 3) return true;
            }

            return false;
        }

        
    }
}
