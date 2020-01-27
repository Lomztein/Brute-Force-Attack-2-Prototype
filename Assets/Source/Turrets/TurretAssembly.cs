using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BruteForceAttackSequel.Turrets.TargetFinders;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Turrets
{
    public class TurretAssembly : MonoBehaviour, IJsonSerializable, ITurretRoot, INamed
    {
        public string Name { get => _name; }
        public string Description { get => _description; }
        [SerializeField] private string _name;
        [SerializeField] private string _description;

        public TurretComponent RootComponent;
        private IRanger _rootRanger;

        private List<TurretComponent> _allComponents = new List<TurretComponent>();

        public void Init()
        {
            Rebuild();
        }

        public void Rebuild ()
        {
            RootComponent = GetComponentInChildren<TurretComponent>();
            _rootRanger = RootComponent.FindComponent<IRanger>(true);
        }

        public float GetRange ()
        {
            return _rootRanger.GetRange();
        }

        public JToken Serialize()
        {
            throw new NotImplementedException();
        }

        public TurretComponent.Size GetSize()
        {
            return RootComponent.GetSize();
        }

        public void AddComponents (params TurretComponent[] component)
        {
            _allComponents.AddRange(component);
            Rebuild();
        }

        public void AddComponents (GameObject root) => AddComponents(root.GetComponentInChildren<TurretComponent>());

        public void RemoveComponents (params TurretComponent[] components)
        {
            _allComponents.RemoveAll(x => components.Contains (x)); // ¯\_(ツ)_/¯
            Rebuild();
        }

        public void CombineInto (TurretAssembly other)
        {
            other.AddComponents(_allComponents.ToArray ());
            Destroy(this);
        }
    }
}
