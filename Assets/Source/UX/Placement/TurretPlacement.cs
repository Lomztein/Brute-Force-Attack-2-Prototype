using Lomztein.BruteForceAttackSequel.Turrets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Placement
{
    public class TurretPlacement : MonoBehaviour, IPlacement
    {
        private GameObject _turret;
        private ITurretRoot _root => _turret.GetComponent<ITurretRoot>();

        public LayerMask TurretLayer;

        [SerializeField] private GameObject _assemblyPrefab;

        private TurretComponent _hoverComponent;
        private TurretAssembly _hoverAssembly => _hoverComponent.Assembly;
        private ComponentAttachmentPoint _hoverPoint;

        public bool PickUp(GameObject obj)
        {
            _turret = obj;

            if (IsAssembly ())
            {
                GetAssembly().Rebuild();
            }

            SetTurretComponentsEnabled(_turret, false);
            _turret.transform.SetParent(transform);
            _turret.transform.position = transform.position;
            return true;
        }

        public bool Place()
        {
            if (CanPlace(transform.position, (int)_root.GetSize() / 2f - 0.1f))
            {
                if (IsAssembly())
                {
                    if (_hoverPoint == null)
                    {
                        SetTurretComponentsEnabled(_turret, true);
                        InitComponents(_turret);
                        return true;
                    }
                    return false;
                }
                else
                {
                    if (_hoverPoint == null)
                    {
                        GameObject newAssembly = Instantiate(_assemblyPrefab, transform);
                        newAssembly.transform.position = transform.position;
                        _turret.transform.SetParent(newAssembly.transform);
                        newAssembly.GetComponent<TurretAssembly>().AddComponents(_turret);
                        _turret = newAssembly;
                        GetAssembly().Init();

                        SetTurretComponentsEnabled(_turret, true);
                        InitComponents(_turret);
                    }
                    else
                    {
                        _turret.transform.SetParent(_hoverComponent.transform);
                        _hoverAssembly.AddComponents(_turret);
                        SetTurretComponentsEnabled(_turret, true);
                        InitComponents(_turret);
                        _hoverPoint.Child = _turret.GetComponent<TurretComponent>();
                    }
                }
                return true;
            }
            return false;
        }

        public bool ToPosition(Vector2 position, Quaternion rotation)
        {
            _hoverPoint = null;
            _hoverComponent = null;

            int size = (int)_root.GetSize();
            if (size == 1)
            {
                position = new Vector2(Mathf.Round (position.x - 0.5f) + 0.5f, Mathf.Round (position.y - 0.5f) + 0.5f);
            } else
            {
                position = new Vector2(
                    Mathf.Round(position.x / size) * size,
                    Mathf.Round(position.y / size) * size
                );
            }

            transform.position = position;
            transform.rotation = rotation;

            return CanPlace(transform.position, size / 2f);
        }

        private bool CanPlace (Vector2 position, float radius)
        {
            return _hoverPoint != null || Physics2D.OverlapCircle(position, radius, TurretLayer) == null;
        }

        public bool ToTransforms(Transform[] transforms)
        {
            _hoverPoint = null;
            _hoverComponent = null;

            var components = transforms.Select(x => x.GetComponent<TurretComponent>()).Where(x => x != null);
            foreach (TurretComponent component in components)
            {
                ComponentAttachmentPoint point = component.GetNearestComponentAttachmentPoint(transform.position, _root.GetSize (), true);
                if (point != null)
                {
                    _hoverPoint = point;
                    _hoverComponent = component;
                }
            }

            if (_hoverPoint != null)
            {
                transform.position = (Vector3)_hoverComponent.AttachmentToWorldPosition (_hoverPoint) + Vector3.back;
                transform.rotation = _hoverComponent.AttachmentToWorldRotation(_hoverPoint);
            }

            return false;
        }

        private bool IsAssembly ()
        {
            return GetAssembly () != null;
        }

        private TurretAssembly GetAssembly() => _turret.GetComponent<TurretAssembly>();

        private void SetTurretComponentsEnabled(GameObject root, bool value)
        {
            MonoBehaviour[] components = root.GetComponentsInChildren<MonoBehaviour>();
            Collider2D[] colliders = root.GetComponentsInChildren<Collider2D>();
            foreach (MonoBehaviour component in components)
            {
                component.enabled = value;
            }

            foreach (Collider2D collider in colliders)
            {
                collider.enabled = value;
            }
        }

        private void InitComponents (GameObject root)
        {
            foreach (TurretComponent component in root.GetComponentsInChildren<TurretComponent>())
            {
                component.Init();
            }
        }
    }
}
