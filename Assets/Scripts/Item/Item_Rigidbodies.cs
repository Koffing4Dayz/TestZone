using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class Item_Rigidbodies : MonoBehaviour
    {
        private Item_Master MasterItem;
        private Rigidbody[] Rigidbodies;

        private void OnEnable()
        {
            Initialize();
            MasterItem.EventObjectThrow += SetKinematicsFalse;
            MasterItem.EventObjectPickup += SetKinematicsTrue;
            IsStartingItem();
        }

        private void OnDisable()
        {
            MasterItem.EventObjectThrow -= SetKinematicsFalse;
            MasterItem.EventObjectPickup -= SetKinematicsTrue;
        }

        private void Initialize()
        {
            MasterItem = GetComponent<Item_Master>();
            Rigidbodies = GetComponentsInChildren<Rigidbody>();
        }

        private void IsStartingItem()
        {
            if (transform.root.CompareTag(GameManager.GameManager_References.Instance.PlayerTag))
            {
                SetKinematicsTrue();
            }
        }

        private void SetKinematicsTrue()
        {
            if (Rigidbodies.Length > 0)
            {
                foreach (Rigidbody item in Rigidbodies)
                {
                    item.isKinematic = true;
                }
            }
        }

        private void SetKinematicsFalse()
        {
            if (Rigidbodies.Length > 0)
            {
                foreach (Rigidbody item in Rigidbodies)
                {
                    item.isKinematic = false;
                }
            }
        }
    }
}
