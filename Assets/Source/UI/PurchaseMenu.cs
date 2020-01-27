using Lomztein.BruteForceAttackSequel.Modifications.Modifiers;
using Lomztein.BruteForceAttackSequel.Placement;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BruteForceAttackSequel.UI
{
    public class PurchaseMenu : MonoBehaviour
    {
        public PlacementController PlacementController;
        public GameObject ObjectPlacementPrefab;

        public RectTransform PurchaseList;
        public GameObject PurchaseButtonPrefab;

        public GameObject[] AvailableOptions;

        private void Start()
        {
            foreach (GameObject option in AvailableOptions)
            {
                GameObject newButton = Instantiate(PurchaseButtonPrefab, PurchaseList, false);
                Button butt = newButton.GetComponent<Button>();
                butt.onClick.AddListener(() => Purchase (option));

                INamed namedObject = option.GetComponent<INamed>();
                newButton.GetComponentInChildren<Text>().text = namedObject == null ? option.name : namedObject.Name;
            }
        }

        private void Purchase(GameObject mod)
        {
            GameObject newObjectPlacement = Instantiate(ObjectPlacementPrefab);
            GameObject newObject = Instantiate(mod);

            IPlacement placement = newObjectPlacement.GetComponent<IPlacement>();

            PlacementController.PickUp(placement, newObject);
        }
    }
}
