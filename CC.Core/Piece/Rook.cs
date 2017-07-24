using System;
using System.Collections.Generic;

namespace CC.Core.Piece
{
    [Serializable]
    public class Rook : PieceBase
    {
        private const int ExistenceValue = 180;
        private const int MobilityValue = 4;

        private static readonly List<DirectionMove> MoveDirection = new List<DirectionMove>
        {
            new DirectionMove(-1, 0),
            new DirectionMove(+1, 0),
            new DirectionMove(0, -1),
            new DirectionMove(0, +1)
        };

        public Rook(int number) : base(number)
        {
        }

        public Rook(int number, int x, int y) : base(number, x, y)
        {
        }

        public override string ToString()
        {
            if (Side == State.UserTurn) return "R";
            return "r";
        }

        public override int EvaluateExistence()
        {
            var value = Side == State.UserTurn ? ExistenceValue : -1 * ExistenceValue;
            return value;
        }

        public override int EvaluateMobility(State state, int fromX, int fromY)
        {
            var value = GenerateAllMove(state, fromX, fromY).Count * MobilityValue;
            value = Side == State.UserTurn ? value : -1 * value;
            return value;
        }

        public override int EvaluateStatic(int x, int y)
        {
            var k = Utility.GetOneDimention(x, y);
            var value = Side == State.UserTurn
                ? Evaluate.RedRookPositionValue[k]
                : -1 * Evaluate.BlackRookPositionValue[k];
            return value;
        }

        public override List<Move> GenerateAllMove(State state, int fromX, int fromY)
        {
            var newMoveList = new List<Move>();
            var pieceList = state.GetPieceList();
            var fromK = Utility.GetOneDimention(fromX, fromY);
            for (var i = 0; i < 4; i++)
            for (var j = 1; j <= 10; j++)
            {
                var toX = fromX + MoveDirection[i].X * j;
                var toY = fromY + MoveDirection[i].Y * j;
                var toK = Utility.GetOneDimention(toX, toY);
                if (!IsOnBoard(toX, toY)) break;
                var fromSide = pieceList.Get(fromK).GetSide();
                var toSide = pieceList.Get(toK).GetSide();
                if (fromSide != toSide)
                {
                    var newMove = new Move(fromX, fromY, toX, toY);
                    newMoveList.Add(newMove);
                    if (toSide != State.EmptySpace) break;
                }
                else
                {
                    break;
                }
            }
            return newMoveList;
        }

        public override bool IsLegalMove(State state, int fromX, int fromY, int toX, int toY)
        {
            if (!IsLegalBasic(state, fromX, fromY, toX, toY)) return false;

            var pieceList = state.GetPieceList();
            if (fromX == toX)
            {
                var lowerBound = (fromY > toY ? toY : fromY) + 1;
                var upperBound = (fromY > toY ? fromY : toY) - 1;
                for (var i = lowerBound; i <= upperBound; i++)
                    if (!(pieceList.Get(Utility.GetOneDimention(fromX, i)) is Empty))
                        return false;
                return true;
            }
            if (fromY == toY)
            {
                var lowerBound = (fromX > toX ? toX : fromX) + 1;
                var upperBound = (fromX > toX ? fromX : toX) - 1;
                for (var i = lowerBound; i <= upperBound; i++)
                    if (!(pieceList.Get(Utility.GetOneDimention(i, fromY)) is Empty))
                        return false;
                return true;
            }
            return false;
        }
    }
}