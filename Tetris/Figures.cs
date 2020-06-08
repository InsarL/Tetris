using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Figures
    {
        public int StartingPointX = 12;
        public int StartingPointY = 3;
        Random random = new Random();
        public Point Square = new Point(Game.HorizontalSizePlayingField / 2, -1);

        public List<Point> PointsFigure = new List<Point>();

        public List<Point> RandomizationFigures()
        {
            int randomFigure = random.Next(1, 8);

            switch (randomFigure)
            {
                case 1: return FigureO();
                case 2: return FigureJ();
                case 3: return FigureL();
                case 4: return FigureS();
                case 5: return FigureZ();
                case 6: return FigureT();
                case 7: return FigureI();
                default: return FigureSquare();
            }
        }

        public List<Point> FigureO()
        {
            return PointsFigure = new List<Point>()
            {
               new Point(StartingPointX, StartingPointY),
               new Point(StartingPointX+1, StartingPointY),
               new Point(StartingPointX, StartingPointY-1),
               new Point(StartingPointX+1, StartingPointY-1)
            };
        }

        public List<Point> FigureJ()
        {
            return PointsFigure = new List<Point>()
            {
                new Point(StartingPointX, StartingPointY),
             new Point(StartingPointX, StartingPointY+1),
             new Point(StartingPointX, StartingPointY-1),
             new Point(StartingPointX-1, StartingPointY+1)
            };
        }

        public List<Point> FigureL()
        {
            return PointsFigure = new List<Point>()
            {
               new Point(StartingPointX, StartingPointY),
               new Point(StartingPointX, StartingPointY+1),
               new Point(StartingPointX, StartingPointY-1),
               new Point(StartingPointX+1, StartingPointY+1)
            };
        }

        public List<Point> FigureS()
        {
            return PointsFigure = new List<Point>()
            {
                new Point(StartingPointX, StartingPointY),
                new Point(StartingPointX, StartingPointY-1),
                new Point(StartingPointX+1, StartingPointY-1),
                new Point(StartingPointX-1, StartingPointY)
            };
        }

        public List<Point> FigureZ()
        {
            return PointsFigure = new List<Point>()
            {
                new Point(StartingPointX, StartingPointY),
                new Point(StartingPointX+1, StartingPointY),
                new Point(StartingPointX, StartingPointY-1),
                new Point(StartingPointX-1, StartingPointY-1)
            };
        }

        public List<Point> FigureT()
        {
            return PointsFigure = new List<Point>()
            {
                new Point(StartingPointX, StartingPointY),
                new Point(StartingPointX, StartingPointY+1),
                new Point(StartingPointX-1, StartingPointY),
                new Point(StartingPointX+1, StartingPointY),
            };
        }

        public List<Point> FigureI()
        {
            return PointsFigure = new List<Point>()
            {
                new Point(StartingPointX, StartingPointY),
                new Point(StartingPointX-1, StartingPointY),
                new Point(StartingPointX+1, StartingPointY),
                new Point(StartingPointX+2, StartingPointY),
            };
        }

        public List<Point> FigureSquare()
        {
            return PointsFigure = new List<Point>()
            {
               new Point(StartingPointX, StartingPointY)
            };
        }
    }
}
