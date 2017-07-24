using System;
using System.Diagnostics;
using CC.Core;
using CC.Core.Algorithm;

namespace CC_AI_Console
{
    public class Game
    {
        //UI related constant
        private const string SelectTargetString = "Please select a piece to move... \n";
        private const string SelecttargetLocationString = "Please specify the target position... \n";
        private const string XyPositionString = "x(0~8), y(0~9): ";

        //game related variables
        public State CurrentState;

        public Game()
        {
            this.CurrentState = new State();
        }

        public void Run()
        {
            Console.WriteLine(CurrentState.ToString());
            while (true)
            {               
                if (TestWin()) return;
                Console.WriteLine(SelectTargetString);
                Console.WriteLine(XyPositionString);
                var fromX = Convert.ToInt16(Console.ReadKey().KeyChar.ToString()); 
                var fromY = Convert.ToInt16(Console.ReadKey().KeyChar.ToString());
                Console.WriteLine(SelecttargetLocationString);
                Console.WriteLine(XyPositionString);
                var toX = Convert.ToInt16(Console.ReadKey().KeyChar.ToString());
                var toY = Convert.ToInt16(Console.ReadKey().KeyChar.ToString());
                var moveStatus = this.HandleUserMove(CurrentState, fromX, fromY, toX, toY);
                if (moveStatus == Move.MoveStatus.NoError)
                {
                    Console.WriteLine(CurrentState);
                    if (TestWin()) return;
                    Console.WriteLine("Now it's Computer's turn... ");
                    CurrentState = (AlphaBetaSearch.DoSearch(CurrentState, 1));
                }
                Console.Clear();
                Console.WriteLine(CurrentState.ToString());                
            }                      
        }

        private Move.MoveStatus HandleUserMove(State state, int fromX, int fromY, int toX, int toY)
        {
            var move = Move.MoveStatus.NoError;
            
            var fromK = Utility.GetOneDimention(fromX, fromY);
            var piece = CurrentState.GetPieceList().Get(fromK);
            if (piece.GetSide() == State.EmptySpace)
            {
                move = Move.MoveStatus.WrongPiece;
            }
            else if (piece.GetSide() != State.UserTurn)
            {
                move = Move.MoveStatus.WrongPiece;
            }
            else if (!PieceMove.MovePieceLegal(state, fromX, fromY, toX, toY))
            {
                move = Move.MoveStatus.Illegal;
            }
            PieceMove.MovePiece(state, fromX, fromY, toX, toY);
            return move;
        }

        private bool TestWin()
        {
            var winner = CurrentState.GetWinner();
            if (winner == State.CompTurn)
            {
                Console.WriteLine("Computer Wins!");
                return true;
            }
            if (winner == State.UserTurn)
            {
                Console.WriteLine("You Win!");
                return true;
            }
            return false;
        }
    }
}
