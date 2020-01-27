using Lomztein.BruteForceAttackSequel.UX.Selectables.Handlers;
using Lomztein.BruteForceAttackSequel.UX.Selectables.Visualizers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BruteForceAttackSequel.UX.Selectables
{
    public interface ISelectable
    {
        bool Select();
        bool Deselect();

        ISelectionHandler GetSelectionHandler();
        ISelectionVisualizer GetSelectionVisualizer();
    }
}
