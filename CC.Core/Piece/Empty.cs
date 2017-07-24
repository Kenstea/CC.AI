using System;
using System.Collections.Generic;

namespace CC.Core.Piece
{
    [Serializable]
    public class Empty : PieceBase
    {
        public Empty(int number) : base(number)
        {
        }

        public Empty(int number, int x, int y) : base(number, x, y)
        {
        }

        public override int EvaluateExistence()
        {
            return 0;
        }

        public override int EvaluateMobility(State state, int fromX, int fromY)
        {
            return 0;
        }

        public override int EvaluateStatic(int x, int y)
        {
            return 0;
        }

        public override List<Move> GenerateAllMove(State state, int fromX, int fromY)
        {
            return null;
        }

        public override bool IsLegalMove(State state, int fromX, int fromY, int toX, int toY)
        {
            return false;
        }

        public override string ToString()
        {
            return "+";
        }
    }
}