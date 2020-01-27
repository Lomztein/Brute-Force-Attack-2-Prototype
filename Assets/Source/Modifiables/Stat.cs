using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Modifiables
{
    public class Stat
    {
        public enum Type
        {
            Turret_Strength, Turret_Speed,
            Weapon_Precision, Weapon_ProjectileAmount
        }
        public Type Identifier;

        public string Name;
        public string Description;

        public float BaseValue { get; internal set; }
        private readonly Dictionary<object, float> _additiveModifiers;

        private readonly Dictionary<object, float> _multiplicativeModifiers;

        private bool _hasBeenModified = true;
        private float _cachedValue = 0f;

        public event Action<float> OnModified;

        public Stat(Type identifier, string name, string description, float baseValue)
        {
            Identifier = identifier;
            Name = name;
            Description = description;
            BaseValue = baseValue;

            _additiveModifiers = new Dictionary<object, float>();
            _multiplicativeModifiers = new Dictionary<object, float>();
        }

        internal void SetBaseValue(float baseValue)
        {
            BaseValue = baseValue;
        }

        public void SetNameAndDesc (string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void AddAdditive(float value, object source)
        {
            _additiveModifiers.Add(source, value);
            RaiseOnModified();
        }

        public void RemoveAdditive(object source)
        {
            _additiveModifiers.Remove(source);
            RaiseOnModified();
        }

        public void AddMultiplicative(float value, object source)
        {
            _multiplicativeModifiers.Add(source, value);
            RaiseOnModified();
        }

        public void RemoveMultiplicative(object source)
        {
            _multiplicativeModifiers.Remove(source);
            RaiseOnModified();
        }

        private void RaiseOnModified ()
        {
            _hasBeenModified = true;
            OnModified?.Invoke(GetValue ());
            Debug.Log("Raise On Modified for " + Name);
        }

        public float GetValue()
        {
            if (_hasBeenModified == false)
                return _cachedValue;

            _cachedValue = BaseValue + _additiveModifiers.Sum(x => x.Value);
            for (int i = 0; i < _multiplicativeModifiers.Count; i++)
            {
                _cachedValue *= _multiplicativeModifiers.ElementAt(i).Value;
            }

            return _cachedValue;
        }
    }

}