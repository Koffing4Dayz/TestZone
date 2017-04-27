using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class Item_Animator : MonoBehaviour
    {
        private Item_Master MasterItem;
        private Animator myAnimator;

        private void OnEnable()
        {
            Initialize();
            MasterItem.EventObjectThrow += DisableAnimator;
            MasterItem.EventObjectPickup += EnableAnimator;
        }

        private void OnDisable()
        {
            MasterItem.EventObjectThrow -= DisableAnimator;
            MasterItem.EventObjectPickup -= EnableAnimator;
        }

        private void Initialize()
        {
            MasterItem = GetComponent<Item_Master>();
            myAnimator = GetComponent<Animator>();
        }

        private void EnableAnimator()
        {
            if (myAnimator != null)
            {
                myAnimator.enabled = true;
            }
        }

        private void DisableAnimator()
        {
            if (myAnimator != null)
            {
                myAnimator.enabled = false;
            }
        }
    }
}
