using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Modifications
{
    public class ModProperty
    {
        public enum StackBehaviour
        {
            Add, AddMinusOne, Multiply, Max, Min
        }
        private readonly Dictionary<StackBehaviour, Action<ModProperty>> BehaviourMethods;

        public StackBehaviour Behaviour;
        private float _baseValue;
        private float _currentValue;

        public ModProperty () { }

        public ModProperty(float baseValue, StackBehaviour behaviour)
        {
            Set(baseValue);
            Behaviour = behaviour;

            BehaviourMethods = new Dictionary<StackBehaviour, Action<ModProperty>> {
                { StackBehaviour.Add, Add },
                { StackBehaviour.AddMinusOne, AddMinusOne},
                { StackBehaviour.Multiply, Multiply },
                { StackBehaviour.Max, Max },
                { StackBehaviour.Min, Min},
            };
        }

        public void Set (float value)
        {
            _baseValue = value;
            Reset();
        }

        public float Get ()
        {
            return _currentValue;
        }

        public float GetInt ()
        {
            return Mathf.RoundToInt (_currentValue);
        }

        public void Reset ()
        {
            _currentValue = _baseValue;
        }

        public void Stack (ModProperty other)
        {
            BehaviourMethods[Behaviour](other);
        }

        public void Add (ModProperty other)
        {
            _currentValue += other._currentValue;
        }

        public void AddMinusOne (ModProperty other)
        {
            _currentValue += other._currentValue - 1;
        }

        public void Multiply (ModProperty other)
        {
            _currentValue *= other._currentValue;
        }

        public void Max (ModProperty other)
        {
            _currentValue = Mathf.Max(_currentValue, other._currentValue);
        }

        public void Min (ModProperty other)
        {
            _currentValue = Mathf.Min(_currentValue, other._currentValue);
        }

        public override string ToString()
        {
            return Get().ToString();
        }
    }
}
