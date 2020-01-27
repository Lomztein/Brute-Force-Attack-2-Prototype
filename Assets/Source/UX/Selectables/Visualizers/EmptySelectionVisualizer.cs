using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.UX.Selectables.Visualizers
{
    public class EmptySelectionVisualizer : ISelectionVisualizer
    {
        private GameObject _empty;
        public ISelectable Visualized { get; set; }

        public void Begin()
        {
        }

        public void BeginHover()
        {
        }

        public void End()
        {
        }

        public void EndHover()
        {
        }

        public GameObject GetGraphic()
        {
            if (_empty == null)
            {
                _empty = new GameObject("Empty Selection Visualizer");
            }
            return _empty;
        }

        public void Tick(float deltaTime)
        {
        }
    }
}
