using System.Collections.Generic;

namespace CC.Core.Piece
{
    public interface IPiece
    {
        string ToString();
        int GetNumber();
        int GetSide();
        int EvaluateStatic(int x, int y);
        int EvaluateExistence();
        int EvaluateMobility(State state, int fromX, int fromY);
        List<Move> GenerateAllMove(State state, int fromX, int fromY);
        bool IsLegalMove(State state, int fromX, int fromY, int toX, int toY);
        int GetX();
        int GetY();
        int GetK();
        void SetPosition(int x, int y);
        object Clone();
    }
}