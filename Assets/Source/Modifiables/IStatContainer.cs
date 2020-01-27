using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BruteForceAttackSequel.Modifiables
{
    public interface IStatContainer
    {
        Stat GetStat(Stat.Type identifier);

        void AddStat(Stat.Type identifier, string name, string description, float baseValue);

        void RemoveStat(Stat.Type identifier);
    }
}
