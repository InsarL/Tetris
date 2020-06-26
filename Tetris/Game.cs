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
            specificFigure = nextFigure;
            MovingFigureToGameField();
        }

        public void MovingFigureToGameField()
        {
            specificFigure = nextFigure;
            specificFigure.Points = nextFigure.Points.Select(point => new Point(point.X - 7, point.Y - 4)).ToArray();
            nextFigure = FigureFactory.CreateRandomFigure();
        }

        public void MoveFigure(Keys key)
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

        public bool IsPossibleMoveFigure(int x, int y)
        {
            return x < gameFieldWidth && y < gameFieldHeight && x >= 0 && y >= 0 && !busyCells.Contains(new Point(x, y));
        }

        private int CalculateScore(int lines)
        {

            switch (lines)
            {
                case 0: return Score += 0;
                case 1: return Score += 100;
                case 2: return Score += 300;
                case 3: return Score += 700;
                case 4: return Score += 1500;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public void Update()
        {
            if (specificFigure.Points.First() == new Point(FigureFactory.OriginX, FigureFactory.OriginY))
                MovingFigureToGameField();

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
                Defeat();

            int lines = 0;
            for (int i = 0; i < gameFieldHeight; i++)
                if (busyCells.Count(x => x.Y == i) == gameFieldWidth)
                {
                    List<Point> cellsUp = new List<Point>();
                    cellsUp = busyCells.Where(x => x.Y < i).Select(point => new Point(point.X, point.Y + 1)).ToList();
                    busyCells.RemoveAll(x => x.Y <= i);
                    busyCells.AddRange(cellsUp);
                    lines++;
                }
            CalculateScore(lines);
            specificFigure.Points = specificFigure.Points.Select(x => new Point(x.X, x.Y + 1)).ToArray();
        }
    }
}
