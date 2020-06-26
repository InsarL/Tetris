using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Figure
    {
        public int X;
        public int Y;
        private FigureType FigureType;
        public  Point[] Points;

        public Figure(FigureType figureType, params Point[] points)
        {
            FigureType = figureType;
            Points = points;
        }

        public void Draw(Graphics graphics, int cellSize)
        {
            
            foreach (Point cell in Points)
              graphics.FillRectangle(Brushes.BlueViolet, cell.X * cellSize, cell.Y * cellSize, cellSize, cellSize);
        }

        public void Rotate()
        {
            switch (FigureType)
            {
                case FigureType.O:
                case FigureType.Square:
                    return;

                case FigureType.I:
                case FigureType.S:
                case FigureType.Z:
                    if (Points[1].Y == Points[2].Y)
                        RotateClockwise();
                    else
                        RotateCounterClockwise();
                    break;

                case FigureType.J:
                case FigureType.L:
                case FigureType.T:
                    RotateClockwise();
                    break;
            }
        }

        private void RotateClockwise()
        {
            Point pivotPoint = Points.First();
             Points = Points.Select(point =>
                           new Point(pivotPoint.X + (pivotPoint.Y - point.Y),
                             pivotPoint.Y + (point.X - pivotPoint.X))).ToArray();
        }

        private void RotateCounterClockwise()
        {
            Point pivotPoint = Points.First();

             Points = Points.Select(point =>
                           new Point(pivotPoint.X + (point.Y - pivotPoint.Y),
                             pivotPoint.Y + (pivotPoint.X - point.X))).ToArray();
        }
    }
}
