using System;
using CC.Core.Piece;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CC.AI.Test.Piece
{
    [TestClass]
    public class PieceFactoryTest
    {
        [TestMethod]
        public void TestGetRookPiece()
        {
            var piece = PieceFactory.GetPiece(23, 1, 9);
            Assert.AreEqual(typeof(Rook),piece.GetType());
        }

        [TestMethod]
        public void TestGetEmptyPiece()
        {
            var piece = PieceFactory.GetPiece(0, 0, 9);
            Assert.AreEqual(typeof(Empty), piece.GetType());
        }

      
    }
}
