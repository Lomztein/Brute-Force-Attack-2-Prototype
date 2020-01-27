using Lomztein.BruteForceAttackSequel.Damage;
using Lomztein.BruteForceAttackSequel.Projectiles.HitscanRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Projectiles
{
    public class HitscanProjectile : Projectile
    {
        public float DestroyTime;
        [SerializeField] private HitscanRenderer _renderer;

        private void Start()
        {
            Hitscan();
            Invoke("DestroyProjectile", DestroyTime);
        }

        private void Hitscan ()
        {
            RaycastHit2D[] allHits = Physics2D.RaycastAll(transform.position, Direction, GetRange (), Layer, 0, 0);

            bool firstHit = true;
            foreach (RaycastHit2D hit in allHits)
            {
                DamageInfo info = new DamageInfo(Damage, hit.point, hit.normal, hit.collider.gameObject, this);
                if (Damage > 0)
                {
                    Damage -= Hit(hit.collider, info);

                    if (firstHit)
                    {
                        RaiseOnHit(info);
                        RaiseOnFirstHit(info);
                        firstHit = false;
                    }
                    if (Damage > 0)
                    {
                        RaiseOnHit(info);
                        RaiseOnPierce(info);
                    }
                    else
                    {
                        RaiseOnHit(info);
                        RaiseOnLastHit(info);
                        _renderer.SetPositions(transform.position, hit.point);
                        break;
                    }
                }
            }

            if (Damage > 0)
            {
                Vector2 end = transform.position + Direction * Range;
                _renderer.SetPositions(transform.position, transform.position + Direction * Range);
                RaiseOnFinished();
            }
        }

        public override float GetSpeed()
        {
            return float.PositiveInfinity;
        }
    }
}
