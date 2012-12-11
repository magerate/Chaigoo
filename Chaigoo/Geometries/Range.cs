using System;


namespace Chaigoo.Geometries
{
    public struct Range
    {
        private double min;
        private double max;


        public Range(double x, double y)
        {
            if (x > y)
            {
                min = y;
                max = x;
            }
            else
            {
                min = x;
                max = y;
            }
        }

        public bool Contains(double value)
        {
            return Range.Contains(min, max, value);
        }

        public static bool Contains(double x, double y, double value)
        {
            return value >= Math.Min(x, y) && value <= Math.Max(x, y);
        }
    }
}
