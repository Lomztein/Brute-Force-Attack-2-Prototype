using Lomztein.BruteForceAttackSequel.Damage;
using Lomztein.BruteForceAttackSequel.Modifiables;
using Lomztein.BruteForceAttackSequel.Projectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Modifications.EventMods
{
    public class SplitOnHit : WeaponEventMod<DamageEventInfo>
    {
        [ExposedProperty] public ModProperty StartAngle = new ModProperty (45, ModProperty.StackBehaviour.Max);
        [ExposedProperty] public ModProperty EndAngle = new ModProperty (-45, ModProperty.StackBehaviour.Min);
        [ExposedProperty] public ModProperty DamageCoeffecient = new ModProperty(1f, ModProperty.StackBehaviour.Max);
        [ExposedProperty] public ModProperty RangeCoeffecient = new ModProperty (0.5f, ModProperty.StackBehaviour.Max);

        [ExposedProperty] public ModProperty BaseProjectileCount = new ModProperty (2, ModProperty.StackBehaviour.Add);

        [ExposedProperty] public bool IncludeLast;
        [ExposedProperty] public bool HalfOffset;

        public override ModProperty[] Properties => new ModProperty[] { Coeffecient, StartAngle, EndAngle, DamageCoeffecient, RangeCoeffecient, BaseProjectileCount };

        public override void Execute(object source, DamageEventInfo info)
        {
            DamageInfo damage = info.Info;

            int projCount = Mathf.RoundToInt(BaseProjectileCount.Get () * Coeffecient.Get ());
            float step = (StartAngle.Get () - EndAngle.Get ()) / (projCount - (IncludeLast ? 1 : 0));

            for (int i = 0; i < projCount; i++)
            {
                float angle = StartAngle.Get () - step * i + (HalfOffset ? step / 2f : 0f);
                Vector3 dir = Quaternion.Euler(0f, 0f, angle) * damage.Projectile.Direction;
                GameObject newProjectile = Object.Instantiate(damage.Projectile.gameObject, damage.Point, Quaternion.identity);
                Projectile projectile = newProjectile.GetComponent<Projectile>();
                AttachProjectileToSources (projectile);
                projectile.SetProcLayer(damage.Projectile.ProcLayer + 1);
                projectile.Fire(damage.Projectile.Weapon, dir, damage.Projectile.Layer, damage.Projectile.Target, damage.Amount * DamageCoeffecient.Get (), damage.Projectile.Range * RangeCoeffecient.Get ());
            }
        }
    }
}
