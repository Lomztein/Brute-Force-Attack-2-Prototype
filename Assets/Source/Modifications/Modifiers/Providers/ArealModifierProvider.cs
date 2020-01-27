using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Modifications.Modifiers.Providers
{
    class ArealModifierProvider : MonoBehaviour, IModifierProvider
    {
        public event Action<IModdable> OnApplied;
        public event Action<IModdable> OnRemoved;

        public Modifier Modifier;

        private IModdable[] _withinRange;
        public float Range;
        public LayerMask TargetLayer;

        private void Start()
        {
            Apply();
        }

        private void OnDisable()
        {
            try
            {
                Remove();
            } catch (Exception) { }
        }

        public void Apply()
        {
            _withinRange = GetModdablesWithinRange(Range, TargetLayer);
            foreach (IModdable moddable in _withinRange)
            {
                Modifier.AddTo(moddable, this);
                OnApplied?.Invoke(moddable);
            }
        }

        public void Remove()
        {
            foreach (IModdable moddable in _withinRange)
            {
                Modifier.RemoveFrom(moddable, this);
                OnRemoved?.Invoke(moddable);
            }
        }

        private IModdable[] GetModdablesWithinRange (float range, LayerMask targetLayer)
        {
            Collider2D[] nearby = Physics2D.OverlapCircleAll(transform.position, range, targetLayer);
            return nearby.Select(x => x.GetComponent<IModdable>()).Where(x => x != null).Where (x => Modifier.IsComptabible (x)).ToArray();
        }
    }
}
