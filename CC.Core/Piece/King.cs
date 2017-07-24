using System;
using System.Collections.Generic;
using CC.Core.Piece;

namespace CC.Core.Pieces
{
    [Serializable]
    public class King : PieceBase
    {
        private static readonly List<DirectionMove> _moveDirection =
            new List<DirectionMove>
            {
                new DirectionMove(-1, 0),
                new DirectionMove(+1, 0),
                new DirectionMove(0, -1),
                new DirectionMove(0, +1)
            };

        private static readonly List<int> _legalPosition = new List<int>
        {
            0,
            0,
            0,
            1,
            1,
            1,
            0,
            0,
            0,
            0,
            0,
            0,
            1,
            1,
            1,
            0,
            0,
            0,
            0,
            0,
            0,
            1,
            1,
            1,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,

            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            1,
            1,
            1,
            0,
            0,
            0,
            0,
            0,
            0,
            1,
            1,
            1,
            0,
            0,
            0,
            0,
            0,
            0,
            1,
            1,
            1,
            0,
            0,
            0
        };

        private static readonly int _existenceValue = 10000;
        private static readonly int _mobilityValue = 2;

        public King(int number) : base(number)
        {
        }

        public King(int number, int x, int y) : base(number, x, y)
        {
        }

        public override string ToString()
        {
            if (Side == State.UserTurn) return "K";
            return "k";
        }

        public override int EvaluateExistence()
        {
            var value = Side == State.UserTurn ? _existenceValue : -1 * _existenceValue;
            return value;
        }

        public override int EvaluateMobility(State state, int fromX, int fromY)
        {
            var value = GenerateAllMove(state, fromX, fromY).Count * _mobilityValue;
            value = Side == State.UserTurn ? value : -1 * value;
            return value;
        }

        public override int EvaluateStatic(int x, int y)
        {
            var k = Utility.GetOneDimention(x, y);
            var value = Side == State.UserTurn
                ? Evaluate.RedKingPositionValue[k]
                : -1 * Evaluate.BlackKingPositionValue[k];
            return value;
        }

        public override List<Move> GenerateAllMove(State state, int fromX, int fromY)
        {
            var newMoveList = new List<Move>();
            var pieceList = state.GetPieceList();
            var fromK = Utility.GetOneDimention(fromX, fromY);
            for (var i = 0; i < 4; i++)
            {
                var toX = fromX + _moveDirection[i].X;
                var toY = fromY + _moveDirection[i].Y;
                if (!IsOnBoard(toX, toY)) continue;
                var toK = Utility.GetOneDimention(toX, toY);
                var fromSide = pieceList.Get(fromK).GetSide();
                var toSide = pieceList.Get(toK).GetSide();
                if (_legalPosition[toK] == 1 && fromSide != toSide)
                {
                    var newMove = new Move(fromX, fromY, toX, toY);
                    newMoveList.Add(newMove);
                }
            }
            if (GetSide() == State.UserTurn)
            {
                int i;
                for (i = 0; i < 3; i++)
                    if (pieceList.Get(Utility.GetOneDimention(fromX, i)) is King) break;
                if (i < 3 && CleanPath(state, fromX, i + 1, fromY - 1))
                {
                    var newMove = new Move(fromX, fromY, fromX, i);
                    newMoveList.Add(newMove);
                }
                else
                {
                    return newMoveList;
                }
            }
            else if (GetSide() == State.CompTurn)
            {
                int i;
                for (i = 9; i > 6; i--)
                    if (pieceList.Get(Utility.GetOneDimention(fromX, i)) is King) break;
                if (i > 6 && CleanPath(state, fromX, fromY + 1, i - 1))
                {
                    var newMove = new Move(fromX, fromY, fromX, i);
                    newMoveList.Add(newMove);
                }
                else
                {
                    return newMoveList;
                }
            }
            return newMoveList;
        }

        private bool CleanPath(State state, int x, int fromY, int toY)
        {
            var pieceList = state.GetPieceList();
            for (var i = fromY; i <= toY; i++)
                if (!(pieceList.Get(Utility.GetOneDimention(x, i)) is Empty)) return false;
            return true;
        }

        public override bool IsLegalMove(State state, int fromX, int fromY, int toX, int toY)
        {
            if (!IsLegalBasic(state, fromX, fromY, toX, toY)) return false;

            var toK = Utility.GetOneDimention(toX, toY);
            if (!(_legalPosition[toK] == 1)) return false;
            if (fromX == toX && Utility.Abs(fromY - toY) > 4)
            {
                if (fromY > toY)
                    return CleanPath(state, fromX, toY + 1, fromY - 1);
                return CleanPath(state, fromX, fromY + 1, toY - 1);
            }
            if (Utility.DistanceSquare(fromX, fromY, toX, toY) != 1) return false;
            return true;
        }
    }
}