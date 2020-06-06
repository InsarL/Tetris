using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Game
    {
        public int GameFieldSize = 10;
        public int CellSize = 25;
        public void Draw(Graphics graphiks)
        {
            for (int i = 0; i <= GameFieldSize; i++)
            {
                graphiks.DrawLine(Pens.Black, 0, i * CellSize, GameFieldSize*CellSize, i * CellSize);
                graphiks.DrawLine(Pens.Black, 0, (GameFieldSize+ i) * CellSize,
                                                 GameFieldSize * CellSize, (GameFieldSize+i) * CellSize);
                graphiks.DrawLine(Pens.Black, i * CellSize, 0, i * CellSize, GameFieldSize * CellSize);
                graphiks.DrawLine(Pens.Black, i*CellSize, GameFieldSize*CellSize,i*CellSize , 2*CellSize*GameFieldSize);

            }
            
        }
    }
}
