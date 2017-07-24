using CC.Core;
using System.Windows.Forms;

namespace CC_AI
{
    public partial class Board : Form
    {           
        public Board()
        {
            InitializeComponent();            
            //var game = new Game();
            //InitBoard(game.currentState.getPieceList());
            //game.Run();
        }

        private void InitBoard(PieceMap<int, IPiece> pieceMap)
        {
            for (var y = 3; y <= 12; y++)
            {
                for (var x = 3; x <= 11; x++)
                {
                    var k = x + (y << 4);
                    var piece = pieceMap.Get(k);
                }
            }
        }
    }
}
