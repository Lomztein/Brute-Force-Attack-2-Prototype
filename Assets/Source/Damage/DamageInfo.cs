using Lomztein.BruteForceAttackSequel.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Damage
{
    public class DamageInfo
    {
        public readonly float Amount;
        public readonly Vector2 Point;
        public readonly Vector2 Normal;
        public readonly GameObject Target;
        public readonly Projectile Projectile;

        public DamageInfo (float amount, Vector2 point, Vector2 normal, GameObject target, Projectile projectile)
        {
            Amount = amount;
            Point = point;
            Normal = normal;
            Target = target;
            Projectile = projectile;
        }
    }
}
