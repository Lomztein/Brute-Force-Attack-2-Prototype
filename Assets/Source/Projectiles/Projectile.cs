using Lomztein.BruteForceAttackSequel.Damage;
using Lomztein.BruteForceAttackSequel.Weapons;
using System;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        public float Damage { get; protected set; }
        public Vector3 Direction { get; protected set; }
        public float Range { get; protected set; }
        public Weapon Weapon { get; protected set; }
        public LayerMask Layer { get; protected set; }
        public int ProcLayer { get; private set; }
        public Transform Target { get; private set; }

        public event Action<DamageInfo> OnHit;
        public event Action<DamageInfo> OnFirstHit;
        public event Action<DamageInfo> OnPierce;
        public event Action<DamageInfo> OnLastHit;
        public event Action<DamageInfo> OnAOEHit;
        public event Action<DamageInfo> OnDOTHit;
        public event Action<Projectile> OnFinished;

        [SerializeField] private float _rangeMult = 1f;

        public void Fire(Weapon parentWeapon, Vector3 direction, LayerMask targetLayer, Transform target, float damage, float range)
        {
            Weapon = parentWeapon;
            Damage = damage;
            Direction = direction;
            Range = range;
            Layer = targetLayer;
            Target = target;
        }

        public void SetProcLayer (int value)
        {
            ProcLayer = value;
        }

        public void SetTarget (Transform newTarget)
        {
            Target = newTarget;
        }

        public abstract float GetSpeed();

        public float GetRange ()
        {
            return Range * _rangeMult;
        }

        public void AttachEvents (Action<DamageInfo> onHit, Action<DamageInfo> firstHit, Action<DamageInfo> pierce, Action<DamageInfo> lastHit, Action<DamageInfo> aoe, Action<DamageInfo> dot, Action<Projectile> onFinished)
        {
            OnHit += onHit;
            OnFirstHit += firstHit;
            OnPierce += pierce;
            OnLastHit += lastHit;
            OnAOEHit += aoe;
            OnDOTHit += dot;
            OnFinished += OnFinished;
        }

        protected void RaiseOnHit(DamageInfo info) => OnHit?.Invoke(info);
        protected void RaiseOnFirstHit(DamageInfo info) => OnFirstHit?.Invoke(info);
        protected void RaiseOnPierce(DamageInfo info) => OnPierce?.Invoke(info);
        protected void RaiseOnLastHit(DamageInfo info) => OnLastHit?.Invoke(info);
        protected void RaiseOnAOEHit(DamageInfo info) => OnAOEHit?.Invoke(info);
        protected void RaiseOnDOTHit(DamageInfo info) => OnDOTHit?.Invoke(info);
        protected void RaiseOnFinished() => OnFinished?.Invoke(this);

        public void SetDirection (Vector3 newDirection)
        {
            Direction = newDirection;
        }

        protected float Hit (Collider2D collider, DamageInfo damage)
        {
            if (CheckHit (collider))
            {
                return ApplyHit(collider, damage);
            }
            return 0;
        }

        private bool CheckHit (Collider2D collider)
        {
            int colMask = (int)Mathf.Pow(2, collider.gameObject.layer);
            return ((colMask & Layer.value) == colMask);
        }

        private float ApplyHit (Collider2D collider, DamageInfo damage)
        {
            return collider.GetComponent<IDamagable>().TakeDamage(damage);
        }

        protected void DestroyProjectile ()
        {
            Destroy(gameObject);
        }

        public void NeutralizeProjectile ()
        {
            Damage = 0f;
        }
    }
}
