using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BruteForceAttackSequel.Damage
{
    public interface IDamagable
    {
        float TakeDamage(DamageInfo damage);
    }
}
