using System;

namespace Chaigoo.Geometries
{
    public struct Line : IEquatable<Line>
    {
        private Point2 p1;
        private Point2 p2;

        public Point2 P1
        {
            get { return p1; }
            set { p1 = value; }
        }

        public Point2 P2
        {
            get { return p2; }
            set { p2 = value; }
        }

        public Line(Point2 p1, Point2 p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }

        public static bool IsPointOnLine(Point2 p1, Point2 p2, Point2 point)
        {
            return Vector.CrossProduct(p1 - point, point - p2) == 0;
        }

        public static bool IsPointOnLine(Line line, Point2 point)
        {
            return Line.IsPointOnLine(line.p1, line.p2, point);
        }

        public static bool IsPointAlmostOnLine(Point2 p1, Point2 p2, Point2 point)
        {
            var v1 = p1 - point;
            var v2 = point - p2;
            v1.Normalize();
            v2.Normalize();

            return Vector.CrossProduct(v1, v2) <= Constants.Epsilon;
        }

        public static bool IsPointAlmostOnLine(Line line, Point2 point)
        {
            return Line.IsPointAlmostOnLine(line.p1, line.p2, point);
        }

        public static bool Equals(Point2 p1, Point2 p2, Point2 p3, Point2 p4)
        {
            return IsPointOnLine(p1, p2, p3) && IsPointOnLine(p1, p2, p4);
        }

        public static bool Equals(Line line1, Line line2)
        {
            return Equals(line1.p1, line1.p2, line2.p1, line2.p2);
        }

        public bool Equals(Line line)
        {
            return Line.Equals(line, this);
        }

        public override bool Equals(object obj)
        {
            if (null == obj || !(obj is Line))
                return false;
            return Equals((Line)obj);
        }

        public override int GetHashCode()
        {
            //wrong 
            return p1.GetHashCode() ^ p2.GetHashCode();
        }

        public static bool IsParallel(Point2 p1, Point2 p2, Point2 p3, Point2 p4)
        {
            return Vector.CrossProduct(p1 - p2, p3 - p4) == 0;
        }

        public static bool IsParallel(Line line1, Line line2)
        {
            return Line.IsParallel(line1.p1, line1.p2, line2.p1, line2.p2);
        }

        public static bool IsAlmostParallel(Point2 p1, Point2 p2, Point2 p3, Point2 p4)
        {
            var v1 = p1 - p2;
            v1.Normalize();
            var v2 = p3 - p4;
            v2.Normalize();
            return Vector.CrossProduct(v1, v2) <= Constants.Epsilon;
        }

        public static bool IsAlmostParallel(Line line1, Line line2)
        {
            return Line.IsAlmostParallel(line1.p1, line1.p2, line2.p1, line2.p2);
        }

        public static bool IsOrthogonal(Point2 p1, Point2 p2, Point2 p3, Point2 p4)
        {
            return (p1 - p2) * (p3 - p4) == 0;
        }

        public static bool IsOrthogonal(Line line1, Line line2)
        {
            return Line.IsOrthogonal(line1.p1, line1.p2, line2.p1, line2.p2);
        }

        public static bool IsAlmostOrthogonal(Point2 p1, Point2 p2, Point2 p3, Point2 p4)
        {
            var v1 = p1 - p2;
            v1.Normalize();
            var v2 = p3 - p4;
            v2.Normalize();

            return v1 * v2 <= Constants.Epsilon;
        }

        public static bool IsAlmostOrthogonal(Line line1, Line line2)
        {
            return Line.IsAlmostOrthogonal(line1.p1, line1.p2, line2.p1, line2.p2);
        }

        public static Point2? GetCross(Point2 p1, Point2 p2, Point2 p3, Point2 p4)
        {
            if (Line.IsParallel(p1, p2, p3, p4) || Line.Equals(p1, p2, p3, p4))
                return null;


            double x = ((p1.X * p2.Y - p2.X * p1.Y) * (p3.X - p4.X) - (p3.X * p4.Y - p4.X * p3.Y) * (p1.X - p2.X)) /
                        ((p1.X - p2.X) * (p3.Y - p4.Y) - (p3.X - p4.X) * (p1.Y - p2.Y));
            double y = ((p1.X * p2.Y - p2.X * p1.Y) * (p3.Y - p4.Y) - (p3.X * p4.Y - p4.X * p3.Y) * (p1.Y - p2.Y)) /
                        ((p1.X - p2.X) * (p3.Y - p4.Y) - (p3.X - p4.X) * (p1.Y - p2.Y));

            return new Point2(x, y);
        }

        public void Offset(double x,double y)
        {
            p1.Offset(x, y);
            p2.Offset(x, y);
        }

        public void Offset(Vector vector)
        {
            this.Offset(vector.X, vector.Y);
        }

        public static Line Rotate(Line line,Point2 point,double angle)
        {
            var p1 = Point2.Rotate(line.p1, point, angle);
            var p2 = Point2.Rotate(line.p2, point, angle);
            return new Line(p1, p2);
        }

        public void Rotate(Point2 point,double angle)
        {
            p1.Rotate(point, angle);
            p2.Rotate(point, angle);
        }

        public void Rotate(double angle)
        {
            this.Rotate(Point2.Empty, angle);
        }

        public static Point2 Project(Point2 p1,Point2 p2,Point2 point)
        {
            if (p1 == p2)
                return p1;

            var v1 = point - p1;
            var v2 = p2 - p1;
            return p1 + v1 * v2 / v2.LengthSquared * v2;
        }

        public static Point2 Project(Line line,Point2 point)
        {
            return Line.Project(line.p1, line.p2, point);
        }

        public Point2 Project(Point2 point)
        {
            return Line.Project(this, point);
        }

        public static double DistanceBetween(Point2 p1,Point2 p2,Point2 point)
        {
            if (p1 == p2)
                return Point2.DistanceBetween(p1, point);

            var v1 = point - p1;
            var v2 = p2 - p1;

            return Math.Abs(Vector.CrossProduct(v1, v2) / v2.Length);
        }

        public static double DistanceBetween(Line line,Point2 point)
        {
            return Line.DistanceBetween(line.p1, line.p2, point);
        }

        public double DistanceBetween(Point2 point)
        {
            return Line.DistanceBetween(this, point);
        }


    }
}
