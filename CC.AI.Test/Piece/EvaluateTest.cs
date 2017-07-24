using System;
using CC.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CC.AI.Test.Piece
{
    [TestClass]
    public class EvaluateTest
    {
        private State _currentState = new State();

        [TestMethod]
        public void TestMethod1()
        {
        }
        [TestMethod]
        public void TestInitValue()
        {
            int value = Evaluate.EvaluateState(_currentState);
            Assert.AreEqual(0,value);
        }
        [TestMethod]
        public void TestRook()
        {
            int count = 0;
            for (int y = 0; y <= 9; y++)
            {
                for (int x = 0; x <= 8; x++)
                {
                    int k = y * 9 + x;
                    int m = (9 - y) * 9 + 8 - x;
                    if (!Evaluate.BlackRookPositionValue[m].Equals(Evaluate.RedRookPositionValue[k]))
                    {
                        Console.WriteLine(x);
                        Console.WriteLine(y);
                        Console.WriteLine("\n");
                        count++;
                    }
                }
            }
            Assert.AreEqual(0,0);
        }
        [TestMethod]
        public void TestAllPieces()
        {
            int count = 0;
            for (int y = 0; y <= 9; y++)
            {
                for (int x = 0; x <= 8; x++)
                {
                    int k = y * 9 + x;
                    int m = (9 - y) * 9 + 8 - x;
                    if (!Evaluate.BlackCannonPositionValue[m].Equals(Evaluate.RedCannonPositionValue[k]))
                    {
                        Console.WriteLine(x);
                        Console.WriteLine(y);
                        Console.WriteLine("(Cannon)\n");
                        count++;
                        count++;
                    }
                    else if (!Evaluate.BlackPondPositionValue[m].Equals(Evaluate.RedPondPositionValue[k]))
                    {
                        Console.WriteLine(x);
                        Console.WriteLine(y);
                        Console.WriteLine("(Pond)\n");
                        count++;
                    }
                    else if (!Evaluate.BlackBishopPositionValue[m].Equals(Evaluate.RedBishopPositionValue[k]))
                    {
                        Console.WriteLine(x);
                        Console.WriteLine(y);
                        Console.WriteLine("(Bishop)\n");
                        count++;
                    }
                    else if (!Evaluate.BlackKingPositionValue[m].Equals(Evaluate.RedKingPositionValue[k]))
                    {
                        Console.WriteLine(x);
                        Console.WriteLine(y);
                        Console.WriteLine("(King)\n");
                        count++;
                    }
                    else if (!Evaluate.BlackKnightPositionValue[m].Equals(Evaluate.RedKnightPositionValue[k]))
                    {  
                        Console.WriteLine(x);
                        Console.WriteLine(y);
                        Console.WriteLine("(Knight)\n");
                        count++;
                    }
                }
            }
            //		Utility.debug("Number of Inconsistent pair:");
            //		Utility.debug(count);
            Assert.AreEqual(0,0);
        }

        [TestMethod]
        public void TestSingleRook()
        {
            if (!Evaluate.BlackRookPositionValue[9 * 0 + 0].Equals(Evaluate.RedRookPositionValue[9 * 9 + 0]))
            {
                Console.WriteLine("Inconsistent");
                Console.WriteLine(Evaluate.BlackRookPositionValue[9 * 0 + 0]);
                Console.WriteLine(Evaluate.RedRookPositionValue[9 * 9 + 0]);
            }
            Assert.AreEqual(Evaluate.BlackRookPositionValue[9 * 0 + 0], Evaluate.RedRookPositionValue[9 * 9 + 0]) ;
        }
    }
}
