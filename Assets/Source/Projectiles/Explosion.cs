using Lomztein.BruteForceAttackSequel.Damage;
using Lomztein.BruteForceAttackSequel.Effects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace Lomztein.BruteForceAttackSequel.Projectiles
{
    public class Explosion : Projectile
    {
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private float _sizeCoeffecient;
        [SerializeField] private float _lifeTime;

        private void Start()
        {
            SetParticleSize();
            Explode();
            Invoke("DestroyProjectile", _lifeTime);
        }

        private void SetParticleSize ()
        {
            ParticleSystemScaler.Scale(_particle, Range * _sizeCoeffecient);
        }

        private void Explode ()
        {
            Collider2D[] nearby = Physics2D.OverlapCircleAll(transform.position, Range, Layer);
            foreach (Collider2D col in nearby) {
                DamageInfo info = new DamageInfo(Damage, col.transform.position, col.transform.position - transform.position, col.gameObject, this);
                RaiseOnAOEHit (info);
                Hit(col, info);
            }
        }

        public override float GetSpeed()
        {
            return float.PositiveInfinity;
        }
    }
}
