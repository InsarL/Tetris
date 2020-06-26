﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetris
{
    class FigureFactory
    {
        public static int OriginX = 12;
        public static int OriginY = 4;
        private static Random random = new Random();

        private static Figure FigureO()
        {
            return new Figure(FigureType.O, new[]
            {
                new Point(OriginX, OriginY),
                new Point(OriginX+1, OriginY),
                new Point(OriginX, OriginY-1),
                new Point(OriginX+1, OriginY-1)
            });
        }

        private static Figure FigureJ()
        {
            return new Figure(FigureType.J, new[]
            {
                new Point(OriginX, OriginY),
                new Point(OriginX, OriginY+1),
                new Point(OriginX, OriginY-1),
                new Point(OriginX-1, OriginY+1)
            });
        }

        private static Figure FigureL()
        {
            return new Figure(FigureType.L, new[]
            {
                new Point(OriginX, OriginY),
                new Point(OriginX, OriginY+1),
                new Point(OriginX, OriginY-1),
                new Point(OriginX+1, OriginY+1)
            });
        }

        private static Figure FigureS()
        {
            return new Figure(FigureType.S, new[]
            {
                new Point(OriginX, OriginY),
                new Point(OriginX, OriginY-1),
                new Point(OriginX+1, OriginY-1),
                new Point(OriginX-1, OriginY)
            });
        }

        private static Figure FigureZ()
        {
            return new Figure(FigureType.Z, new[]
            {
                new Point(OriginX, OriginY),
                new Point(OriginX+1, OriginY),
                new Point(OriginX, OriginY-1),
                new Point(OriginX-1, OriginY-1)
            });
        }

        private static Figure FigureT()
        {
            return new Figure(FigureType.T, new[]
            {
                new Point(OriginX, OriginY),
                new Point(OriginX, OriginY+1),
                new Point(OriginX-1, OriginY),
                new Point(OriginX+1, OriginY)
            });
        }

        private static Figure FigureI()
        {
            return new Figure(FigureType.I, new[]
            {
                new Point(OriginX, OriginY),
                new Point(OriginX-1, OriginY),
                new Point(OriginX+1, OriginY),
                new Point(OriginX+2, OriginY)
            });
        }

        private static Figure FigureSquare()
        {
            return new Figure(FigureType.Square, new[] { new Point(OriginX, OriginY) });
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