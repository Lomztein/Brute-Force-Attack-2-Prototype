using Lomztein.BruteForceAttackSequel.Modifiables;
using Lomztein.BruteForceAttackSequel.Modifications.Modifiers.Providers;
using Lomztein.BruteForceAttackSequel.Projectiles;
using Lomztein.BruteForceAttackSequel.Turrets;
using Lomztein.BruteForceAttackSequel.Turrets.Tools;
using Lomztein.BruteForceAttackSequel.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Modifications.EventMods
{
    public abstract class WeaponEventMod<T> : EventMod<T> where T : EventInfo
    {
        protected List<Weapon> _sourceWeapons = new List<Weapon>();

        protected Action<Weapon> OnWeaponSourceAdded;
        protected Action<Weapon> OnWeaponSourceRemoved;

        public WeaponEventMod ()
        {
            OnSourceAdded += WeaponEventMod_OnSourceAdded;
            OnSourceRemoved += WeaponEventMod_OnSourceRemoved;
        }

        private void WeaponEventMod_OnSourceRemoved(object obj)
        {
            Weapon weapon = (obj as Component).GetComponentInChildren<Weapon>();
            _sourceWeapons.Remove(weapon);
            OnWeaponSourceAdded?.Invoke(weapon);
        }

        private void WeaponEventMod_OnSourceAdded(object obj)
        {
            Weapon weapon = (obj as Component).GetComponentInChildren<Weapon>();
            _sourceWeapons.Add(weapon);
            OnWeaponSourceRemoved?.Invoke(weapon);
        }

        protected void AttachProjectileToSources (Projectile proj)
        {
            foreach (var weapon in _sourceWeapons)
            {
                weapon.AttachProjectileCallbacks(proj);
            }
        }
    }
}
