using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Modifiables
{
    public interface IEventContainer
    {
        Event GetEvent(Event.Type type);
    }
}
