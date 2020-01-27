using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.UX.Selectables.Visualizers
{
    public interface ISelectionVisualizer
    {
        ISelectable Visualized { get; set; }

        void BeginHover();
        void EndHover();

        void Begin();
        void Tick(float deltaTime);
        void End();
    }
}