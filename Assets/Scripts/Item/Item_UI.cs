using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class Item_UI : MonoBehaviour
    {
        private Item_Master MasterItem;
        public GameObject myUI;

        private void OnEnable()
        {
            Initialize();
            MasterItem.EventObjectThrow += DisableUI;
            MasterItem.EventObjectPickup += EnableUI;
        }

        private void OnDisable()
        {
            MasterItem.EventObjectThrow -= DisableUI;
            MasterItem.EventObjectPickup -= EnableUI;
        }

        private void Initialize()
        {
            MasterItem = GetComponent<Item_Master>();
        }

        private void EnableUI()
        {
            if (myUI != null)
            {
                myUI.SetActive(true);
            }
        }

        private void DisableUI()
        {
            if (myUI != null)
            {
                myUI.SetActive(false);
            }
        }
    }
}
