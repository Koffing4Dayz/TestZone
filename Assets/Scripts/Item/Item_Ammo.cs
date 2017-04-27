using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class Item_Ammo : MonoBehaviour
    {
        private Item_Master MasterItem;
        private GameObject playerGO;
        public string AmmoName;
        public int Quanity;
        public bool IsTriggerPickup;

        private void OnEnable()
        {
            Initialize();
            MasterItem.EventObjectPickup += TakeAmmo;
        }

        private void OnDisable()
        {
            MasterItem.EventObjectPickup -= TakeAmmo;
        }

        private void Start()
        {
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GameManager.GameManager_References.Instance.PlayerTag) && IsTriggerPickup)
            {
                TakeAmmo();
            }
        }

        private void Initialize()
        {
            MasterItem = GetComponent<Item_Master>();
            playerGO = GameManager.GameManager_References.Instance.Player;

            if (IsTriggerPickup)
            {
                if (GetComponent<Collider>() != null)
                {
                    GetComponent<Collider>().isTrigger = true;
                }
                if (GetComponent<Rigidbody>() != null)
                {
                    GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }

        private void TakeAmmo()
        {
            playerGO.GetComponent<Player.Player_Master>().CallEventPickUpAmmo(AmmoName, Quanity);
            Destroy(gameObject);
        }
    }
}
