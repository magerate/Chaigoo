using System;


namespace Chaigoo.Geometries
{
    public struct Ray:IEquatable<Ray>
    {
        private Point2 point;
        private Vector direction;

        public Point2 Point
        {
            get { return point; }
            set { point = value; }
        }

        public Vector Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public Ray(Point2 point,Vector direction)
        {
            this.point = point;
            this.direction = direction;
        }

        public bool Equals(Ray ray)
        {
            return point == ray.point && direction.Angle == ray.direction.Angle;
        }

        public override bool Equals(object obj)
        {
            if (null == obj || !(obj is Ray))
                return false;
            return Equals((Ray)obj);
        }

        public override int GetHashCode()
        {
            return point.GetHashCode() ^ direction.Angle.GetHashCode();
        }

        public static bool IsPointOnRay(Point2 p1,Point2 p2,Point2 point)
        {
            return Line.IsPointOnLine(p1, p2, point) && Contains(p1.X, p2.X, point.X);
        }

        public static bool Contains(double lower,double upper,double value)
        {
            if (lower == upper)
                return lower == value;

            if (lower < upper)
                return value >= lower;
            else
                return value <= lower;
        }
    }
}
