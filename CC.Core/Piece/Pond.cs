using System;
using System.Collections.Generic;
using CC.Core.Piece;

namespace CC.Core.Pieces
{
    [Serializable]
    public class Pond : PieceBase
    {
        private static readonly List<DirectionMove> _moveDirection =
            new List<DirectionMove>
            {
                new DirectionMove(-1, 0),
                new DirectionMove(+1, 0),
                new DirectionMove(0, -1),
                new DirectionMove(0, +1)
            };

        private static readonly List<int> _redLegalPosition = new List<int>
        {
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,

            1,
            0,
            1,
            0,
            1,
            0,
            1,
            0,
            1,
            1,
            0,
            1,
            0,
            1,
            0,
            1,
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
            0
        };

        private static readonly List<int> _blackLegalPosition = new List<int>
        {
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
            0,
            1,
            0,
            1,
            0,
            1,
            0,
            1,
            1,
            0,
            1,
            0,
            1,
            0,
            1,
            0,
            1,

            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1
        };

        private static readonly int _existenceValue = 30;
        private static readonly int _mobilityValue = 2;

        public Pond(int number) : base(number)
        {
        }

        public Pond(int number, int x, int y) : base(number, x, y)
        {
        }

        public override string ToString()
        {
            if (Side == State.UserTurn) return "P";
            return "p";
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
                ? Evaluate.RedPondPositionValue[k]
                : -1 * Evaluate.BlackPondPositionValue[k];
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
                if (GetSide() == State.UserTurn && _moveDirection[i].Y == 1) continue;
                if (GetSide() == State.CompTurn && _moveDirection[i].Y == -1) continue;
                if (!IsOnBoard(toX, toY)) continue;
                var toK = Utility.GetOneDimention(toX, toY);
                var fromSide = pieceList.Get(fromK).GetSide();
                var toSide = pieceList.Get(toK).GetSide();
                if (CheckLegalPosition(toK) && fromSide != toSide)
                {
                    var newMove = new Move(fromX, fromY, toX, toY);
                    newMoveList.Add(newMove);
                }
            }
            return newMoveList;
        }

        private bool CheckLegalPosition(int toK)
        {
            if (Side == State.UserTurn) return _redLegalPosition[toK] == 1;
            return _blackLegalPosition[toK] == 1;
        }

        public override bool IsLegalMove(State state, int fromX, int fromY, int toX, int toY)
        {
            if (!IsLegalBasic(state, fromX, fromY, toX, toY)) return false;

            var toK = Utility.GetOneDimention(toX, toY);
            if (!CheckLegalPosition(toK)) return false;
            if (Utility.DistanceSquare(fromX, fromY, toX, toY) != 1) return false;
            return true;
        }
    }
}