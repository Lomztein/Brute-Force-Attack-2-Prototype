using System.Linq;
using UnityEngine;

namespace Lomztein.BruteForceAttackSequel.Placement
{
    public class PlacementController : MonoBehaviour
    {
        private IPlacement _currentPlaceable;
        public LayerMask applicablePlacementLayers;

        private void Update()
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (_currentPlaceable != null)
            {
                _currentPlaceable.ToPosition(mousePos, Quaternion.identity);
                Collider2D[] colliders = Physics2D.OverlapPointAll(mousePos, applicablePlacementLayers);

                if (colliders.Length > 0)
                {
                    _currentPlaceable.ToTransforms(colliders.Select(x => x.transform).ToArray());
                }

                if (Input.GetMouseButtonDown(0))
                {
                    PlaceCurrent();
                }
            }
        }

        public bool PickUp(IPlacement placeable, GameObject obj)
        {
            if (placeable.PickUp (obj))
            {
                _currentPlaceable = placeable;
                return true;
            }
            return false;
        }

        public bool PlaceCurrent ()
        {
            if (_currentPlaceable.Place ())
            {
                _currentPlaceable = null;
                return true;
            }
            return false;
        }
    }
}
