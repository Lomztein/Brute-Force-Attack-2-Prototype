using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Projectiles
{
    public class HomingProjectileMotor : MonoBehaviour
    {
        [SerializeField] private Projectile _projectile;

        private Transform _target;
        public Vector2 TargetPosition => (Vector2)_target?.position;

        public float MinTurnRate;
        public float MaxTurnRate;
        public float SlerpSpeed;

        private TargetFinder _targetFinder = new TargetFinder();

        private void Start()
        {
            _target = _projectile.Target;
        }

        private void FixedUpdate()
        {
            if (_target != null)
            {
                float angle = Mathf.Rad2Deg * Mathf.Atan2(TargetPosition.y - transform.position.y, TargetPosition.x - transform.position.x);
                float dot = Vector3.Dot(transform.right, (TargetPosition - (Vector2)transform.position).normalized);
                float rotSpeed = Mathf.Lerp (MaxTurnRate, MinTurnRate,  (dot + 1) / 2f);

                Quaternion rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler (0f, 0f, angle), rotSpeed * Time.fixedDeltaTime);
                _projectile.SetDirection(rotation * Vector3.right);
                _projectile.transform.rotation = rotation;
            }
            else
            {
                _target = _targetFinder.FindTarget(transform.position, _projectile.GetRange (), _projectile.Layer);
            }
        }
    }
}
