using System.Collections;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Projectiles.HitscanRenderers
{
    public class BeamHitscanRenderer : HitscanRenderer
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private float _shrinkTime;

        public override void SetPositions(Vector3 start, Vector3 end)
        {
            _lineRenderer.SetPosition(0, start);
            _lineRenderer.SetPosition(1, end);
        }

        private void Start()
        {
            StartCoroutine(ShrinkBeam ());
        }

        IEnumerator ShrinkBeam ()
        {
            float width = _lineRenderer.startWidth;
            float shrinkSpeed = width / _shrinkTime;

            for (int i = 0; i < 1 / Time.fixedDeltaTime * _shrinkTime; i++)
            {
                width -= shrinkSpeed * Time.fixedDeltaTime;
                _lineRenderer.startWidth = width;
                _lineRenderer.endWidth = width;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
