using Lomztein.BruteForceAttackSequel.Modifiables;
using Lomztein.BruteForceAttackSequel.Modifications;
using Lomztein.BruteForceAttackSequel.Turrets.Targeters;
using Lomztein.BruteForceAttackSequel.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Turrets.Tools
{
    public class TurretWeapon : TurretComponent
    {
        private Weapon _weapon;
        private ITargeter _targeter;

        public float AngleTolerance;

        protected override void InitComponent()
        {
            Stats.GetStat(Stat.Type.Turret_Speed).SetNameAndDesc("Firerate", "The amount of times this weapon can fire per second.");
            Stats.GetStat(Stat.Type.Turret_Strength).SetNameAndDesc("Damage", "The amount of damage this weapon does.");

            Stats.AddStat(new Stat(Stat.Type.Weapon_Precision, "Prisicion", "The prisicion of this weapon, the higher the better.", 0));
            Stats.AddStat(new Stat(Stat.Type.Weapon_ProjectileAmount, "Projectiles", "The amount of projectiles fired by this weapon.", 1));

            Events.AddEvent(Modifiables.Event.Type.OnHit);
            Events.AddEvent(Modifiables.Event.Type.OnFirstHit);
            Events.AddEvent(Modifiables.Event.Type.OnPierce);
            Events.AddEvent(Modifiables.Event.Type.OnLastHit);
            Events.AddEvent(Modifiables.Event.Type.OnAOEHit);
            Events.AddEvent(Modifiables.Event.Type.OnDOTHit);
            Events.AddEvent(Modifiables.Event.Type.OnBeginFire);
            Events.AddEvent(Modifiables.Event.Type.OnFire);
            Events.AddEvent(Modifiables.Event.Type.OnProjectileFired);
            Events.AddEvent(Modifiables.Event.Type.OnProjectileFinished);

            _baseModTags.Add(Mod.Tag.TurretWeapon);

            _weapon = GetComponentInChildren<Weapon>();
            _targeter = transform.parent.GetComponent<ITargeter>();
        }

        protected override void PostInitComponent()
        {
            SetWeaponStats();
            AttachStatUpdates();
            AttachWeaponEvents();
            if (_targeter != null)
            {
                _targeter.PredictionSpeed = GetProjectileSpeed();
            }
        }

        private void SetWeaponStats ()
        {
            _weapon.SetFirerate(Stats.GetStat(Stat.Type.Turret_Speed).GetValue());
            _weapon.SetDamage(Stats.GetStat(Stat.Type.Turret_Strength).GetValue());
            _weapon.SetPrecision(Stats.GetStat(Stat.Type.Weapon_Precision).GetValue());
            _weapon.SetProjectileMul(Stats.GetStat(Stat.Type.Weapon_ProjectileAmount).GetValue());
        }

        private void AttachStatUpdates ()
        {
            Stats.GetStat(Stat.Type.Turret_Speed).OnModified += _weapon.SetFirerate;
            Stats.GetStat(Stat.Type.Turret_Strength).OnModified += _weapon.SetDamage;
            Stats.GetStat(Stat.Type.Weapon_Precision).OnModified += _weapon.SetPrecision;
            Stats.GetStat(Stat.Type.Weapon_ProjectileAmount).OnModified += _weapon.SetProjectileMul;
        }

        private void AttachWeaponEvents ()
        {
            _weapon.OnHit += (info) => Events.GetEvent(Modifiables.Event.Type.OnHit).Raise(this, new DamageEventInfo(info));
            _weapon.OnFirstHit += (info) => Events.GetEvent(Modifiables.Event.Type.OnFirstHit).Raise(this, new DamageEventInfo(info));
            _weapon.OnPierce += (info) => Events.GetEvent(Modifiables.Event.Type.OnPierce).Raise(this, new DamageEventInfo(info));
            _weapon.OnLastHit += (info) => Events.GetEvent(Modifiables.Event.Type.OnLastHit).Raise(this, new DamageEventInfo(info));
            _weapon.OnAOEHit += (info) => Events.GetEvent(Modifiables.Event.Type.OnAOEHit).Raise(this, new DamageEventInfo(info));
            _weapon.OnDOTHit += (info) => Events.GetEvent(Modifiables.Event.Type.OnDOTHit).Raise(this, new DamageEventInfo(info));
            _weapon.OnBeginFire += (info) => Events.GetEvent(Modifiables.Event.Type.OnBeginFire).Raise(this, info);
            _weapon.OnFire += (info) => Events.GetEvent(Modifiables.Event.Type.OnFire).Raise(this, info);
            _weapon.OnProjectileFired += (info) => Events.GetEvent(Modifiables.Event.Type.OnProjectileFired).Raise(this, info);
            _weapon.OnProjectileFinished += (info) => Events.GetEvent(Modifiables.Event.Type.OnProjectileFinished).Raise(this, info);
        }

        public float GetProjectileSpeed ()
        {
            return _weapon.GetProjectileSpeed();
        }

        private void OnDisable()
        {
            if (_weapon)
                _weapon.Firing = false;
        }

        protected override void Tick(float deltaTime)
        {
            if (_targeter != null)
            {
                _weapon.Firing = false;
                if (_targeter.AngularTargetDistance () < AngleTolerance) {
                    _weapon.SetRange(Assembly.GetRange());
                    _weapon.Firing = true;
                    _weapon.Target = _targeter?.GetTarget();
                }
            }else
            {
                _weapon.Fire();
            }
        }
    }
}
