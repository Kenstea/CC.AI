using System.Collections.Generic;

namespace CC.Core.Algorithm
{
    public class AlphaBetaSearch
    {
        public static State DoSearch(State state, int depth)
        {
            return MinSearch(state, depth, State.CompTurn, int.MinValue, int.MaxValue);
        }
        private static int ChangeSide(int side)
        {
            if (side == State.CompTurn) return State.UserTurn;
            return State.CompTurn;
        }
        private static State MinSearch(State state, int depth, int side, int alpha, int beta)
        {
            if (depth <= 0)
            {
                state.EvaluateValue();
                return state;
            }

            var newAlpha = alpha;
            var newBeta = beta;
            var moveList = state.GenerateAllMoves(side);
            var it = moveList.GetEnumerator();

            var minState = new State();
            minState.SetValue(newBeta);

            while (it.MoveNext())
            {
                var newState = PieceMove.MovePiece(state, it.Current);
                var newValue = MaxSearch(newState, depth - 1, ChangeSide(side), newAlpha, newBeta).GetValue();
                if (newValue < newBeta)
                {
                    minState = newState;
                    newBeta = newValue;
                    minState.SetValue(newValue);
                }
                if (newBeta <= newAlpha)
                {
                    return minState;
                }
            }
            return minState;
        }

        private static State MaxSearch(State state, int depth, int side, int alpha, int beta)
        {
            if (depth <= 0)
            {
                state.EvaluateValue();
                return state;
            }

            var newAlpha = alpha;
            var newBeta = beta;
            var moveList = state.GenerateAllMoves(side);
            var it = moveList.GetEnumerator();

            var maxState = new State();
            maxState.SetValue(newAlpha);

            while (it.MoveNext())
            {
                var newState = PieceMove.MovePiece(state, it.Current);
                var newValue = MinSearch(newState, depth - 1, ChangeSide(side), newAlpha, newBeta).GetValue();
                if (newValue > newAlpha)
                {
                    maxState = newState;
                    newAlpha = newValue;
                    maxState.SetValue(newValue);
                }
                if (newBeta <= newAlpha)
                {
                    return maxState;
                }
            }
            return maxState;
        }
    }
}
