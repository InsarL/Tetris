using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetris
{
    static class FigureFactory
    {
        private static Random random = new Random();

        private static Figure FigureO()
        {
            return new Figure(FigureType.O, new Point(0, 0),    new Point(1, 0),  new Point(0, -1),  new Point(1, -1));
        }

        private static Figure FigureJ()
        {
            return new Figure(FigureType.J,  new Point(0, 0), new Point(0, 1), new Point(0, -1), new Point(-1, 1));
        }

        private static Figure FigureL()
        {
            return new Figure(FigureType.L, new Point(0, 0), new Point(0, 1),  new Point(0, -1),new Point(1, 1));
        }

        private static Figure FigureS()
        {
            return new Figure(FigureType.S, new Point(0, 0), new Point(0, -1),new Point(1, -1), new Point(-1, 0));
        }

        private static Figure FigureZ()
        {
            return new Figure(FigureType.Z, new Point(0, 0), new Point(1, 0),new Point(0, -1), new Point(-1, -1));
        }

        private static Figure FigureT()
        {
            return new Figure(FigureType.T, new Point(0, 0), new Point(0, 1), new Point(-1, 0), new Point(1, 0));
        }

        private static Figure FigureI()
        {
            return new Figure(FigureType.I, new Point(0, 0), new Point(-1, 0), new Point(1, 0), new Point(2, 0));
        }

        private static Figure FigureSquare()
        {
            return new Figure(FigureType.Square, new Point(0, 0));
        }

        public static Figure CreateRandomFigure()
        {
            switch (random.Next(1, 9))
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
