using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Color.Colorizers
{
    public class RendererColorizer : MonoBehaviour, IColorizer
    {
        public Renderer Renderer;
        private Material _sharedMaterial;

        public void SetRenderer (Renderer renderer)
        {
            Renderer = renderer;
        }

        private Material GetMaterial ()
        {
            if (_sharedMaterial == null)
                _sharedMaterial = Instantiate(Renderer.material);
            return _sharedMaterial;
        }

        public void SetColor(UnityEngine.Color color)
        {
            if (GetMaterial().HasProperty("_EmissionColor"))
            {
                UnityEngine.Color curEmission = GetMaterial().GetColor("_EmissionColor");
                float intensity = curEmission.grayscale;
                GetMaterial().SetColor("_EmissionColor", color * intensity);
            }

            GetMaterial().color = color;
            Renderer.material = GetMaterial();
        }
    }
}
