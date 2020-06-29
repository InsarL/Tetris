
using System.Drawing;
using System.Linq;

namespace Tetris
{
    class Figure
    {
        public int X;
        public int Y;
        public Point[] PointsOnGameField => points.Select(point => new Point(point.X + X, point.Y + Y)).ToArray();
        private int rotateNumber = 0;
        private FigureType figureType;
        private Point[] points;
        
        public Figure(FigureType figureType, params Point[] points)
        {
            this.figureType = figureType;
            this.points = points;
        }

        public void Draw(Graphics graphics, int cellSize)
        {
            foreach (Point cell in PointsOnGameField)
                graphics.FillRectangle(Brushes.BlueViolet, cell.X * cellSize, cell.Y * cellSize, cellSize, cellSize);
        }

        public void Rotate()
        {
            switch (figureType)
            {
                case FigureType.O:
                case FigureType.Square:
                    return;
                case FigureType.I:
                case FigureType.S:
                case FigureType.Z:
                    if (rotateNumber == 0)
                    {
                        rotateNumber++;
                         RotateClockwise();
                    }
                    else
                    {
                        rotateNumber--;
                         RotateCounterClockwise();
                    }
                    break;
                case FigureType.J:
                case FigureType.L:
                case FigureType.T:
                   RotateClockwise();
                    break;
            }
        }

        public Figure Clone()
        {
            return new Figure(figureType, points.ToArray())
            {
                X = this.X,
                Y = this.Y,
                rotateNumber = this.rotateNumber
            };
        }

        private void RotateClockwise()
        {
          points = points.Select(point => new Point(- point.Y,point.X )).ToArray();
        }

        private void RotateCounterClockwise()
        {
           points = points.Select(point => new Point(point.Y, -point.X)).ToArray();
        }
    }
}
