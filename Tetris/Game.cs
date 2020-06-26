using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace Tetris
{
    class Game
    {
        public int Score;
        public event Action Defeat;
        public const int gameFieldWidth = 10;
        public const int gameFieldHeight = 20;
        private const int cellSize = 25;
        private List<Point> busyCells = new List<Point>();
        private Figure nextFigure;
        private Figure specificFigure;

        public void Restart()
        {
            busyCells.Clear();
            Score = 0;
            nextFigure = FigureFactory.CreateRandomFigure();
            nextFigure.PointOnGameField.Select(point => new Point(12, 4)).ToArray();
            AddNextFigure();
        }

        private void AddNextFigure()
        {
            specificFigure = nextFigure;
            specificFigure.PointOnGameField = nextFigure.PointOnGameField.Select(point => new Point(5, 2)).ToArray();
            nextFigure = FigureFactory.CreateRandomFigure();
        }

        private bool IsFreeCell(int x, int y)
        {
            return x < gameFieldWidth && y < gameFieldHeight && x >= 0 && y >= 0 && !busyCells.Contains(new Point(x, y));
        }

        private bool IsPossibleMoveFigure(Figure figure, int offsetX, int offsetY)
        {
            return figure.PointOnGameField.All(point =>  IsFreeCell(point.X + offsetX, point.Y + offsetY));
        }

        public void MoveFigure(Keys key)
        {
            if (key == Keys.Left && IsPossibleMoveFigure(specificFigure, -1, 0))
                specificFigure.PointOnGameField = specificFigure.PointOnGameField.Select(point => new Point(point.X - 1, point.Y)).ToArray();

            if (key == Keys.Right && IsPossibleMoveFigure(specificFigure,  1, 0))
                specificFigure.PointOnGameField = specificFigure.PointOnGameField.Select(point => new Point(point.X + 1, point.Y)).ToArray();

            if (key == Keys.Down && IsPossibleMoveFigure(specificFigure, 0, 1))
                specificFigure.PointOnGameField = specificFigure.PointOnGameField.Select(point => new Point(point.X, point.Y + 1)).ToArray();

            if (key == Keys.Space)
            {
                while (IsPossibleMoveFigure(specificFigure, 0, 1))
                    specificFigure.PointOnGameField = specificFigure.PointOnGameField.Select(point => new Point(point.X, point.Y + 1)).ToArray();
            }

            if (key == Keys.Up)
            {
                specificFigure.Rotate();
            }

        }

        public void Draw(Graphics graphics)
        {
            for (int i = 0; i <= gameFieldWidth; i++)
                graphics.DrawLine(Pens.Black, i * cellSize, 0, i * cellSize, gameFieldHeight * cellSize);

            for (int i = 0; i <= gameFieldHeight; i++)
                graphics.DrawLine(Pens.Black, 0, i * cellSize, gameFieldWidth * cellSize, i * cellSize);

            specificFigure.Draw(graphics, cellSize);
            nextFigure.Draw(graphics, cellSize);

            foreach (Point cell in busyCells)
                graphics.FillRectangle(Brushes.BlueViolet, cell.X * cellSize, cell.Y * cellSize, cellSize, cellSize);


        }



        private int CalculateScore(int lines)
        {
            switch (lines)
            {
                case 0: return 0;
                case 1: return 100;
                case 2: return 300;
                case 3: return 700;
                case 4: return 1500;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public void Update()
        {

            if (specificFigure.PointOnGameField.Any(x => x.Y == gameFieldHeight - 1)
                || busyCells.Intersect(specificFigure.PointOnGameField.Select(point => new Point(point.X, point.Y + 1))).Any())
            {
                busyCells.AddRange(specificFigure.PointOnGameField);
                AddNextFigure();
                return;
            }

            if (specificFigure.PointOnGameField.Intersect(busyCells).Any())
                Defeat();

            int lines = 0;
            for (int i = 0; i < gameFieldHeight; i++)
                if (busyCells.Count(x => x.Y == i) == gameFieldWidth)
                {
                    Point[] cellsUp = busyCells
                        .Where(x => x.Y < i)
                        .Select(point => new Point(point.X, point.Y + 1))
                        .ToArray();
                    busyCells.RemoveAll(x => x.Y <= i);
                    busyCells.AddRange(cellsUp);
                    lines++;
                }
            Score += CalculateScore(lines);
            specificFigure.PointOnGameField = specificFigure.PointOnGameField.Select(x => new Point(x.X, x.Y + 1)).ToArray();
        }
    }
}
