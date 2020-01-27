using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Modifications.Modifiers.Providers
{
    public class NaturalModifierProvider : MonoBehaviour, IModifierProvider
    {
        [SerializeField] private Modifier _modifier;

        public event Action<IModdable> OnApplied;
        public event Action<IModdable> OnRemoved;

        private IModdable GetTarget() => GetComponent<IModdable>();

        private void Start()
        {
            Apply();
        }

        public void Apply()
        {
            _modifier.AddTo(GetTarget (), this);
            OnApplied?.Invoke(GetTarget());
        }

        public void Remove()
        {
            _modifier.RemoveFrom(GetComponent<IModdable>(), this);
            OnRemoved?.Invoke(GetTarget());
        }
    }
}
