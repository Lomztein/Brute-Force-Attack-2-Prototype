using Lomztein.BruteForceAttackSequel.Damage;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Projectiles
{
    public class PhysicalProjectile : Projectile
    {
        public float Speed;
        private bool _firstHit = true;

        private void Awake()
        {
            Speed *= Random.Range(0.9f, 1.1f);
        }

        private void Start ()
        {
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Rad2Deg * Mathf.Atan2(Direction.y, Direction.x));
            Invoke("DestroyProjectile", GetRange () / Speed);
        }

        private void FixedUpdate()
        {
            Step(Time.fixedDeltaTime);
        }

        private void Step (float deltaTime)
        {
            Vector3 forward = Direction * Speed;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, forward * deltaTime, Speed * deltaTime, Layer);

            if (hit.collider != null)
            {
                DamageInfo damage = new DamageInfo(Damage, hit.point, hit.normal, hit.collider.gameObject, this);

                Damage -= Hit(hit.collider, damage);
                if (_firstHit) {
                    RaiseOnHit(damage);
                    RaiseOnFirstHit(damage);
                    _firstHit = false;
                }
                if (Damage > 0f)
                {
                    RaiseOnHit(damage);
                    RaiseOnPierce(damage);
                } else
                {
                    RaiseOnHit(damage);
                    RaiseOnLastHit(damage);
                }
            }
            if (Damage <= 0)
            {
                RaiseOnFinished();
                DestroyProjectile();
            }

            transform.position += forward * deltaTime;
        }

        public override float GetSpeed()
        {
            return Speed;
        }
    }
}
