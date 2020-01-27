using Lomztein.BruteForceAttackSequel.Modifications;
using Lomztein.BruteForceAttackSequel.Modifications.Modifiers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Placement
{
    public class ModifierPlacement : MonoBehaviour, IPlacement
    {
        private Modifier _modifier;
        private int _focusIndex = 0;
        private IModdable focusModdable;

        private void Update()
        {
            _focusIndex += (int)Input.GetAxis("Mouse ScrollWheel");
        }

        public bool PickUp(GameObject obj)
        {
            _modifier = obj.GetComponent<Modifier>();
            return true;
        }

        public bool Place()
        {
            if (focusModdable != null)
            {
                _modifier.AddTo(focusModdable, this);

                if (focusModdable is Component component)
                {
                    _modifier.transform.SetParent(component.transform);
                }
                Destroy(gameObject);
                return true;
            }
            return false;
        }

        public bool ToPosition(Vector2 position, Quaternion rotation)
        {
            focusModdable = null;
            transform.position = position;
            transform.rotation = rotation;
            return true;
        }

        public bool ToTransforms(Transform[] transforms)
        {
            IEnumerable<IModdable> moddableList = transforms.Select(x => x.GetComponent<IModdable>());
            IModdable[] moddables = moddableList.Where (x => _modifier.IsComptabible (x)).ToArray ();
            
            if (moddables.Length > 0)
            {
                focusModdable = moddables[Mathf.Abs (_focusIndex % moddables.Length)];
                return true;
            }
            return false;
        }

        private void OnGUI()
        {
            Vector2 mousePosition = Input.mousePosition;
            mousePosition.y = Screen.height - mousePosition.y;

            if (focusModdable == null)
            {
                GUI.Label(new Rect(mousePosition, new Vector2(500, 20)), _modifier.Name);
            }
            else
            {
                GUI.Label(new Rect(mousePosition, new Vector2(500, 20)), "Add " + _modifier.Name + " to " + (focusModdable as INamed).Name + " (Scroll to change)");
            }
        }
    }
}
