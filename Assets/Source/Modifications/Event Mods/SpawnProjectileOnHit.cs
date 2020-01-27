using Lomztein.BruteForceAttackSequel.Damage;
using Lomztein.BruteForceAttackSequel.Modifiables;
using Lomztein.BruteForceAttackSequel.Projectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Modifications.EventMods
{
    public class SpawnProjectileOnHit : WeaponEventMod<DamageEventInfo>
    {
        [ExposedProperty] public Resource ProjectilePrefab;
        [ExposedProperty] public ModProperty DamageCoeffecient = new ModProperty(1f, ModProperty.StackBehaviour.Add);
        [ExposedProperty] public ModProperty AreaCoeffecient = new ModProperty(0.1f, ModProperty.StackBehaviour.Add);
        [ExposedProperty] public bool IncrementProcLayer;

        public override ModProperty[] Properties => new ModProperty[] { Coeffecient, DamageCoeffecient, AreaCoeffecient };

        public override void Execute(object source, DamageEventInfo info)
        {
            DamageInfo damage = info.Info;
            GameObject newExplosion = Object.Instantiate(ProjectilePrefab.Get () as GameObject, info.Info.Point, Quaternion.identity);
            Projectile proj = newExplosion.GetComponent<Projectile>();
            proj.Fire(damage.Projectile.Weapon, damage.Projectile.Direction, damage.Projectile.Layer, damage.Projectile.Target, damage.Amount * DamageCoeffecient.Get (), Mathf.Sqrt(damage.Amount * AreaCoeffecient.Get () / Mathf.PI) * Coeffecient.Get ());
            AttachProjectileToSources(proj);

            if (IncrementProcLayer)
            {
                proj.SetProcLayer(damage.Projectile.ProcLayer + 1);
            }
        }
    }
}
