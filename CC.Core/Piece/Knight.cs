using System;
using System.Collections.Generic;
using CC.Core.Piece;

namespace CC.Core.Pieces
{
    [Serializable]
    public class Knight : PieceBase
    {
        private static readonly List<DirectionMove> _moveDirection = new List<DirectionMove>
        {
            new DirectionMove(-2, -1),
            new DirectionMove(-2, +1),
            new DirectionMove(-1, -2),
            new DirectionMove(-1, +2),
            new DirectionMove(+1, -2),
            new DirectionMove(+1, +2),
            new DirectionMove(+2, -1),
            new DirectionMove(+2, +1)
        };

        private static readonly int _existenceValue = 80;
        private static readonly int _mobilityValue = 5;

        public Knight(int number) : base(number)
        {
        }

        public Knight(int number, int x, int y) : base(number, x, y)
        {
        }

        public override string ToString()
        {
            if (Side == State.UserTurn) return "N";
            return "n";
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
                ? Evaluate.RedKnightPositionValue[k]
                : -1 * Evaluate.BlackKnightPositionValue[k];
            return value;
        }

        public override List<Move> GenerateAllMove(State state, int fromX, int fromY)
        {
            var newMoveList = new List<Move>();
            var pieceList = state.GetPieceList();
            var fromK = Utility.GetOneDimention(fromX, fromY);
            for (var i = 0; i < 8; i++)
            {
                var toX = fromX + _moveDirection[i].X;
                var toY = fromY + _moveDirection[i].Y;
                if (!IsOnBoard(toX, toY)) continue;
                var obstacleX = fromX + _moveDirection[i].X / 2;
                var obstacleY = fromY + _moveDirection[i].Y / 2;
                if (!(pieceList.Get(Utility.GetOneDimention(obstacleX, obstacleY)) is Empty)) continue;
                var toK = Utility.GetOneDimention(toX, toY);
                var fromSide = pieceList.Get(fromK).GetSide();
                var toSide = pieceList.Get(toK).GetSide();
                if (fromSide != toSide)
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

            if (Utility.DistanceSquare(fromX, fromY, toX, toY) != 5) return false;

            var obstacleXDistance = Utility.Abs(toX - fromX);
            var obstacleX = obstacleXDistance == 2 ? (fromX + toX) / 2 : fromX;
            var obstacleYDistance = Utility.Abs(toY - fromY);
            var obstacleY = obstacleYDistance == 2 ? (fromY + toY) / 2 : fromY;
            if (!(pieceList.Get(Utility.GetOneDimention(obstacleX, obstacleY)) is Empty)) return false;

            return true;
        }
    }
}