using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lomztein.BruteForceAttackSequel.Modifiables;
using Lomztein.BruteForceAttackSequel.Modifications;
using System.Linq;
using System;

namespace Lomztein.BruteForceAttackSequel.Turrets
{
    public abstract class TurretComponent : MonoBehaviour, IStatContainer, IEventContainer, IModdable, IModContainer, ITurretRoot, INamed
    {
        public enum Size { Small = 1, Medium = 2, Large = 3 }

        public string Name { get => _name; }
        public string Description { get => _description; }
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        public Size TurretSize;

        public TurretAssembly Assembly { get; protected set; }

        public StatContainer Stats = new StatContainer();
        public EventContainer Events = new EventContainer();
        public IModContainer Mods = new ModContainer();

        [SerializeField] protected List<Mod.Tag> _baseModTags = new List<Mod.Tag>();
        private List<Mod.Tag> _tempModTags = new List<Mod.Tag>();
        private List<Mod> _currentMods = new List<Mod>();

        [SerializeField] private ComponentAttachmentPoint[] _componentAttachmentPoint;
        public ComponentAttachmentPoint[] ComponentAttachmentPoints => _componentAttachmentPoint;

        public void Init()
        {
            PreInitComponent();

            Rebuild();
            Stats.AddStat(new Stat(Stat.Type.Turret_Speed, "Clock", "A speed modifier that applies to all turret components. This description should be overwritted by inheriters.", 0f));
            Stats.AddStat(new Stat(Stat.Type.Turret_Strength, "Charge", "A power modifier that applies to all turret components. This description should be overwritted by inheriters.", 0f));

            _baseModTags.Add(Mod.Tag.TurretComponent);

            InitComponent();

            Stats.SetBaseValues();
        }

        private void Start()
        {
            PostInitComponent();
        }

        private void Rebuild ()
        {
            if (Assembly)
                Assembly.RemoveComponents(this);
            Assembly = GetComponentInParent<TurretAssembly>();
        }

        public void FixedUpdate()
        {
            Tick(Time.fixedDeltaTime);
        }

        public Modifiables.Event GetEvent(Modifiables.Event.Type type)
        {
            return ((IEventContainer)Events).GetEvent(type);
        }

        public Stat GetStat(Stat.Type identifier)
        {
            return ((IStatContainer)Stats).GetStat(identifier);
        }

        public void AddStat (Stat.Type identifier, string name, string description, float baseValue)
        {
            Stats.AddStat(new Stat(identifier, name, description, baseValue));
        }

        public void RemoveStat (Stat.Type identifier)
        {
            Stats.RemoveStat(identifier);
        }

        protected virtual void PreInitComponent() { }

        protected abstract void InitComponent();

        protected virtual void PostInitComponent() { }

        protected abstract void Tick(float deltaTime);

        public Mod.Tag[] GetModTags()
        {
            List<Mod.Tag> modSet = new List<Mod.Tag>();
            modSet.AddRange(_baseModTags);
            modSet.AddRange(_tempModTags);
            return modSet.ToArray();
        }

        public void AddTag(Mod.Tag tag)
        {
            _tempModTags.Add(tag);
        }

        public void RemoveTag(Mod.Tag tag)
        {
            _tempModTags.Remove(tag);
        }

        public Mod[] GetCurrentMods()
        {
            return _currentMods.ToArray();
        }

        public Size GetSize()
        {
            return TurretSize;
        }

        private T GetNearestAttachmentPoint<T> (Vector2 position, T[] points, Predicate<T> filter, bool requireFree = false) where T : AttachmentPoint
        {
            float score = float.MaxValue;
            T nearest = null;

            foreach (T point in points)
            {
                Vector2 worldPos = (Vector2)transform.position + point.Position;
                float pointScore = Vector3.SqrMagnitude(worldPos - position);

                if (pointScore < score && !(requireFree && !point.IsFree ()) && filter (point))
                {
                    pointScore = score;
                    nearest = point;
                }
            }

            return nearest;
        }

        public ComponentAttachmentPoint GetNearestComponentAttachmentPoint(Vector2 position, Size size, bool requireFree = false) => GetNearestAttachmentPoint(position, ComponentAttachmentPoints, x => x.Size == size, requireFree);

        public Vector2 AttachmentToWorldPosition(AttachmentPoint point) => transform.position + (transform.rotation * point.Position);
        public Quaternion AttachmentToWorldRotation(AttachmentPoint point) => transform.rotation * Quaternion.Euler (0f, 0f, point.Angle);

        public T FindComponent<T> (bool recursive = false) 
        {
            if (this is T thisComp)
                return thisComp;

            foreach (ComponentAttachmentPoint point in ComponentAttachmentPoints)
            {
                if (!point.IsFree())
                {
                    if (point.Child is T comp)
                    {
                        return comp;
                    }

                    if (recursive)
                    {
                        return point.Child.FindComponent<T>(recursive);
                    }
                }
            }
            return default;
        }

        private void OnDrawGizmosSelected()
        {
            foreach (ComponentAttachmentPoint point in ComponentAttachmentPoints)
            {
                Gizmos.DrawWireSphere(AttachmentToWorldPosition(point), (float)point.Size / 2f);
            }
        }

        public Mod[] GetMods()
        {
            return Mods.GetMods();
        }

        public Mod[] GetUniqueMods()
        {
            return Mods.GetUniqueMods();
        }

        public bool AddMod(Mod mod)
        {
            if (Mods.AddMod(mod))
            {
                ModApplier.ApplyMod(mod, this);
                return true;
            }
            return false;
        }

        public bool RemoveMod(Mod mod)
        {
            if (Mods.RemoveMod(mod))
            {
                ModApplier.RemoveMod(mod, this);
                return true;
            }
            return false;
        }
    }

}
