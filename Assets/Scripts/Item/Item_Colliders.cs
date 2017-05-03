using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class Item_Colliders : MonoBehaviour
    {
        private Item_Master MasterItem;
        public Collider[] Colliders;
        public PhysicMaterial myPhysicMat;

        private void OnEnable()
        {
            Initialize();
            MasterItem.EventObjectThrow += EnableColliders;
            MasterItem.EventObjectPickup += DisableColliders;
        }

        private void OnDisable()
        {
            MasterItem.EventObjectThrow -= EnableColliders;
            MasterItem.EventObjectPickup -= DisableColliders;
        }

        private void Initialize()
        {
            MasterItem = GetComponent<Item_Master>();
            //Colliders = GetComponentsInChildren<Collider>();
            IsStartingItem();
        }

        private void IsStartingItem()
        {
            if (transform.root.CompareTag(GameManager.GameManager_References.Instance.PlayerTag))
            {
                DisableColliders();
            }
        }

        private void EnableColliders()
        {
            if (Colliders.Length > 0)
            {
                foreach (Collider item in Colliders)
                {
                    item.enabled = true;

                    if (myPhysicMat != null)
                    {
                        item.material = myPhysicMat;
                    }
                }
            }
        }

        private void DisableColliders()
        {
            if (Colliders.Length > 0)
            {
                foreach (Collider item in Colliders)
                {
                    item.enabled = false;
                }
            }
        }
    }
}
