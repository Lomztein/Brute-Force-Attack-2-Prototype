using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Modifiables
{
    public class EventContainer : IEventContainer
    {
        private Dictionary<Event.Type, Event> _events = new Dictionary<Event.Type, Event>();

        public Event GetEvent(Event.Type type)
        {
            return _events[type];
        }

        public void AddEvent (Event.Type type)
        {
            _events.Add(type, new Event());
        }

        public void RemoveEvent (Event.Type type)
        {
            _events.Remove(type);
        }
    }
}

