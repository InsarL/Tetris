using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Tetris
{
    class Game
    {
        public int Score;
        public event Action Defeat;
        private const int gameFieldWidth = 10;
        private const int gameFieldHeight = 20;
        private const int cellSize = 25;
        private List<Point> busyCells = new List<Point>();
        private Figure nextFigure;
        private Figure currentFigure;

        public void Restart()
        {
            busyCells.Clear();
            Score = 0;
            nextFigure = FigureFactory.CreateRandomFigure();

            AddNextFigure();
        }

        public void Draw(Graphics graphics)
        {
            for (int i = 0; i <= gameFieldWidth; i++)
                graphics.DrawLine(Pens.Black, i * cellSize, 0, i * cellSize, gameFieldHeight * cellSize);

            for (int i = 0; i <= gameFieldHeight; i++)
                graphics.DrawLine(Pens.Black, 0, i * cellSize, gameFieldWidth * cellSize, i * cellSize);

            foreach (Point cell in busyCells)
                graphics.FillRectangle(Brushes.BlueViolet, cell.X * cellSize, cell.Y * cellSize, cellSize, cellSize);

            currentFigure.Draw(graphics, cellSize);
            nextFigure.Draw(graphics, cellSize);
        }

        public void Update()
        {
            if (IsPossibleMoveFigure(currentFigure, 0, 1))
                currentFigure.Y++;
            else
            {
                busyCells.AddRange(currentFigure.PointsOnGameField);

                int countRemovedLines = RemoveFullLines();

                Score += CalculateScore(countRemovedLines);

                AddNextFigure();
            }
        }

        public void MoveFigure(Keys key)
        {
            if (key == Keys.Left && IsPossibleMoveFigure(currentFigure, -1, 0))
                currentFigure.X--;

            if (key == Keys.Right && IsPossibleMoveFigure(currentFigure, 1, 0))
                currentFigure.X++;

            if (key == Keys.Down && IsPossibleMoveFigure(currentFigure, 0, 1))
                currentFigure.Y++;

            if (key == Keys.Space)
            {
                while (IsPossibleMoveFigure(currentFigure, 0, 1))
                    currentFigure.Y++;
            }

            if (key == Keys.Up)
            {
                if (CanRotate(currentFigure))
                    currentFigure.Rotate();
            }
        }

        private void AddNextFigure()
        {
            currentFigure = nextFigure;

            //Устанавливаем фигуру сверху игрового поля
            currentFigure.X = gameFieldWidth / 2;
            currentFigure.Y = 1;

            nextFigure = FigureFactory.CreateRandomFigure();

            //Устанавливаем следующую фигуру под  полем "Следующая фигура"
            nextFigure.X = gameFieldWidth + 2;
            nextFigure.Y = 4;

            if (!IsPossibleMoveFigure(currentFigure, 0, 0))
                Defeat();
        }

        private bool CanRotate(Figure figure)
        {
           Figure figureCopy = figure.Clone();
            figureCopy.Rotate();
            return IsPossibleMoveFigure(figureCopy, 0, 0);
        }

        private bool IsPossibleMoveFigure(Figure figure, int offsetX, int offsetY)
        {
            return figure.PointsOnGameField.All(point => IsFreeCell(point.X + offsetX, point.Y + offsetY));
        }

        private bool IsFreeCell(int x, int y)
        {
            return x < gameFieldWidth && y < gameFieldHeight && x >= 0 && y >= 0 && !busyCells.Contains(new Point(x, y));
        }

        private int CalculateScore(int countRemovedLines)
        {
            switch (countRemovedLines)
            {
                case 0: return 0;
                case 1: return 100;
                case 2: return 300;
                case 3: return 700;
                case 4: return 1500;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private int RemoveFullLines()
        {
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

            return lines;
        }
    }
}
