using System;

namespace Chaigoo.Geometries
{
    public struct Vector : IEquatable<Vector>
    {
        public static readonly Vector Zero = new Vector(0, 0);
        public double X, Y;

        public Vector(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public double LengthSquared
        {
            get { return X * X + Y * Y; }
        }

        public double Length
        {
            get { return Math.Sqrt(X * X + Y * Y); }
        }

        //[0,2pi)
        public double Angle
        {
            get
            {
                if (Zero == this)
                    return 0;
                if (X == 0)
                    return Y > 0 ? Math.PI / 2 : 3 * Math.PI / 2;

                if (X > 0 && Y >= 0)
                    return Math.Atan2(Y, X);
                else if (X > 0 && Y <= 0)
                    return Math.PI * 2 + Math.Atan2(Y, X);
                else
                    return Math.PI + Math.Atan2(Y, X);
            }
        }

        public void Negate()
        {
            X = -X;
            Y = -Y;
        }

        public static Vector Cross(Vector left, Vector right)
        {
            left.X = left.X * right.X;
            left.Y = left.Y * right.Y;
            return left;
        }

        public void Normalize()
        {
            this /= Length;
        }

        public bool Equals(Vector v)
        {
            return X == v.X && Y == v.Y;
        }

        public override bool Equals(object obj)
        {
            if (null == obj || !(obj is Vector))
                return false;
            return this.Equals((Vector)obj);
        }

        public static bool operator ==(Vector v1, Vector v2)
        {
            return v1.Equals(v2);
        }

        public static bool operator !=(Vector v1, Vector v2)
        {
            return !v1.Equals(v2);
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            v1.X += v2.X;
            v1.Y += v2.Y;
            return v1;
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            v1.X -= v2.X;
            v1.Y -= v2.Y;
            return v1;
        }

        public static Vector operator -(Vector v)
        {
            v.X = -v.X;
            v.Y = -v.Y;
            return v;
        }

        public static Vector operator *(Vector v, double scalar)
        {
            v.X *= scalar;
            v.Y *= scalar;
            return v;
        }

        public static Vector operator *(double scalar, Vector v)
        {
            v.X *= scalar;
            v.Y *= scalar;
            return v;
        }

        public static double operator *(Vector v1, Vector v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y;
        }

        public static Vector operator /(Vector v, double scalar)
        {
            v.X /= scalar;
            v.Y /= scalar;
            return v;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", X.ToString(), Y.ToString());
        }
    }
}
