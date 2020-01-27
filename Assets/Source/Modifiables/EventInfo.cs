using Lomztein.BruteForceAttackSequel.Damage;
using Lomztein.BruteForceAttackSequel.Projectiles;
using Lomztein.BruteForceAttackSequel.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Modifiables
{
    public abstract class EventInfo { }

    public class DamageEventInfo : EventInfo //TODO: Fill this out once damage has been implemented.
    {
        public DamageEventInfo (DamageInfo info)
        {
            Info = info;
        }
        public readonly DamageInfo Info;
    }

    public class FireEventInfo : EventInfo
    {
        public FireEventInfo (Transform muzzle, Weapon weapon)
        {
            Muzzle = muzzle;
            Weapon = weapon;
        }

        public readonly Transform Muzzle;
        public readonly Weapon Weapon;
    }

    public class ProjectileEventInfo : EventInfo
    {
        public ProjectileEventInfo (Projectile _projectile)
        {
            Projectile = _projectile;
        }

        public readonly Projectile Projectile;
    }

    public class TargetEventInfo : EventInfo
    {
        public readonly Transform Target;

        public TargetEventInfo (Transform target)
        {
            Target = target;
        }
    }
}
