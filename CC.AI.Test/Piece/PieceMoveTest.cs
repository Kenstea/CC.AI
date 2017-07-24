using System;
using System.Collections.Generic;
using System.Diagnostics;
using CC.Core;
using CC.Core.Algorithm;
using CC.Core.Piece;
using CC.Core.Pieces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CC.AI.Test.Piece
{
    [TestClass]
    public class PieceMoveTest
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void TestMovePieceSimple()
        {
            var states = new State();

            states = PieceMove.MovePiece(states, 7, 7, 4, 7);//user
           
            Debug.WriteLine(states.ToString());
            //states = PieceMove.MovePiece(states, 1, 2, 1, 9);//com
            states = (AlphaBetaSearch.DoSearch(states, 1));
            Debug.WriteLine(states.ToString());
            states = PieceMove.MovePiece(states, 0, 9, 1, 9);//user
            Debug.WriteLine(states.ToString());
            Assert.AreEqual(30, states.GetPieceList().Count);
            var piece = PieceFactory.GetPiece(23, 1, 9);
            Assert.AreEqual(typeof(Rook), piece.GetType());
        }

        [TestMethod]
        public void TestMovePieceLegal()
        {
            var states = new State();

            PieceMove.MovePieceLegal(states, 7, 7, 4, 7);//user
            Debug.WriteLine(states.ToString());
            
            states = (AlphaBetaSearch.DoSearch(states, 1));
            Debug.WriteLine(states.ToString());

            PieceMove.MovePieceLegal(states, 0, 9, 1, 9);//user
            Debug.WriteLine(states.ToString());

            Assert.AreEqual(30, states.GetPieceList().Count);

            var piece = PieceFactory.GetPiece(23, 1, 9);
            Assert.AreEqual(typeof(Rook), piece.GetType());
            Assert.AreEqual("R",piece.ToString());

            Assert.AreEqual("R",states.GetPieceList()[82].ToString());
        }
    }
}
