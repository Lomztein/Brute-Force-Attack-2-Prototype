using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Turrets.TargetFinders
{
    public interface ITargetProvider
    {
        Transform GetTarget();
    }

}
