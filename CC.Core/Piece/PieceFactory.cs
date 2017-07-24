using CC.Core.Pieces;

namespace CC.Core.Piece
{
    public class PieceFactory
    {
        /// <summary>
        ///     Gets the piece.
        /// 
        /// </summary>
        /// <param name="number">The constant number. E.g: Red Rook is 23, Black Rook is 39</param>
        /// <param name="x">The current X coordinate</param>
        /// <param name="y">The current X coordinate</param>
        /// <returns></returns>
        public static PieceBase GetPiece(int number, int x, int y)
        {
            if ((number & State.UserTurn) == State.UserTurn)
                switch (number)
                {
                    case 16: return new King(number, x, y);
                    case 17:
                    case 18: return new Advisor(number, x, y);
                    case 19:
                    case 20: return new Bishop(number, x, y);
                    case 21:
                    case 22: return new Knight(number, x, y);
                    case 23:
                    case 24: return new Rook(number, x, y);
                    case 25:
                    case 26: return new Cannon(number, x, y);
                    case 27:
                    case 28:
                    case 29:
                    case 30:
                    case 31: return new Pond(number, x, y);
                    default: return new Empty(number, x, y);
                }
            switch (number - State.UserTurn)
            {
                case 16: return new King(number, x, y);
                case 17:
                case 18: return new Advisor(number, x, y);
                case 19:
                case 20: return new Bishop(number, x, y);
                case 21:
                case 22: return new Knight(number, x, y);
                case 23:
                case 24: return new Rook(number, x, y);
                case 25:
                case 26: return new Cannon(number, x, y);
                case 27:
                case 28:
                case 29:
                case 30:
                case 31: return new Pond(number, x, y);
            }
            return new Empty(number, x, y);
        }
    }
}