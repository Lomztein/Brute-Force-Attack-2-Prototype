using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.UX.Selectables
{
    public interface ITargetableSelectable : ISelectable
    {
        GameObject Target { get; set; }

        void Apply();
    }
}
