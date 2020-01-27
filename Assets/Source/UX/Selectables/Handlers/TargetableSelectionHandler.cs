using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.UX.Selectables.Handlers
{
    public class TargetableSelectionHandler : ISelectionHandler
    {
        public ITargetableSelectable TargetableSelectable { get => Selectable as ITargetableSelectable; set => Selectable = value; }
        public ISelectable Selectable { get; set; }
        public SelectionController Controller { get; set; }

        private readonly Predicate<Collider2D> _filter;
        private GameObject _currentTarget;

        public TargetableSelectionHandler (Predicate<Collider2D> filter)
        {
            _filter = filter;
        }

        public void Begin()
        {
            TargetableSelectable.Select();
        }

        public void Tick(float deltaTime)
        {
            var applicable = Physics2D.OverlapPointAll(World.MousePosition).Where(x => _filter(x)).ToArray();
            TargetableSelectable.Target = applicable.FirstOrDefault()?.gameObject;

            if (Input.GetMouseButtonDown (1))
            {
                TargetableSelectable.Apply();
            }
        }

        public void End()
        {
            TargetableSelectable.Deselect();
        }
    }
}
