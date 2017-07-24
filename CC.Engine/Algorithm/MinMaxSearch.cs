using System.Collections.Generic;

namespace CC.Core.Algorithm
{
    public class MinMaxSearch
    {
        public static State minMaxSearch(State state, int depth)
        {
            return MinSearch(state, depth, State.CompTurn);
        }

        private static State MinSearch(State state, int depth, int side)
        {
            if (depth <= 0)
            {
                state.EvaluateValue();
                return state;
            }

            var moveList = state.GenerateAllMoves(side);
            var it = moveList.GetEnumerator();

            State minState = null;
            var value = int.MaxValue;

            while (it.MoveNext())
            {                   
                var newState = PieceMove.MovePiece(state, it.Current);
                var newValue = MaxSearch(newState, depth - 1, ChangeSide(side)).GetValue();
                if (newValue < value)
                {
                    minState = newState;
                    value = newValue;
                    minState.SetValue(newValue);
                }
                
            }
            return minState;
        }

        private static State MaxSearch(State state, int depth, int side)
        {
            if (depth <= 0)
            {
                state.EvaluateValue();
                return state;
            }
            var moveList = state.GenerateAllMoves(side);
            var it = moveList.GetEnumerator();

            State maxState = null;
            var value = int.MaxValue;

            while (it.MoveNext())
            {
                var newState = PieceMove.MovePiece(state, it.Current);
                var newValue = MinSearch(newState, depth - 1, ChangeSide(side)).GetValue();
                if (newValue > value)
                {
                    maxState = newState;
                    value = newValue;
                    maxState.SetValue(newValue);
                }
            }
            return maxState;
        }

        private static int ChangeSide(int side)
        {
            if (side == State.CompTurn) return State.UserTurn;
            return State.CompTurn;
        }

    }
}
