using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.FluidField
{
    /// <summary>
    /// This fluid simulator is based on https://www.mikeash.com/pyblog/fluid-simulation-for-dummies.html
    /// Also I had Codetrain essentially read it to me because I'm dumb and lazy :D https://youtu.be/alhpH6ECFvQ
    /// </summary>
    public class FluidSimulator
    {
        public int Width;
        public int Height;
        public float TileSize;

        public float Viscosity = 1;
        public float Diffusion = 1;
        public int Iterations = 10;

        private VectorField2D _vectorField;
        private float[,] _density;
        private bool[,] _isSolid;

        public void ResetFluid ()
        {
            _vectorField = new VectorField2D(Width, Height);
            _density = new float[Width, Height];
            _isSolid = new bool[Width, Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {

                }
            }
        }

        public void AddDensity (int x, int y, float amount)
        {
            _density[x, y] += amount;
        }

        public void AddVelocity (int x, int y, float vx, float vy)
        {
            _vectorField.AddVelocity(x, y, vx, vy);
        }

        public (float x, float y) Get (float x, float y)
        {
            return _vectorField.Get(x, y);
        }

        public void Step (float deltaTime)
        {
        }

        private void Diffuse (float deltaTime)
        {
            float a = deltaTime * Diffusion * (Width - 2) * (Height - 2);
            LiniarSolve();
        }

        private void Project ()
        {

        }

        private void Advect ()
        {

        }

        private void SetBounds ()
        {

        }

        private void LiniarSolve ()
        {
            for (int i = 0; i < Iterations; i++)
            {
                for (int x = 0; x < _vectorField.Width; i++)
                {
                    for (int y = 0; y < _vectorField.Height; y++)
                    {

                    }
                }
            }
        }
    }
}
