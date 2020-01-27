using Lomztein.BruteForceAttackSequel.Modifiables;
using Lomztein.BruteForceAttackSequel.Turrets.TargetFinders;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Turrets.Targeters
{
    public class TurretRotator : TurretComponent, ITargeter
    {
        public float PredictionSpeed { get; set; } = float.PositiveInfinity;

        private Vector2 _targetPos;
        private Vector2 _prevPos;
        private Vector2 _predictedPos;

        private ITargetProvider _targetProvider;

        public float AngularTargetDistance()
        {
            float delta = 180f;
            if (_targetProvider.GetTarget () != null)
            {
                delta = Mathf.Abs(Mathf.DeltaAngle(transform.rotation.eulerAngles.z, AngleToPos(_predictedPos)));
            }
            return delta;
        }

        public Transform GetTarget()
        {
            if (_targetProvider != null)
            {
                return _targetProvider.GetTarget();
            }
            else return null;
        }

        protected override void InitComponent()
        {
            Stats.GetStat(Stat.Type.Turret_Speed).SetNameAndDesc ("Speed", "The speed of which this rotator rotates.");
            Stats.GetStat(Stat.Type.Turret_Strength).SetNameAndDesc("Carry Capacity", "The amount of weight this rotator is capable of carrying.");

            _targetProvider = transform.parent.GetComponentInParent<ITargetProvider>();
        }

        protected override void Tick(float deltaTime)
        {
            CalculatePositions(deltaTime);
            float angle = AngleToPos (_predictedPos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, angle), GetRotatorSpeed() * deltaTime);
        }

        private void CalculatePositions (float deltaTime)
        {
            _targetPos = transform.position + transform.right;
            Transform target = _targetProvider?.GetTarget();

            if (target)
            {
                _targetPos = target.position;
            }

            if (float.IsInfinity (PredictionSpeed))
            {
                _predictedPos = _targetPos;
            }else
            {
                float time = Vector2.Distance(transform.position, _targetPos) / PredictionSpeed;
                _predictedPos = _targetPos + (_prevPos - _targetPos) * time * deltaTime;
            }

            _prevPos = _targetPos;
        }

        private float AngleToPos (Vector2 position)
        {
            float angle = Mathf.Rad2Deg * Mathf.Atan2(position.y - transform.position.y, position.x - transform.position.x);
            return angle;
        }

        public float GetRotatorSpeed ()
        {
            return Stats.GetStat(Stat.Type.Turret_Speed).GetValue();
        }

        public float GetWeightCapacity ()
        {
            return Stats.GetStat(Stat.Type.Turret_Strength).GetValue();
        }

    }
}
