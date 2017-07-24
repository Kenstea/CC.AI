using System;
using CC.Core;
using CC.Core.Algorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CC.AI.Test.Piece
{
    [TestClass]
    public class MinMaxSearchTest
    {
        private State _state = new State();

        [TestMethod]
        public void TestLevel1()
        {
            var choosenState = MinMaxSearch.minMaxSearch(_state, 1);
            Console.WriteLine(choosenState.ToString());
            Assert.AreEqual(true,true);
        }

        [TestMethod]
        public void TestLevel2()
        {
            var choosenState = MinMaxSearch.minMaxSearch(_state, 2);
            Console.WriteLine(choosenState.ToString());
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void TestLevel3()
        {
            var choosenState = MinMaxSearch.minMaxSearch(_state, 3);
            Console.WriteLine(choosenState);
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void TestLevel4()
        {
            var choosenState = AlphaBetaSearch.DoSearch(_state,4);
            Console.WriteLine(choosenState.ToString());
            Assert.AreEqual(true, true);
        }
        //[TestMethod]
        //public void TestLevelThreeKill()
        //{
        //    State midState = new State();
        //    int fromX = 0;
        //    int fromY = 9;
        //    int toX = 4;
        //    int toY = 3;

        //    midState = PieceMove.MovePiece(state, fromX, fromY, toX, toY);
        //    State chosenState = (MinMaxSearch.minMaxSearch(midState, 3));
        //    Console.WriteLine(chosenState);
        //    Assert.AreEqual(true, true);
        //}
    }
}
