using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Figure
    {
        public int OriginX = 12;
        public int OriginY = 3;
        Random random = new Random();

        private Point[] FigureO()
        {
            return new[]
            {
                new Point(OriginX, OriginY),
                new Point(OriginX+1, OriginY),
                new Point(OriginX, OriginY-1),
                new Point(OriginX+1, OriginY-1)
            };
        }

        private Point[] FigureJ()
        {
            return new []
            {
                new Point(OriginX, OriginY),
                new Point(OriginX, OriginY+1),
                new Point(OriginX, OriginY-1), 
                new Point(OriginX-1, OriginY+1)
            };
        }

        private Point[] FigureL()
        {
            return new[]
            {
                new Point(OriginX, OriginY),
                new Point(OriginX, OriginY+1),
                new Point(OriginX, OriginY-1),
                new Point(OriginX+1, OriginY+1)
            };
        }

        private Point[] FigureS()
        {
            return new []
            {
                new Point(OriginX, OriginY), 
                new Point(OriginX, OriginY-1),
                new Point(OriginX+1, OriginY-1),
                new Point(OriginX-1, OriginY)};
        }

        private Point[] FigureZ()
        {
            return new []
            {
                new Point(OriginX, OriginY),
                new Point(OriginX+1, OriginY),
                new Point(OriginX, OriginY-1),
                new Point(OriginX-1, OriginY-1)
            };
        }

        private Point[] FigureT()
        {
            return new [] 
            {
                new Point(OriginX, OriginY),
                new Point(OriginX, OriginY+1),
                new Point(OriginX-1, OriginY),
                new Point(OriginX+1, OriginY)
            };
        }

        private Point[] FigureI()
        {
            return new [] 
            {
                new Point(OriginX, OriginY),
                new Point(OriginX-1, OriginY),
                new Point(OriginX+1, OriginY),
                new Point(OriginX+2, OriginY)
            };
        }

        private Point[] FigureSquare()
        {
            return new [] { new Point(OriginX, OriginY) };
        }

        public Point[] CreateRandomFigure()
        {
            int randomFigure = random.Next(1, 9);

            switch (randomFigure)
            {
                case 1: return FigureO();
                case 2: return FigureJ();
                case 3: return FigureL();
                case 4: return FigureS();
                case 5: return FigureZ();
                case 6: return FigureT();
                case 7: return FigureI();
                case 8: return FigureSquare();
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}
