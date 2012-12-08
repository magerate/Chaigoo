using System;


namespace Chaigoo.Geometries
{
    public struct Vector:IEquatable<Vector>
    {
        public static readonly Vector Zero = new Vector(.0f,.0f);
        public float X, Y;

        public Vector(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public void 
    }
}
