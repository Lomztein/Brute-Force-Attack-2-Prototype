using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel
{
    class TargetFinder
    {
        public Func<Transform, Vector2, float, float> ScoreFunction;
        public Predicate<Transform> Filter;

        public TargetFinder ()
        {
            ScoreFunction = new Func<Transform, Vector2, float, float>((transform, center, range) => Vector2.SqrMagnitude (center - (Vector2)transform.position) * -1f);
            Filter = new Predicate<Transform>(x => true);
        }

        public TargetFinder (Func<Transform, Vector2, float, float> scoreFunction, Predicate<Transform> filter)
        {
            ScoreFunction = scoreFunction;
            Filter = filter;
        }

        /// <summary>
        /// Finds the Transform object with the highest score according to the TargetFinders sort function and filtered by its Filter predicate.
        /// </summary>
        /// <param name="center"></param>
        /// <param name="range"></param>
        /// <param name="targetLayer"></param>
        /// <returns></returns>
        public Transform FindTarget(Vector2 center, float range, LayerMask targetLayer)
        {
            Collider2D[] nearby = Physics2D.OverlapCircleAll(center, range, targetLayer).Where (x => Filter (x.transform)).ToArray ();

            if (nearby.Length == 0)
                return null;

            Collider2D best = nearby[0];
            float bestScore = float.MinValue;

            for (int i = 0; i < nearby.Length; i++)
            {
                Collider2D current = nearby[i];
                float score = ScoreFunction(current.transform, center, range);

                if (score > bestScore)
                {
                    best = current;
                    bestScore = score;
                }
            }

            return best.transform;
        }

    }
}
