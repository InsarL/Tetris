using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    class Game
    {
        public static int HorizontalSizePlayingField = 10;
        public static int VerticalSizePlayingField = 20;
        public int CellSize = 25;
        public Point Square = new Point(HorizontalSizePlayingField / 2, -1);
        public Keys DirectionFigure;
        public List <Point> BusyCell = new List<Point>();

        public void Draw(Graphics graphiks)
        {
            for (int i = 0; i <= HorizontalSizePlayingField; i++)
            {
                graphiks.DrawLine(Pens.Black, 0, i * CellSize, HorizontalSizePlayingField * CellSize, i * CellSize);
                graphiks.DrawLine(Pens.Black, 0, (HorizontalSizePlayingField + i) * CellSize,
                                                  HorizontalSizePlayingField * CellSize, (HorizontalSizePlayingField + i) * CellSize);
                graphiks.DrawLine(Pens.Black, i * CellSize, 0, i * CellSize, VerticalSizePlayingField * CellSize);
            }

            graphiks.FillRectangle(Brushes.BlueViolet, Square.X * CellSize, Square.Y * CellSize, CellSize, CellSize);

            foreach  (Point cell in BusyCell)
            {
                graphiks.FillRectangle(Brushes.BlueViolet, cell.X * CellSize, cell.Y * CellSize, CellSize, CellSize);
            }
        }

        public void MovementFigure(Keys key)
        {
            if (key == Keys.Down ||
                key == Keys.Up ||
                key == Keys.Right ||
                key == Keys.Left)
                DirectionFigure = key;

            if (DirectionFigure == Keys.Left && IsPossibleMoveShapeInThisDirection( Square.X-1,Square.Y))
                Square.X--;

            if (DirectionFigure == Keys.Right && IsPossibleMoveShapeInThisDirection(Square.X + 1, Square.Y))
                Square.X++;

            if (DirectionFigure == Keys.Down
                && IsPossibleMoveShapeInThisDirection(Square.X, Square.Y+1))
                Square.Y++;

            if (DirectionFigure == Keys.Up)
                return;

            DirectionFigure = Keys.None;
        }

        public bool IsPossibleMoveShapeInThisDirection(int x, int y)
        {
            return x < HorizontalSizePlayingField && y < VerticalSizePlayingField && x >= 0 && y >= 0 && !BusyCell.Contains(new Point (x,y));
        }

        public void Update()
        {
            if (Square.Y == VerticalSizePlayingField - 1 || BusyCell.Contains(new Point (Square.X, Square.Y+1)))
            {
                BusyCell.Add(new Point(Square.X, Square.Y));
                Square.Y = -1;
            }

            Square.Y++;

            for (int i = 0; i < VerticalSizePlayingField; i++)
            {
                List<Point> cellsUp = new List<Point> ();
                if (BusyCell.Count(x => x.Y == i) == HorizontalSizePlayingField)
                {
                    BusyCell.Where(x => x.Y < i).Select(point => new Point(point.X, point.Y + 1)).ToList();
                    BusyCell.RemoveAll(x => x.Y <= i);
                    BusyCell.AddRange(cellsUp);
                }
            }
        }
    }
}
