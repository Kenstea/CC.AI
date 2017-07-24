using System;

namespace CC.Core
{
    public class Move
    {
        public enum MoveStatus
        {
            NoError,
            WrongPiece,
            Illegal
        }
        //public const int NoError = 0;
        //public const int WrongPiece = -1;
        //public const int IllegalMove = -2;
        public int FromX;
        public int FromY;
        public int ToX;
        public int ToY;

        public Move(int fromX, int fromY, int toX, int toY)
        {
            FromX = fromX;
            FromY = fromY;
            ToX = toX;
            ToY = toY;
        }

        public static Move GetReverse(Move move)
        {
            return new Move(move.ToX, move.ToY, move.FromX, move.FromY);
        }

        //public static void PrintError(int error)
        //{
        //    switch (error)
        //    {
        //        case WrongPiece:
        //            Console.WriteLine("Error: You moved a wrong piece. Please try again.\n");
        //            break;
        //        case IllegalMove:
        //            Console.WriteLine("Error: You moved a piece illegally. Please make sure you follow the rule.\n");
        //            break;
        //    }
        //}

        public static void PrintEror(MoveStatus moveStatus)
        {
            switch (moveStatus)
            {
                case MoveStatus.WrongPiece:
                    Console.WriteLine("Error: You moved a wrong piece. Please try again.\n");
                    break;
                case MoveStatus.Illegal:
                    Console.WriteLine("Error: You moved a piece illegally. Please make sure you follow the rule.\n");
                    break;
                case MoveStatus.NoError:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(moveStatus), moveStatus, null);
            }
        }
    }
}