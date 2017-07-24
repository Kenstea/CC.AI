using System;
using System.Diagnostics;
using CC.Core.Piece;

namespace CC.Core
{
    public class PieceMove
    {
        public static State MovePiece(State state, Move move)
        {
            return MovePiece(state, move.FromX, move.FromY, move.ToX, move.ToY);
        }

        public static State MovePiece(State state, int fromX, int fromY, int toX, int toY)
        {
            State newState = null;
            try
            {
                newState = (State) state.Clone;
                var pieceList = newState.GetPieceList();

                var fromK = Utility.GetOneDimention(fromX, fromY);
                var toK = Utility.GetOneDimention(toX, toY);

                var pieceFrom = (IPiece) pieceList.Get(fromK).Clone();
                var pieceTo = (IPiece) pieceList.Get(toK).Clone();

                pieceList.TryRemove(fromK, out pieceFrom);
                if (pieceTo != null)
                    pieceList.TryRemove(toK, out pieceTo);
                pieceFrom.SetPosition(toX, toY);
                pieceList.TryAdd(toK, pieceFrom);
            }
            catch (Exception)
            {
                //e.printStackTrace();
            }
            return newState;
        }

        public static bool MovePieceLegal(State state, int fromX, int fromY, int toX, int toY)
        {
            var pieceList = state.GetPieceList();

            var fromK = Utility.GetOneDimention(fromX, fromY);
            var toK = Utility.GetOneDimention(toX, toY);
            var pieceFrom = (IPiece) pieceList.Get(fromK).Clone();
           
            if (pieceFrom.IsLegalMove(state, fromX, fromY, toX, toY))
            {
                pieceList.TryRemove(pieceFrom.GetK(), out pieceFrom);
                pieceFrom.SetPosition(toX, toY);
                pieceList.TryAdd(toK, pieceFrom);
                return true;
            }
            return false;
        }
    }
}