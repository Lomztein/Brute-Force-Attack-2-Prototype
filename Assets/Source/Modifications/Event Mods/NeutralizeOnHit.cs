using Lomztein.BruteForceAttackSequel.Modifiables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BruteForceAttackSequel.Modifications.EventMods
{
    public class NeutralizeOnHit : EventMod<DamageEventInfo>
    {
        public override ModProperty[] Properties => new ModProperty[] { Coeffecient }; 

        public override void Execute(object source, DamageEventInfo info)
        {
            info.Info.Projectile.NeutralizeProjectile();
        }
    }
}
