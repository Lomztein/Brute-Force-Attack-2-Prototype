using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lomztein.BruteForceAttackSequel.Modifiables;

namespace Lomztein.BruteForceAttackSequel.Turrets.TargetFinders
{
    public class TurretBase : TurretComponent, ITargetProvider, IRanger
    {
        private Transform _currentTarget;
        private TargetFinder _targetFinder = new TargetFinder();

        public LayerMask TargetLayer;

        protected override void InitComponent()
        {
            Stats.GetStat(Stat.Type.Turret_Strength).SetNameAndDesc ("Complexity Capacity", "Increases how complex a turret this base can support.");
            Stats.GetStat(Stat.Type.Turret_Speed).SetNameAndDesc ("Range", "Increases range wherein targets will be found.");

            Events.AddEvent(Modifiables.Event.Type.OnTargetAquired);
            Events.AddEvent(Modifiables.Event.Type.OnTargetOutOfRange);
            Events.AddEvent(Modifiables.Event.Type.OnTargetLost);
        }

        protected override void Tick(float deltaTime)
        {
            if (_currentTarget == null)
            {
                _currentTarget = _targetFinder.FindTarget(transform.position, GetRange (), TargetLayer);
                if (_currentTarget != null)
                {
                    Events.GetEvent(Modifiables.Event.Type.OnTargetAquired).Raise(this, new TargetEventInfo(_currentTarget));
                }
            }else if (Vector2.SqrMagnitude (_currentTarget.position - transform.position) > Mathf.Pow (GetRange (), 2))
            {
                Events.GetEvent(Modifiables.Event.Type.OnTargetOutOfRange).Raise(this, new TargetEventInfo(_currentTarget));
                _currentTarget = null;
            }
        }

        public Transform GetTarget()
        {
            return _currentTarget;
        }

        public float GetRange ()
        {
            return Stats.GetStat(Stat.Type.Turret_Speed).GetValue();
        }

        public float GetComplexityThreshold ()
        {
            return Stats.GetStat(Stat.Type.Turret_Strength).GetValue();
        }
    }
}