using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class Item_Pickup : MonoBehaviour
    {
        private Item_Master MasterItem;

        private void OnEnable()
        {
            Initialize();
            MasterItem.EventPickupAction += PickupActions;
        }

        private void OnDisable()
        {

            MasterItem.EventPickupAction -= PickupActions;
        }

        private void Initialize()
        {
            MasterItem = GetComponent<Item_Master>();
        }

        private void PickupActions(Transform parent)
        {
            transform.SetParent(parent);
            MasterItem.CallEventObjectPickup();
            transform.gameObject.SetActive(false);
        }
    }
}
