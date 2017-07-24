using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CC.Core.Piece
{
    [Serializable]
    public abstract class PieceBase : IPiece
    {
        private readonly int _number;
        private int _k; //Piece value           
        private int _x; // Coordinate
        private int _y; // Coordinate
        protected int Side; // Side of the piece (COMP/Human/None)

        protected PieceBase(int number)
        {
            _number = number;
            SetSide();
        }

        protected PieceBase(int number, int x, int y)
        {
            _number = number;
            _x = x;
            _y = y;
            _k = Utility.GetOneDimention(x, y);
            SetSide();
        }

        public abstract override string ToString();
        public abstract int EvaluateExistence();

        public abstract int EvaluateMobility(State state, int fromX, int fromY);


        public abstract int EvaluateStatic(int x, int y);

        public abstract List<Move> GenerateAllMove(State state, int fromX, int fromY);


        public int GetK()
        {
            return _k;
        }

        public int GetNumber()
        {
            return _number;
        }

        public int GetSide()
        {
            return Side;
        }

        public int GetX()
        {
            return _x;
        }

        public int GetY()
        {
            return _y;
        }

        public abstract bool IsLegalMove(State state, int fromX, int fromY, int toX, int toY);

        public void SetPosition(int x, int y)
        {
            _x = x;
            _y = y;
            _k = Utility.GetOneDimention(x, y);
        }

        //public object Clone()
        //{
        //    object returnObj = null;
        //    try
        //    {
        //        returnObj = base.MemberwiseClone();
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    return returnObj;
        //}

        public object Clone()
        {
            using (var stream = new MemoryStream())
            {
                if (GetType().IsSerializable)
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, this);
                    stream.Position = 0;
                    return formatter.Deserialize(stream);
                }
                return null;
            }
        }

        protected static bool IsLegalBasic(State state, int fromX, int fromY, int toX, int toY)
        {
            if (!IsOnBoard(toX, toY)) return false;
            if (Suicide(state, fromX, fromY, toX, toY)) return false;
            if (StandStill(fromX, fromY, toX, toY)) return false;
            return true;
        }

        private static bool Suicide(State state, int fromX, int fromY, int toX, int toY)
        {
            var pieceList = state.GetPieceList();
            var sourcePiece = pieceList.Get(Utility.GetOneDimention(fromX, fromY));
            var targetPiece = pieceList.Get(Utility.GetOneDimention(toX, toY));
            if (sourcePiece.GetSide() == targetPiece.GetSide()) return true;
            return false;
        }

        protected static bool StandStill(int x1, int y1, int x2, int y2)
        {
            if (x1 == x2 && y1 == y2) return true;
            return false;
        }

        protected static bool IsOnBoard(int x, int y)
        {
            return x <= 8 && x >= 0 && y <= 9 && y >= 0;
        }

        private void SetSide()
        {
            if (_number == 0) Side = State.EmptySpace;
            else if ((_number & State.UserTurn) == State.UserTurn) Side = State.UserTurn;
            else Side = State.CompTurn;
        }
    }
}