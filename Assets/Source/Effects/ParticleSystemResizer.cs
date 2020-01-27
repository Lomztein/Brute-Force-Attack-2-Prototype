using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Effects
{
    public static class ParticleSystemScaler
    {
        public static void Scale (ParticleSystem system, float scale)
        {
            system.transform.localScale = Vector3.one * scale;
            var main = system.main;

            main.startSizeMultiplier = scale;
            main.startSpeedMultiplier = scale;

            var emission = system.emission;

            emission.rateOverTimeMultiplier = scale;
            emission.rateOverDistanceMultiplier = scale;
            var bursts = new ParticleSystem.Burst[emission.burstCount];

            emission.GetBursts(bursts);

            for (int i = 0; i < bursts.Length; i++)
            {
                bursts[i].maxCount *= (short)(scale);
                bursts[i].minCount *= (short)(scale);
            }
        }
    }
}
