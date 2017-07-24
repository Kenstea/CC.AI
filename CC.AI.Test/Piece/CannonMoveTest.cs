using CC.Core;
using CC.Core.Piece;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CC.AI.Test.Piece
{
    [TestClass()]
    public class CannonMoveTest
    {
        private State _state;
        private Cannon _cannon;
        private State State => _state ?? (_state = new State());
        private Cannon Cannon => _cannon ?? (_cannon = new Cannon(25));
        [TestMethod()]
        public void TestGenerateAllMoveSimple()
        {
            var fromX = 1;
            var fromY = 7;
            var newMoveList = Cannon.GenerateAllMove(State, fromX, fromY);
            Assert.AreEqual(12,newMoveList.Count);
        }

        [TestMethod]
        public void TestGenerateAllMoveAtMidState()
        {
            var fromX = 1;
            var fromY = 7;
            var toX = 4;
            var toY = 7;
            var midState = PieceMove.MovePiece(State, fromX, fromY, toX, toY);
            var newMoveList = Cannon.GenerateAllMove(midState, toX, toY);
            Assert.AreEqual(8,newMoveList.Count);
        }

        [TestMethod]
        public void TestLegalMove()
        {
            var fromX = 1;
            var fromY = 7;
            var toX = 1;
            var toY = 5;
            Assert.AreEqual(true,Cannon.IsLegalMove(State,fromX,fromY,toX,toY));
        }

        [TestMethod]
        public void TestIllLegalMove()
        {
            var fromX = 1;
            var fromY = 7;
            var toX = 1;
            var toY = 2;
            Assert.AreEqual(false, Cannon.IsLegalMove(State, fromX, fromY, toX, toY));
        }

        [TestMethod]
        public void TestIllLegalMoveAcross()
        {
            var fromX = 1;
            var fromY = 7;
            var toX = 1;
            var toY = 1;
            Assert.AreEqual(false, Cannon.IsLegalMove(State, fromX, fromY, toX, toY));
        }

        [TestMethod]
        public void TestLegalMoveSuicide()
        {
            var fromX = 1;
            var fromY = 7;
            var toX = 1;
            var toY = 9;

            var midState = PieceMove.MovePiece(State, fromX, fromY, toX, toY);
            var toXFinal = 1;
            var toYFinal = 9;
            Assert.AreEqual(false, Cannon.IsLegalMove(midState, toX, toY, toXFinal, toYFinal));
        }

        [TestMethod]
        public void TestLegalMoveWithObstacle()
        {
            var fromX = 1;
            var fromY = 7;
            var toX = 1;
            var toY = 0;
            Assert.AreEqual(true, Cannon.IsLegalMove(State, fromX, fromY, toX, toY));
        }
    }
}