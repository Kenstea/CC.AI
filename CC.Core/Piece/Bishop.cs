using System;
using System.Collections.Generic;
using CC.Core.Piece;

namespace CC.Core.Pieces
{
    [Serializable]
    public class Bishop : PieceBase
    {
        private static readonly List<DirectionMove> _moveDirection =
            new List<DirectionMove>
            {
                new DirectionMove(-2, -2),
                new DirectionMove(-2, +2),
                new DirectionMove(+2, -2),
                new DirectionMove(+2, +2)
            };

        private static readonly List<int> _legalPosition = new List<int>
        {
            0,
            0,
            1,
            0,
            0,
            0,
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
            1,
            0,
            0,
            0,
            1,
            0,
            0,
            0,
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
            1,
            0,
            0,
            0,
            1,
            0,
            0,

            0,
            0,
            1,
            0,
            0,
            0,
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
            1,
            0,
            0,
            0,
            1,
            0,
            0,
            0,
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
            1,
            0,
            0,
            0,
            1,
            0,
            0
        };

        private static readonly int _existenceValue = 40;
        private static readonly int _mobilityValue = 2;

        public Bishop(int number) : base(number)
        {
        }

        public Bishop(int number, int x, int y) : base(number, x, y)
        {
        }

        public override string ToString()
        {
            if (Side == State.UserTurn) return "B";
            return "b";
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
                ? Evaluate.RedBishopPositionValue[k]
                : -1 * Evaluate.BlackBishopPositionValue[k];
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
                var obstacleX = fromX + _moveDirection[i].X / 2;
                var obstacleY = fromY + _moveDirection[i].Y / 2;
                if (!(pieceList.Get(Utility.GetOneDimention(obstacleX, obstacleY)) is Empty))
                    continue;
                var toK = Utility.GetOneDimention(toX, toY);
                var fromSide = pieceList.Get(fromK).GetSide();
                var toSide = pieceList.Get(toK).GetSide();
                if (_legalPosition[toK] == 1 && fromSide != toSide)
                {
                    var newMove = new Move(fromX, fromY, toX, toY);
                    newMoveList.Add(newMove);
                }
            }
            return newMoveList;
        }

        public override bool IsLegalMove(State state, int fromX, int fromY, int toX, int toY)
        {
            if (!IsLegalBasic(state, fromX, fromY, toX, toY)) return false;

            var pieceList = state.GetPieceList();
            var toK = Utility.GetOneDimention(toX, toY);

            if (!(_legalPosition[toK] == 1)) return false;
            if (Utility.DistanceSquare(fromX, fromY, toX, toY) != 8) return false;

            var obstacleX = (fromX + toX) / 2;
            var obstacleY = (fromY + toY) / 2;
            if (!(pieceList.Get(Utility.GetOneDimention(obstacleX, obstacleY)) is Empty))
                return false;

            return true;
        }
    }
}