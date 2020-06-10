using System;
using System.Collections.Generic;
using System.Deployment.Application;
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
        public static Figures figure = new Figures();
        public static List<Point> NextFigure = figure.RandomizationFigures();
        public List<Point> SpecificFigure = NextFigure.Select(x => new Point(x.X - 7, x.Y - 4)).ToList();

        public void MovingFigureToGameField()
        {
            if (SpecificFigure.Count() == 0)
                SpecificFigure = NextFigure.Select(x => new Point(x.X - 7, x.Y - 4)).ToList();

            NextFigure = figure.RandomizationFigures();
        }

        public void MovementFigure(Keys key)
        {
            if (key == Keys.Left && SpecificFigure.All(x => IsPossibleMoveShapeInThisDirection(x.X - 1, x.Y)))
                SpecificFigure = SpecificFigure.Select(x => new Point(x.X - 1, x.Y)).ToList();

            if (key == Keys.Right && SpecificFigure.All(x => IsPossibleMoveShapeInThisDirection(x.X + 1, x.Y)))
                SpecificFigure = SpecificFigure.Select(x => new Point(x.X + 1, x.Y)).ToList();

            if (key == Keys.Down && SpecificFigure.All(x => IsPossibleMoveShapeInThisDirection(x.X, x.Y + 1)))
                SpecificFigure = SpecificFigure.Select(x => new Point(x.X, x.Y + 1)).ToList();

            if (key == Keys.Space)
            {
                while (SpecificFigure.All(x => IsPossibleMoveShapeInThisDirection(x.X, x.Y + 1)))
                    SpecificFigure = SpecificFigure.Select(x => new Point(x.X, x.Y + 1)).ToList();
            }

            if (key == Keys.Up)
            {
                Point pivotPoint = SpecificFigure.First();
                SpecificFigure = SpecificFigure.Select(point =>
                                        new Point(point.X = point.Y - pivotPoint.Y + pivotPoint.X,
                                        point.Y = point.X - point.X + point.Y)).ToList();
            }

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

            foreach (Point cell in BusyCell)
                graphiks.FillRectangle(Brushes.BlueViolet, cell.X * CellSize, cell.Y * CellSize, CellSize, CellSize);

            foreach (Point cell in SpecificFigure)
                graphiks.FillRectangle(Brushes.BlueViolet, cell.X * CellSize, cell.Y * CellSize, CellSize, CellSize);

            foreach (Point cell in NextFigure)
                graphiks.FillRectangle(Brushes.BlueViolet, cell.X * CellSize, cell.Y * CellSize, CellSize, CellSize);
        }

        public bool IsPossibleMoveShapeInThisDirection(int x, int y)
        {
            return x < HorizontalSizePlayingField && y < VerticalSizePlayingField && x >= 0 && y >= 0 && !BusyCell.Contains(new Point(x, y));
        }

        public void Update()
        {

            int lineСountingAndScoresDeduction = 0;

            if (SpecificFigure.Where(x => x.Y == VerticalSizePlayingField - 1).Count() != 0 
                || BusyCell.Intersect(SpecificFigure.Select(point=>new Point(point.X,point.Y+1))).Count()>0)
            {
                BusyCell.AddRange(SpecificFigure);
                SpecificFigure.Clear();
                MovingFigureToGameField();
                return;
            }

            if (BusyCell.Count() > 0 && 
                BusyCell.Intersect(SpecificFigure.Select(point => new Point(point.X, point.Y + 1)))
                .Contains(BusyCell.OrderBy(x => x.Y).First()))
            {
                Score = 0;
                Defeat();
                BusyCell.Clear();
                SpecificFigure.Clear();
                MovingFigureToGameField();
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
            SpecificFigure = SpecificFigure.Select(x => new Point(x.X, x.Y + 1)).ToList();
        }
    }
}
