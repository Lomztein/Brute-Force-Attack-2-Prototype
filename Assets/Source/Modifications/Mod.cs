using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Modifications
{
    public abstract class Mod
    {
        public enum Tag
        {
            /*Component Types*/ TurretComponent, TurretWeapon,
            /*Projectile Types*/ Projectile, ProjectilePhysical, ProjectileHitscan, ProjectileHoming
        }

        [ExposedProperty] public string StackIdentifier;
        [ExposedProperty] public Tag ModTag;
        public ModProperty Coeffecient = new ModProperty (1f, ModProperty.StackBehaviour.AddMinusOne);

        protected List<object> _sources = new List<object> ();
        public int SourceCount => _sources.Count;

        public event Action OnStackRecalculated;

        public abstract ModProperty[] Properties { get; }

        public void AddSource (object source)
        {
            if (!_sources.Contains (source))
            {
                _sources.Add(source);
                OnSourceAdded?.Invoke(source);
            }
        }

        public void RemoveSource (object source)
        {
            _sources.Remove(source);
            OnSourceRemoved?.Invoke(source);
        }

        private void ClearSources ()
        {
            foreach (object source in _sources)
            {
                OnSourceRemoved?.Invoke(source);
            }
            _sources.Clear();
        }

        public event Action<object> OnSourceAdded;
        public event Action<object> OnSourceRemoved;

        public virtual bool IsCompatible (IModdable target)
        {
            Tag[] targetTags = target.GetModTags();
            return targetTags.Contains(ModTag);
        }

        public abstract void ApplyEffect(IModdable target);

        public abstract void RemoveEffect(IModdable target);

        public void Reset ()
        {
            foreach (ModProperty property in Properties)
            {
                property.Reset();
            }
        }

        public void StackWith(Mod[] others)
        {
            Reset();
            foreach (Mod mod in others)
            {
                if (mod == this) continue;
                StackWith(mod);
            }
            OnStackRecalculated?.Invoke();
        }

        public void StackWith(Mod other)
        {
            if (StackIdentifier != other.StackIdentifier) throw new InvalidOperationException("Tried stacking two mods with different stacking identifiers. " + ToString () + " and " + other.ToString ());
            for (int i = 0; i < Properties.Length; i++)
            {
                ModProperty here = Properties[i];
                ModProperty others = other.Properties[i];
                here.Stack(others);
            }

            foreach (var source in other._sources)
            {
                AddSource(source);
            }
        }

        public override string ToString()
        {
            return GetType().Name + ", " + StackIdentifier;
        }
    }
}
