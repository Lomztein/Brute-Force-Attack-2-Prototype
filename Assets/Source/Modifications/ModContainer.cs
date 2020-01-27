using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Modifications
{
    public class ModContainer : IModContainer
    {
        private List<Mod> _allMods = new List<Mod>();
        private List<Mod> _uniqueMods = new List<Mod>();

        public bool AddMod(Mod mod)
        {
            _allMods.Add(mod);
            if (TryAddUnique(mod))
            {
                return true;
            }
            this.RecalculateStack(mod.StackIdentifier);
            return false;
        }

        public Mod[] GetMods()
        {
            return _allMods.ToArray();
        }

        public bool RemoveMod(Mod mod)
        {
            if (TryRemoveUnique(mod))
            {
                _allMods.Remove(mod);
                if (this.GetModsOfIdentifier (mod.StackIdentifier).Length > 0)
                {
                    this.RecalculateStack(mod.StackIdentifier);
                }
                return true;
            }
            _allMods.Remove(mod);
            return false;
        }

        private bool TryAddUnique (Mod mod)
        {
            if (_uniqueMods.Exists (x => x.StackIdentifier == mod.StackIdentifier))
            {
                return false;
            }
            _uniqueMods.Add(mod);
            return true;
        }

        private bool TryRemoveUnique (Mod mod)
        {
            if (this.GetModsOfIdentifier (mod.StackIdentifier).Length != 1)
            {
                return false;
            }
            _uniqueMods.Remove(mod);
            return true;
        }

        public Mod[] GetUniqueMods()
        {
            return _uniqueMods.ToArray();
        }
    }
}
