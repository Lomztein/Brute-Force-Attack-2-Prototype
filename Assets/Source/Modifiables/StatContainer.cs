using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Lomztein.BruteForceAttackSequel.Modifiables
{
    [System.Serializable]
    public class StatContainer : IStatContainer
    {
        private List<Stat> _stats = new List<Stat>();
        public List<StatBaseValueSetter> BaseValues;

        public void SetBaseValues ()
        {
            for (int i = 0; i < BaseValues.Count; i++)
            {
                GetStat(BaseValues[i].Type).SetBaseValue(BaseValues[i].BaseValue);
            }
        }

        public Stat GetStat (Stat.Type identifier)
        {
            return _stats.First(x => x.Identifier == identifier);
        }

        public void AddStat (Stat newStat)
        {
            _stats.Add(newStat);
        }

        public void RemoveStat (Stat.Type identifier)
        {
            _stats.Remove(_stats.First(x => x.Identifier == identifier));
        }

        public void AddStat(Stat.Type identifier, string name, string description, float baseValue)
        {
            _stats.Add(new Stat(identifier, name, description, baseValue));
        }
    }
}
