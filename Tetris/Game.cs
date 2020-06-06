﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Game
    {
        public static int HorizontalSizePlayingField = 10;
        public int VerticalSizePlayingField = 20;
        public int CellSize = 25;
        public Point Square = new Point(HorizontalSizePlayingField/2, -1);



        public void Draw(Graphics graphiks)
        {
            for (int i = 0; i <= HorizontalSizePlayingField; i++)
            {
                graphiks.DrawLine(Pens.Black, 0, i * CellSize, HorizontalSizePlayingField * CellSize, i * CellSize);
                graphiks.DrawLine(Pens.Black, 0, (HorizontalSizePlayingField + i) * CellSize,
                                                  HorizontalSizePlayingField * CellSize, (HorizontalSizePlayingField + i) * CellSize);
                graphiks.DrawLine(Pens.Black, i * CellSize, 0, i * CellSize, VerticalSizePlayingField * CellSize);
            }

            graphiks.FillRectangle(Brushes.BlueViolet, Square.X*CellSize, Square.Y*CellSize, CellSize, CellSize);
        }

        public void Update()
        {
            Square.Y++;
            if (Square.Y == VerticalSizePlayingField)
                Square.Y = -1;
        }
    }
}
