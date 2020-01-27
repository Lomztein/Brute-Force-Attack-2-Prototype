using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Modifications.Modifiers
{
    public class Modifier : MonoBehaviour, INamed
    {
        protected Mod[] Modifications => _mods.ToArray();
        private Mod[] _mods;

        public string Name { get => _name; }
        public string Description { get => _description; }
        [SerializeField] private string _name;
        [SerializeField] private string _description;

        [SerializeField] private StatModInfo[] _statMods;
        [SerializeField] private EventModInfo[] _eventMods;

        public float Coeffecient { get { return Modifications.Average(x => x.Coeffecient.Get ()); } set { foreach (Mod mod in Modifications) { mod.Coeffecient.Set (value); }; } }

        public void SetName (string name)
        {
            _name = name;
        }

        public void SetDesc (string desc)
        {
            _description = desc;
        }

        public void SetStatMods (StatModInfo[] info)
        {
            _statMods = info;
        }

        public void SetEventMods (EventModInfo[] info)
        {
            _eventMods = info;
        }

        private void Awake()
        {
            List<ModInfo> infos = new List<ModInfo>();
            infos.AddRange(_statMods);
            infos.AddRange(_eventMods);
            _mods = GetMods(infos);
        }

        private Mod[] GetMods (List<ModInfo> info)
        {
            Mod[] mods = new Mod[info.Count];
            for (int i = 0; i < info.Count; i++)
            {
                mods[i] = info[i].GetMod();
            }
            return mods;
        }

        public bool IsComptabible (IModdable target)
        {
            return Modifications.All(x => x.IsCompatible(target));
        }

        public void AddTo (IModdable target, object source)
        {
            foreach (Mod mod in Modifications)
            {
                mod.AddSource(source);
                target.AddMod (mod);
            }
        }

        public void RemoveFrom (IModdable target, object source)
        {
            foreach (Mod mod in Modifications)
            {
                target.RemoveMod(mod);
                mod.RemoveSource(this);
                mod.Reset();
            }
        }
    }
}
