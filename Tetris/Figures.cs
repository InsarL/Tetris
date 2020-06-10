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
        public int OriginX = 12;
        public int OriginY = 3;
        Random random = new Random();
        
        private List<Point> FigureO()
        {
            return new List<Point>() {new Point(OriginX, OriginY), new Point(OriginX+1, OriginY),
                                      new Point(OriginX, OriginY-1), new Point(OriginX+1, OriginY-1)};
        }

        private List<Point> FigureJ()
        {
            return new List<Point>() {new Point(OriginX, OriginY), new Point(OriginX, OriginY+1),
                                      new Point(OriginX, OriginY-1), new Point(OriginX-1, OriginY+1)};
        }

        private List<Point> FigureL()
        {
            return  new List<Point>  {new Point(OriginX, OriginY), new Point(OriginX, OriginY+1),
                                      new Point(OriginX, OriginY-1), new Point(OriginX+1, OriginY+1)};
        }
        private List<Point> FigureS()
        {
            return  new List<Point>() {new Point(OriginX, OriginY), new Point(OriginX, OriginY-1),
                                       new Point(OriginX+1, OriginY-1), new Point(OriginX-1, OriginY)};
        }

        private List<Point> FigureZ()
        {
            return  new List<Point>() {new Point(OriginX, OriginY), new Point(OriginX+1, OriginY),
                                       new Point(OriginX, OriginY-1), new Point(OriginX-1, OriginY-1)};
        }

        private List<Point> FigureT()
        {
           return  new List<Point>() {new Point(OriginX, OriginY), new Point(OriginX, OriginY+1),
                                                         new Point(OriginX-1, OriginY), new Point(OriginX+1, OriginY)};
        }

        private List<Point> FigureI()
        {
            return new List<Point>() {new Point(OriginX, OriginY), new Point(OriginX-1, OriginY),
                                                         new Point(OriginX+1, OriginY), new Point(OriginX+2, OriginY)};
        }

        private List<Point> FigureSquare()
        {
            return  new List<Point>() { new Point(OriginX, OriginY) };
        }

        public List<Point> RandomizationFigures()
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
                default: return FigureSquare();
            }
        }

        public void FiguresRotate()
        {


        }
    }
}
