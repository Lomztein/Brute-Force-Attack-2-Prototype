using System;
using System.Collections;
using System.Collections.Generic;

namespace Lomztein.BruteForceAttackSequel.FluidField
{
    public class VectorField2D
    {
        private Vector2D[,] _vectorField;

        public int Width => _vectorField.GetLength (0);
        public int Height => _vectorField.GetLength(1);

        public VectorField2D (int width, int height)
        {
            _vectorField = new Vector2D[width, height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    _vectorField[x, y] = new Vector2D(0,0);
                }
            }
        }

        public (float x, float y) Get (float x, float y)
        {
            Vector2D pos = new Vector2D(x, y);

            Vector2D upperLeft  = new Vector2D((float)Math.Ceiling(x), (float)Math.Ceiling(y));
            Vector2D upperRight = new Vector2D((float)Math.Floor(x), (float)Math.Ceiling(y));
            Vector2D lowerLeft  = new Vector2D((float)Math.Ceiling(x), (float)Math.Floor(y));
            Vector2D lowerRight = new Vector2D((float)Math.Floor(x), (float)Math.Floor(y));

            Vector2D interpolatedVelocity =
                (_vectorField[(int)upperLeft.X,     (int)upperLeft.Y] * Vector2D.SqrDist(pos, upperLeft)) +
                (_vectorField[(int)upperRight.X,    (int)upperRight.Y] * Vector2D.SqrDist(pos, upperRight)) +
                (_vectorField[(int)lowerLeft.X,     (int)lowerLeft.Y] * Vector2D.SqrDist(pos, lowerLeft)) +
                (_vectorField[(int)lowerRight.X,    (int)lowerRight.Y] * Vector2D.SqrDist(pos, lowerRight));

            return (interpolatedVelocity.X, interpolatedVelocity.Y);
        }

        public void AddVelocity (int x, int y, float vx, float vy)
        {
            GetVector(x, y).X += vx;
            GetVector(x, y).Y += vy;
        }

        protected Vector2D GetVector (int x, int y)
        {
            return _vectorField[x, y];
        }

        public bool WithinBounds (float x, float y)
        {
            if (x > Width - 1 || x < 0)
                return false;
            if (y > Height - 1 || y < 0)
                return false;
            return true;
        }

        protected class Vector2D
        {
            public float X;
            public float Y;

            public Vector2D (float x, float y)
            {
                X = x;
                Y = y;
            }

            public static float SqrDist (Vector2D from, Vector2D to)
            {
                return (to.X - from.X) + (to.Y - from.Y);
            }

            public static Vector2D operator + (Vector2D v1, Vector2D v2)
            {
                return new Vector2D(v1.X + v2.X, v1.Y + v2.Y);
            }

            public static Vector2D operator * (Vector2D vector, float scalar)
            {
                return new Vector2D(vector.X * scalar, vector.Y * scalar);
            }
        }
    }
}