using Lomztein.BruteForceAttackSequel.UX.Selectables;
using Lomztein.BruteForceAttackSequel.UX.Selectables.Handlers;
using Lomztein.BruteForceAttackSequel.UX.Selectables.Visualizers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Modifications.Modifiers.Providers
{
    public class TargetedModifierProvider : MonoBehaviour, IModifierProvider, ITargetableSelectable
    {
        public GameObject Target { get; set; }

        private GameObject _currentlyModded;
        private IModdable ModdableTarget => Target?.GetComponent<IModdable>();

        public Modifier Modifier;

        public event Action<IModdable> OnApplied;
        public event Action<IModdable> OnRemoved;

        [SerializeField] private Behaviour[] DisableIfApplied;

        public void Apply()
        {
            Modifier.AddTo(ModdableTarget, this);
            UpdateDisableIfApplied(false);
            _currentlyModded = Target;
        }

        private void UpdateDisableIfApplied (bool value)
        {
            foreach (Behaviour behaviour in DisableIfApplied)
            {
                behaviour.enabled = value;
            }
        }

        public bool Deselect()
        {
            return true;
        }

        public ISelectionHandler GetSelectionHandler() => new TargetableSelectionHandler(x => Modifier.IsComptabible (GetComponent<IModdable>()));
        public ISelectionVisualizer GetSelectionVisualizer() => new TargetableSelectionVisualizer(); 

        public void Remove()
        {
            Modifier.RemoveFrom(ModdableTarget, this);
            UpdateDisableIfApplied(true);
            _currentlyModded = null;
        }

        public bool Select()
        {
            if (_currentlyModded)
            {
                Remove();
            }
            return true;
        }
    }
}
