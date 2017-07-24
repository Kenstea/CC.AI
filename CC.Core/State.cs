using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using CC.Core.Piece;
using CC.Core.Pieces;

namespace CC.Core
{
    [Serializable]
    public class State 
    {
        private int _value;
        private const string RiverString = "    ==============================\n";
        private const string CoordinateXString = "y\\x   0  1  2  3  4  5  6  7  8    x/y\n";
        PieceMap<int, IPiece> _pieceList;
        public static bool PrintNum = false;
        public static PieceMap<int, IPiece> InitPieceList = new PieceMap<int, IPiece>(PieceFactory.GetPiece(0, 10, 10));
        //initial K value
        public static List<int> InitIntList = new List<int> {
            39, 37, 35, 33, 32, 34, 36, 38, 40,
            0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 41, 0, 0, 0, 0, 0, 42, 0,
            43, 0, 44, 0, 45, 0, 46, 0, 47,
            0, 0, 0, 0, 0, 0, 0, 0, 0,

            0, 0, 0, 0, 0, 0, 0, 0, 0,
            27, 0, 28, 0, 29, 0, 30, 0, 31,
            0, 25, 0, 0, 0, 0, 0, 26, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0,
            23, 21, 19, 17, 16, 18, 20, 22, 24};
        public static int UserTurn = 16;
        public static int CompTurn = 32;
        public static int EmptySpace = 0;
        public State()
        {
            InitState();
        }

        private void InitState()
        {
            if (InitPieceList.Count == 0) InitializePieceStateList();
            _pieceList = InitPieceList;
        }

        public object Clone
        {
            get
            {
                using (var stream = new MemoryStream())
                {
                    if (!GetType().IsSerializable) return null;
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, this);
                    stream.Position = 0;
                    var returnState = (State)formatter.Deserialize(stream);
                    returnState.SetPieceList(ClonePieceList());
                    //returnState.PieceList = ClonePieceList();
                    return returnState;
                }
            }
        }

        private PieceMap<int, IPiece> ClonePieceList()
        {
            var list = new PieceMap<int, IPiece>(PieceFactory.GetPiece(0, 10, 10));

            var pieces = _pieceList.Values;
            foreach (var piece in pieces)
            {
                list.TryAdd(piece.GetK(), (IPiece)piece.Clone());
            }
            return list;
        }

        private void SetPieceList(PieceMap<int, IPiece> pieceList)
        {
            _pieceList = pieceList;
        }

        public PieceMap<int, IPiece> GetPieceList()
        {
            return _pieceList;
        }

        //public PieceMap<int, IPiece> PieceList
        //{
        //    get => _pieceList;
        //    private set => _pieceList = value;
        //}



        public static void InitializePieceStateList()
        {
            if (InitIntList == null) return;
            var iterator = InitIntList.GetEnumerator();
            for (var y = 0; y <= 9; y++)
            {
                for (var x = 0; x <= 8; x++)
                {
                    if (!iterator.MoveNext()) continue;
                    var i = iterator.Current;
                    IPiece piece = PieceFactory.GetPiece(i, x, y);
                    if (i != 0) InitPieceList.GetOrAdd(piece.GetK(), piece);
                }
            }
        }
        public void EvaluateValue()
        {
            _value = Evaluate.EvaluateState(this);
        }

        public int GetValue()
        {
            return _value;
        }
        public void SetValue(int value)
        {
            _value = value;
        }

        public List<Move> GenerateAllMoves(int side)
        {
            var childMoves = new List<Move>();
            var pieces = _pieceList.Values;
            var it = pieces.GetEnumerator();

            while (it.MoveNext())
            {
                var piece = it.Current;
                if (piece.GetSide() == side)
                {
                    var newStates = piece.GenerateAllMove(this, piece.GetX(), piece.GetY());

                    childMoves.AddRange(newStates);
                }                
            }
            return childMoves;
        }

        public override string ToString() => ToString(_pieceList);

        public string ToString(PieceMap<int, IPiece> pieceList)
        {
            var buffer = new StringBuilder();
            buffer.Append(CoordinateXString);
            buffer.Append(RiverString);
            for (var y = 0; y <= 9; y++)
            {
                buffer.Append(y + " ||");
                for (var x = 0; x <= 8; x++)
                {
                    buffer.Append("  ");
                    var k = Utility.GetOneDimention(x, y);
                    var piece = pieceList.Get(k);
                    var pieceString = PrintNum ? piece.GetNumber().ToString() : piece.ToString();
                    buffer.Append(pieceString);
                }
                buffer.Append("  ||" + y);
                buffer.Append((y == 4) ? ("\n" + RiverString) : "\n");
            }
            buffer.Append(RiverString);
            buffer.Append(CoordinateXString);
            return (buffer.ToString());
        }

        public int GetWinner()
        {
            {
                var it = _pieceList.Values.GetEnumerator();
                var user = false;
                var computer = false;
                while (it.MoveNext())
                {
                    var piece = it.Current;
                    if (piece is King)
                    {
                        if (piece.GetSide() == CompTurn)
                        {
                            computer = true;
                        }
                        else user = true;
                    }
                }
                if (user == false) return CompTurn;
                return computer == false ? UserTurn : EmptySpace;
            }
        }
    }
}
