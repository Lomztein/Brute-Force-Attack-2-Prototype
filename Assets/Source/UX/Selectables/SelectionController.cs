using Lomztein.BruteForceAttackSequel.UX.Selectables.Handlers;
using Lomztein.BruteForceAttackSequel.UX.Selectables.Visualizers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.UX.Selectables
{
    public class SelectionController : MonoBehaviour
    {
        public List<ISelectable> Selections = new List<ISelectable>();
        public List<ISelectionHandler> Handlers = new List<ISelectionHandler>();
        public List<ISelectionVisualizer> Visualizers = new List<ISelectionVisualizer>();

        //TODO: Split ISelectionVisualizer into two interfaces: ISelectionVisualizer and IHoverVisualizer. Same classes can still inherit.
        private ISelectable _currentHover;
        private ISelectionVisualizer _hoveringVisualizer;

        public LayerMask SelectableLayers;

        private void Update()
        {
            for (int i = 0; i < Selections.Count; i++)
            {
                Tick(Handlers[i], Visualizers[i], Time.deltaTime);
            }

            Vector3 mousePosition = World.MousePosition;
            Collider2D[] colliders = Physics2D.OverlapPointAll(mousePosition, SelectableLayers);
            ISelectable selectable = colliders.Select(x => x.GetComponent<ISelectable>()).Where (x => x != null).FirstOrDefault ();
            if (selectable != _currentHover)
            {
                UpdateHover(selectable);
            }

            if (Input.GetMouseButtonDown (0))
            {
                if (!Input.GetKeyDown (KeyCode.LeftShift))
                {
                    Clear();
                }

                if (selectable != null)
                {
                    Select(selectable);
                }
            }

            if (Input.GetMouseButton (1))
            {
                Clear();
            }
        }

        private void UpdateHover(ISelectable selectable)
        {
            if (_hoveringVisualizer != null)
            {
                _hoveringVisualizer.EndHover();
                _hoveringVisualizer.End();
                _hoveringVisualizer = null;
            }

            _currentHover = selectable;

            if (selectable != null)
            {
                _hoveringVisualizer = _currentHover.GetSelectionVisualizer();
                _hoveringVisualizer.Visualized = selectable;
                _hoveringVisualizer.BeginHover();
                _hoveringVisualizer.Begin();
            }
        }

        private void Select(ISelectable selectable)
        {
            if (!Selections.Contains(selectable))
            {
                ISelectionHandler handler = selectable.GetSelectionHandler();
                ISelectionVisualizer visualizer = selectable.GetSelectionVisualizer();

                handler.Selectable = selectable;
                visualizer.Visualized = selectable;

                handler.Controller = this;

                Selections.Add(selectable);
                Handlers.Add(handler);
                Visualizers.Add(visualizer);

                Begin(handler, visualizer);
            }
        }

        public void Clear()
        {
            for (int i = 0; i < Handlers.Count; i++)
            {
                End(Handlers[i], Visualizers[i]);
            }

            Selections = new List<ISelectable>();
            Handlers = new List<ISelectionHandler>();
            Visualizers = new List<ISelectionVisualizer>();
        }

        private void Begin (ISelectionHandler handler, ISelectionVisualizer visualizer)
        {
            handler.Begin();
            visualizer.Begin();
        }

        private void End (ISelectionHandler handler, ISelectionVisualizer visualizer)
        {
            handler.End();
            visualizer.End();
        }

        void Tick (ISelectionHandler handler, ISelectionVisualizer visualizer, float deltaTime)
        {
            handler.Tick(deltaTime);
            visualizer.Tick(deltaTime);
            _hoveringVisualizer?.Tick(deltaTime);
        }
    }
}
