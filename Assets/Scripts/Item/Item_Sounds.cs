using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class Item_Sounds : MonoBehaviour
    {
        private Item_Master MasterItem;

        public float ThrowVolume;
        public AudioClip ThrowSound;
        public float PickupVolume;
        public AudioClip PickupSound;

        private void OnEnable()
        {
            Initialize();
            MasterItem.EventObjectThrow += PlayThrowSound;
            MasterItem.EventObjectPickup += PlayPickupSound;
        }

        private void OnDisable()
        {
            MasterItem.EventObjectThrow -= PlayThrowSound;
            MasterItem.EventObjectPickup -= PlayPickupSound;
        }

        private void Initialize()
        {
            MasterItem = GetComponent<Item_Master>();
        }

        private void PlayThrowSound()
        {
            if (ThrowSound != null)
            {
                AudioSource.PlayClipAtPoint(ThrowSound, transform.position, ThrowVolume);
            }
        }

        private void PlayPickupSound()
        {
            if (ThrowSound != null)
            {
                AudioSource.PlayClipAtPoint(PickupSound, transform.position, PickupVolume);
            }
        }
    }
}
