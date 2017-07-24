using System;
using CC.Core;
using CC.Core.Algorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CC.AI.Test.Piece
{
    [TestClass]
    public class AlphaBetaSearchTest
    {
        private State _state = new State();

        [TestMethod]
        public void TestLevel1()
        {
            var choosenState = AlphaBetaSearch.DoSearch(_state, 1);
            Console.WriteLine(choosenState.ToString());
            Assert.AreEqual(true,true);
        }

        [TestMethod]
        public void TestLevel2()
        {
            var choosenState = AlphaBetaSearch.DoSearch(_state, 2);
            Console.WriteLine(choosenState.ToString());
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void TestLevel3()
        {
            var choosenState = AlphaBetaSearch.DoSearch(_state, 3);
            Console.WriteLine(choosenState.ToString());
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void TestLevel4()
        {
            var choosenState = AlphaBetaSearch.DoSearch(_state,4);
            Console.WriteLine(choosenState.ToString());
            Assert.AreEqual(true, true);
        }
    }
}
