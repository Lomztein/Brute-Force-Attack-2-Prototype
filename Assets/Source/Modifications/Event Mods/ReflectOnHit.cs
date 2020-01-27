using Lomztein.BruteForceAttackSequel.Damage;
using Lomztein.BruteForceAttackSequel.Modifiables;
using Lomztein.BruteForceAttackSequel.Projectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Modifications.EventMods
{
    public class ReflectOnHit : WeaponEventMod<DamageEventInfo>
    {
        [ExposedProperty] public ModProperty RangeCoeffecient = new ModProperty (1f, ModProperty.StackBehaviour.Add);
        [ExposedProperty] public ModProperty DamageCoeffecient = new ModProperty (1f, ModProperty.StackBehaviour.Add);
        [ExposedProperty] public ModProperty MaxDeviation = new ModProperty (0f, ModProperty.StackBehaviour.Min); // TODO implement Avg stackbehaviour

        public override ModProperty[] Properties => new ModProperty[] { RangeCoeffecient, DamageCoeffecient, MaxDeviation };

        public override void Execute(object source, DamageEventInfo info)
        {
            DamageInfo damage = info.Info;
            Vector2 newDirection = Vector2.Reflect(damage.Projectile.Direction, damage.Normal);
            GameObject newProjectile = Object.Instantiate(damage.Projectile.gameObject, damage.Point, Quaternion.LookRotation (newDirection, Vector3.back));
            Projectile proj = newProjectile.GetComponent<Projectile>();
            AttachProjectileToSources(proj);
            proj.Fire(damage.Projectile.Weapon, newDirection, damage.Projectile.Layer, damage.Projectile.Target, damage.Amount * DamageCoeffecient.Get () * Coeffecient.Get (), damage.Projectile.Range * RangeCoeffecient.Get ());
        }
    }
}