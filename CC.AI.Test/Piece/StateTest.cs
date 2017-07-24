using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using CC.Core;
using CC.Core.Piece;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CC.AI.Test.Piece
{
    [TestClass]
    public class StateTest
    {
        private State _state;
        private State State => _state ?? (_state = new State());

        private State _state2;
        private State State2 => _state2 ?? (_state2 = new State());


        [TestMethod]
        public void TestMethod1()
        {

        }

        private void Setup()
        {
            State.InitializePieceStateList();
        }

        [TestMethod]
        public void TestPieceList()
        {
            Setup();
            var fromX = 1;
            var fromY = 9;
            var toX = 2;
            var toY = 7;
            var midState = PieceMove.MovePiece(State, fromX, fromY, toX, toY);
            var pieceList = midState.GetPieceList();
            var sizeBefore = pieceList.Count;

            var toXkillTeamMate = 3;
            var toYkillTeamMate = 9;

            midState = PieceMove.MovePiece(midState, toX, toY, toXkillTeamMate, toYkillTeamMate);
            pieceList = midState.GetPieceList();
            var sizeAfter = pieceList.Count;
            Assert.AreEqual(sizeBefore,sizeAfter + 1);
        }

        [TestMethod]
        public void TestPieceListSimple()
        {
            int fromX = 1;
            int fromY = 9;
            int toX = 2;
            int toY = 7;

            var pieceListBefore = State.GetPieceList();
            var midState = PieceMove.MovePiece(State,fromX, fromY, toX, toY);
            var pieceListAfter = midState.GetPieceList();

            var piece1 = pieceListBefore.Get(Utility.GetOneDimention(fromX, fromY));
            var piece2 = pieceListAfter.Get(Utility.GetOneDimention(fromX, fromY));
            Console.WriteLine(midState);
            Assert.AreNotEqual(piece1.GetNumber(),piece2.GetNumber());
        }

        [TestMethod]
        public void TestPieceListPrint()
        {
            var stringBefore = State.ToString(State.GetPieceList());
            int fromX = 1;
            int fromY = 9;
            int toX = 2;
            int toY = 7;
            State midState = PieceMove.MovePiece(State, fromX, fromY, toX, toY);
            String stringAfter = midState.ToString(midState.GetPieceList());
            Assert.AreNotEqual(stringBefore,stringAfter);
        }

        [TestMethod]
        public void TestGenerateAllState()
        {
            var newMoveList = State.GenerateAllMoves(State.CompTurn);
            var anotherMoveList = State2.GenerateAllMoves(State.UserTurn);

            Assert.AreEqual(newMoveList.Count,anotherMoveList.Count);
            Assert.AreEqual(anotherMoveList.Count,44);
        }

      
    }
}
