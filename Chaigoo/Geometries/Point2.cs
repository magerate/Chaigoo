namespace Chaigoo.Geometries
{
    using System;

    public struct Point2:IEquatable<Point2>
    {
        public static readonly Point2 Empty = new Point2(0, 0);

        public double X, Y;

        public Point2(double x,double y)
        {
            this.X = x;
            this.Y = y;
        }

        public bool Equals(Point2 p)
        {
            return X == p.X && Y == p.Y;
        }

        public override bool Equals(object obj)
        {
            if (null == obj || !(obj is Point2))
                return false;
            return this.Equals((Point2)obj);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", X.ToString(), Y.ToString());
        }

        public static bool operator ==(Point2 p1,Point2 p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Point2 p1,Point2 p2)
        {
            return !p1.Equals(p2);
        }

        public static Vector operator -(Point2 p1,Point2 p2)
        {
            return new Vector(p1.X - p2.X, p1.Y - p2.Y);
        }

        public static Point2 operator -(Point2 p,Vector v)
        {
            return new Point2(p.X - v.X, p.Y - v.Y);
        }

        public static Point2 operator +(Point2 p,Vector v)
        {
            return new Point2(p.X + v.X, p.Y + v.Y);
        }

        public void Offset(double x,double y)
        {
            this.X += x;
            this.Y += y;
        }

        public void Offset(Vector vector)
        {
            this.Offset(vector.X, vector.Y);
        }

        public static Point2 Flip(Point2 point, Point2 origin)
        {
            return origin + (origin - point);
        }

        public void Flip(Point2 p)
        {
            this = Point2.Flip(this, p);
        }
       
        public static Point2 Rotate(Point2 point,Point2 origin,double angle)
        {
            var v = point - origin;
            v.Rotate(angle);
            return origin + v;
        }

        public void Rotate(Point2 origin,double angle)
        {
            this = Point2.Rotate(this, origin, angle);
        }

        public void Rotate(double angle)
        {
            this = Point2.Rotate(this, Point2.Empty, angle);
        }

        public static double DistanceBetween(Point2 p1,Point2 p2)
        {
            return (p1 - p2).Length;
        }

        public double DistanceBetween(Point2 point)
        {
            return Point2.DistanceBetween(this, point);
        }

        public static double DistanceSquared(Point2 p1,Point2 p2)
        {
            return (p1 - p2).LengthSquared;
        }

        public double DistanceSquared(Point2 point)
        {
            return DistanceSquared(this, point);
        }
    }
}
