using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class Item_Layer : MonoBehaviour
    {
        private Item_Master MasterItem;

        private void OnEnable()
        {
            Initialize();
            MasterItem.EventObjectPickup += ToHoldLayer;
            MasterItem.EventObjectThrow += ToPickupLayer;
            IsStartingItem();
        }

        private void OnDisable()
        {
            MasterItem.EventObjectPickup -= ToHoldLayer;
            MasterItem.EventObjectThrow -= ToPickupLayer;
        }

        private void Initialize()
        {
            MasterItem = GetComponent<Item_Master>();
        }

        private void IsStartingItem()
        {
            if (transform.root.CompareTag(GameManager.GameManager_References.Instance.PlayerTag))
            {
                ToHoldLayer();
            }
            else
            {
                ToPickupLayer();
            }
        }

        private void ToHoldLayer()
        {
            SetLayer(transform, GameManager.GameManager_References.Instance.HoldLayer);
        }

        private void ToPickupLayer()
        {
            SetLayer(transform, GameManager.GameManager_References.Instance.PickupLayer);
        }

        private void SetLayer(Transform tForm, LayerMask layer)
        {
            tForm.gameObject.layer = layer;

            foreach (Transform item in tForm)
            {
                SetLayer(item, layer);
            }
        }
    }
}
