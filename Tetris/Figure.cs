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
        public int X = 12;
        public int Y = 4;
        private int rotateNumber = 0;
        private FigureType FigureType;
        private Point[] points;
        public Point[] PointsOnGameField => points.Select(point => new Point(point.X + X, point.Y + Y)).ToArray();

        public Figure(FigureType figureType, params Point[] points)
        {
            FigureType = figureType;
            this.points = points;
        }

        public void Draw(Graphics graphics, int cellSize)
        {
            foreach (Point cell in PointsOnGameField)
              graphics.FillRectangle(Brushes.BlueViolet, cell.X * cellSize, cell.Y * cellSize, cellSize, cellSize);
        }

        public Figure Rotate(Figure figure)
        {
            switch (FigureType)
            {
                case FigureType.O:
                case FigureType.Square:
                    return figure;

                case FigureType.I:
                case FigureType.S:
                case FigureType.Z:
                    if (rotateNumber == 0)
                    {
                        rotateNumber++;
                        return RotateClockwise(figure);
                    }

                    else
                    {
                        rotateNumber--;
                        return RotateCounterClockwise(figure);
                    }


                case FigureType.J:
                case FigureType.L:
                case FigureType.T:
                   return RotateClockwise(figure);

                default: throw new ArgumentOutOfRangeException();

            }
        }

        private Figure RotateClockwise(Figure figure)
        {
          return new Figure (figure.FigureType, figure.points = points.Select(point => new Point(- point.Y,point.X )).ToArray());
        }

        private Figure RotateCounterClockwise(Figure figure)
        {
           return new Figure(figure.FigureType, figure.points = points.Select(point => new Point(point.Y, -point.X)).ToArray());
        }
    }
}
