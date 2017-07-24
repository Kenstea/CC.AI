using System;
using CC.Core;
using CC.Core.Piece;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CC.AI.Test.Piece
{
    [TestClass]
    public class CloningTest
    {
        private State _state;
        private Cannon _cannon;
        private State State => _state ?? (_state = new State());
        private Cannon Cannon => _cannon ?? (_cannon = new Cannon(25));

        [TestMethod]
        public void TestCloning()
        {
            var fromX = 3;
            var fromY = 9;
            var toX = 4;
            var toY = 8;

            var before = State.ToString();
            PieceMove.MovePiece(State, fromX, fromY, toX, toY);
            var after = State.ToString();
            Assert.AreEqual(before,after);
        }

        [TestMethod]
        public void TestCloningCap()
        {
            var fromX = 1;
            var fromY = 7;
            var toX = 1;
            var toY = 0;

            var before = State.ToString();
            PieceMove.MovePiece(State, fromX, fromY, toX, toY);
            var after = State.ToString();
            Assert.AreEqual(before, after);
        }
    }
}
