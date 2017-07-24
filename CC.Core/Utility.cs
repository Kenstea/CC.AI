namespace CC.Core
{
    public class Utility
    {
        //Convert x, y to position in one-dimentional array
        public static int GetOneDimention(int x, int y)
        {
            return x + y * 9;
        }

        public static int DistanceSquare(int x1, int y1, int x2, int y2)
        {
            return (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
        }

        public static int Abs(int x)
        {
            if (x > 0)
                return x;
            return -x;
        }
    }
}