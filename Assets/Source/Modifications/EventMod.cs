using Lomztein.BruteForceAttackSequel.Modifiables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BruteForceAttackSequel.Modifications
{
    public abstract class EventMod : Mod
    {
        [ExposedPropertyAttribute] public Event.Type Event;
        public Event.EventHandler Handler;

        public override void ApplyEffect(IModdable target)
        {
            IEventContainer container = target as IEventContainer;
            container.GetEvent(Event).Attach(Handler);
        }

        public override void RemoveEffect(IModdable target)
        {
            IEventContainer container = target as IEventContainer;
            container.GetEvent(Event).Detach(Handler);
        }
    }

    public abstract class EventMod<T> : EventMod where T : EventInfo
    {
        public EventMod () {
            Handler = (source, info) => Execute(source, info as T);
        }

        public abstract void Execute(object source, T info);
    }
}
