using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.UX.Selectables.Visualizers
{
    public class TargetableSelectionVisualizer : ISelectionVisualizer
    {
        public ISelectable Visualized { get; set; }
        public ITargetableSelectable VisualizedTargetable => Visualized as ITargetableSelectable;

        private GameObject _gameObject => (Visualized as Component).gameObject;
        private Resource<GameObject> _graphic = new Resource<GameObject>("Prefabs/UI/TargetableSelectionVisualizer");

        private GameObject _graphicObject;
        private LineRenderer _lineRenderer;

        private bool _active;

        public void Begin()
        {
            _active = true;
            _graphicObject = Object.Instantiate(_graphic.Get());
            _lineRenderer = _graphicObject.GetComponentInChildren<LineRenderer>();
            _graphicObject.transform.localScale *= _gameObject.GetComponent<Collider2D>().bounds.size.magnitude;
            _graphicObject.transform.position = _gameObject.transform.position;
        }

        public void End()
        {
            _active = false;
            Object.Destroy(_graphicObject);
        }

        public void Tick(float deltaTime)
        {
            _graphicObject.transform.position = _gameObject.transform.position;

            if (VisualizedTargetable.Target && _active)
            {
                _lineRenderer.enabled = true;
                _lineRenderer.SetPosition(0, _gameObject.transform.position);
                _lineRenderer.SetPosition(1, VisualizedTargetable.Target.transform.position);
            }
            else
            {
                _lineRenderer.enabled = false;
            }
        }

        public void BeginHover()
        {
        }

        public void EndHover()
        {
        }
    }
}