using Lomztein.BruteForceAttackSequel.Turrets.TargetFinders;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Turrets.Targeters
{
    public interface ITargeter : ITargetProvider
    {
        float PredictionSpeed { get; set; }

        float AngularTargetDistance();
    }
}