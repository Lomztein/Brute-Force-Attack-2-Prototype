using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BruteForceAttackSequel.UX.Selectables.Handlers
{
    public interface ISelectionHandler
    {
        SelectionController Controller { get; set; }
        ISelectable Selectable { get; set; }

        void Begin();
        void Tick(float deltaTime);
        void End();
    }
}
