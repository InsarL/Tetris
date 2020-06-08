using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Tetris
{
    class Game
    {
        public const int HorizontalSizePlayingField = 10;
        public const int VerticalSizePlayingField = 20;
        public int CellSize = 25;

        public List<Point> BusyCell = new List<Point>();
        public int Score = 0;
        public event Action Defeat;
        public Figures figure = new Figures();
        public List<Point> SpecificFigure = new List<Point>();


        public void MovementFigure(Keys key)
        {

            if (key == Keys.Left && IsPossibleMoveShapeInThisDirection(figure.Square.X - 1, figure.Square.Y))
                figure.Square.X--;

            if (key == Keys.Right && IsPossibleMoveShapeInThisDirection(figure.Square.X + 1, figure.Square.Y))
                figure.Square.X++;

            if (key == Keys.Down
                && IsPossibleMoveShapeInThisDirection(figure.Square.X, figure.Square.Y + 1))
                figure.Square.Y++;

            if (key == Keys.Space)
            {
                while (IsPossibleMoveShapeInThisDirection(figure.Square.X, figure.Square.Y + 1))
                    figure.Square.Y++;
                BusyCell.Add(new Point(figure.Square.X, figure.Square.Y));
                figure.Square.Y = -1;
            }

            if (key == Keys.Up)
                return;

        }

        public void Draw(Graphics graphiks)
        {
            for (int i = 0; i <= HorizontalSizePlayingField; i++)
            {
                graphiks.DrawLine(Pens.Black, 0, i * CellSize, HorizontalSizePlayingField * CellSize, i * CellSize);
                graphiks.DrawLine(Pens.Black, 0, (HorizontalSizePlayingField + i) * CellSize,
                                                  HorizontalSizePlayingField * CellSize, (HorizontalSizePlayingField + i) * CellSize);
                graphiks.DrawLine(Pens.Black, i * CellSize, 0, i * CellSize, VerticalSizePlayingField * CellSize);
            }

            graphiks.FillRectangle(Brushes.BlueViolet, figure.Square.X * CellSize, figure.Square.Y * CellSize, CellSize, CellSize);

            foreach (Point cell in BusyCell)
            {
                graphiks.FillRectangle(Brushes.BlueViolet, cell.X * CellSize, cell.Y * CellSize, CellSize, CellSize);
            }

            foreach (Point cell in SpecificFigure)
            {
                graphiks.FillRectangle(Brushes.BlueViolet, cell.X * CellSize, cell.Y * CellSize, CellSize, CellSize);
            }
        }



        public bool IsPossibleMoveShapeInThisDirection(int x, int y)
        {
            return x < HorizontalSizePlayingField && y < VerticalSizePlayingField && x >= 0 && y >= 0 && !BusyCell.Contains(new Point(x, y));
        }

        public void Update()
        {
            SpecificFigure = figure.RandomizationFigures();
            int lineСountingAndScoresDeduction = 0;
            if (figure.Square.Y == VerticalSizePlayingField - 1 || BusyCell.Contains(new Point(figure.Square.X, figure.Square.Y + 1)))
            {
                BusyCell.Add(new Point(figure.Square.X, figure.Square.Y));
                figure.Square.Y = -1;
            }

            if (BusyCell.Contains(new Point(figure.Square.X, figure.Square.Y + 1)) && figure.Square.Y < 1)
            {
                Score = 0;
                BusyCell.Clear();
                figure.Square = new Point(HorizontalSizePlayingField / 2, -1);
                Defeat();
            }

            for (int i = 0; i < VerticalSizePlayingField; i++)
            {
                List<Point> cellsUp = new List<Point>();
                if (BusyCell.Count(x => x.Y == i) == HorizontalSizePlayingField)
                {
                    cellsUp = BusyCell.Where(x => x.Y < i).Select(point => new Point(point.X, point.Y + 1)).ToList();
                    BusyCell.RemoveAll(x => x.Y <= i);
                    BusyCell.AddRange(cellsUp);
                    lineСountingAndScoresDeduction++;
                }
            }



            switch (lineСountingAndScoresDeduction)
            {
                case 0:
                    lineСountingAndScoresDeduction = 0;
                    break;
                case 1:
                    lineСountingAndScoresDeduction = 100;
                    break;
                case 2:
                    lineСountingAndScoresDeduction = 300;
                    break;
                case 3:
                    lineСountingAndScoresDeduction = 700;
                    break;
                default:
                    lineСountingAndScoresDeduction = 1500;
                    break;
            }
            Score += lineСountingAndScoresDeduction;
            figure.Square.Y++;

        }
    }
}
