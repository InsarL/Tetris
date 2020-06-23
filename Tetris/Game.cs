using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Tetris
{
    class Game
    {
        public int Score = 0;
        public event Action Defeat;
        private const int gameFieldWidth = 10;
        private const int gameFieldHeight = 20;
        private int cellSize = 25;
        private List<Point> busyCells = new List<Point>();
        
        private static Figure nextFigure = Figure.CreateRandomFigure();
        private static Figure specificFigure = nextFigure;

        public void MovingFigureToGameField()
        {
            specificFigure = nextFigure;
            specificFigure.Points = nextFigure.Points.Select(x => new Point(x.X - 7, x.Y - 4)).ToArray();
            nextFigure = Figure.CreateRandomFigure();
        }


        public void MovementFigure(Keys key)
        {
            if (key == Keys.Left && specificFigure.Points.All(x => IsPossibleMoveFigure(x.X - 1, x.Y)))
                specificFigure.Points = specificFigure.Points.Select(x => new Point(x.X - 1, x.Y)).ToArray();

            if (key == Keys.Right && specificFigure.Points.All(x => IsPossibleMoveFigure(x.X + 1, x.Y)))
                specificFigure.Points = specificFigure.Points.Select(x => new Point(x.X + 1, x.Y)).ToArray();

            if (key == Keys.Down && specificFigure.Points.All(x => IsPossibleMoveFigure(x.X, x.Y + 1)))
                specificFigure.Points = specificFigure.Points.Select(x => new Point(x.X, x.Y + 1)).ToArray();

            if (key == Keys.Space)
            {
                while (specificFigure.Points.All(x => IsPossibleMoveFigure(x.X, x.Y + 1)))
                    specificFigure.Points = specificFigure.Points.Select(x => new Point(x.X, x.Y + 1)).ToArray();
            }

            if (key == Keys.Up)
            {
                Point pivotPoint = specificFigure.Points.First();
                specificFigure.Points = specificFigure.Points.Select(point =>
                new Point(pivotPoint.X + (pivotPoint.Y - point.Y),
                  pivotPoint.Y + (point.X - pivotPoint.X))).ToArray();
            }

        }

        public void Draw(Graphics graphics)
        {
            for (int i = 0; i <= gameFieldWidth; i++)
                graphics.DrawLine(Pens.Black, i * cellSize, 0, i * cellSize, gameFieldHeight * cellSize);

            for (int i = 0; i <= gameFieldHeight; i++)
                graphics.DrawLine(Pens.Black, 0, i * cellSize, gameFieldWidth * cellSize, i * cellSize);


            foreach (Point cell in busyCells)
                graphics.FillRectangle(Brushes.BlueViolet, cell.X * cellSize, cell.Y * cellSize, cellSize, cellSize);

            foreach (Point cell in specificFigure.Points)
                graphics.FillRectangle(Brushes.BlueViolet, cell.X * cellSize, cell.Y * cellSize, cellSize, cellSize);

            foreach (Point cell in nextFigure.Points)
                graphics.FillRectangle(Brushes.BlueViolet, cell.X * cellSize, cell.Y * cellSize, cellSize, cellSize);
        }

        public bool IsPossibleMoveFigure(int x, int y)
        {
            return x < gameFieldWidth && y < gameFieldHeight && x >= 0 && y >= 0 && !busyCells.Contains(new Point(x, y));
        }

        public void Update()
        {
            int lineСountingAndScoresDeduction = 0;
            
            if (specificFigure.Points.Where(x => x.Y == gameFieldHeight - 1).Count() != 0
                || busyCells.Intersect(specificFigure.Points.Select(point => new Point(point.X, point.Y + 1))).Count() > 0)
            {
                busyCells.AddRange(specificFigure.Points);
                MovingFigureToGameField();
                return;
            }

            if (busyCells.Count() > 0 &&
                busyCells.Intersect(specificFigure.Points.Select(point => new Point(point.X, point.Y + 1)))
                .Contains(busyCells.OrderBy(x => x.Y).First()))
            {
                Score = 0;
                Defeat();
                busyCells.Clear();
                MovingFigureToGameField();
            }

            for (int i = 0; i < gameFieldHeight; i++)
            {
                List<Point> cellsUp = new List<Point>();
                if (busyCells.Count(x => x.Y == i) == gameFieldWidth)
                {
                    cellsUp = busyCells.Where(x => x.Y < i).Select(point => new Point(point.X, point.Y + 1)).ToList();
                    busyCells.RemoveAll(x => x.Y <= i);
                    busyCells.AddRange(cellsUp);
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
                case 4:
                    lineСountingAndScoresDeduction = 1500;
                    break;
                default: throw new ArgumentOutOfRangeException();
            }
            Score += lineСountingAndScoresDeduction;
            specificFigure.Points = specificFigure.Points.Select(x => new Point(x.X, x.Y + 1)).ToArray();
        }
    }
}
