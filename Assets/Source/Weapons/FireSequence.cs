using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Weapons
{
    public class FireSequence : FireControl
    {
        public int SequenceAmount;
        public float FirerateFraction;

        public override void Fire(float firerate)
        {
            StartCoroutine(SequencedFire(firerate));
        }

        private IEnumerator SequencedFire(float firerate)
        {
            int index = 0;

            for (int i = 0; i < SequenceAmount; i++)
            {
                FireCallback(index);
                index++;

                yield return new WaitForSeconds(firerate * FirerateFraction);
            }
        }
    }
}
