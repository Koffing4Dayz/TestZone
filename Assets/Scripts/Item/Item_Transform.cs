using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class Item_Transform : MonoBehaviour
    {
        private Item_Master MasterItem;
        private Transform myTransform;
        private Vector3 myScale;

        public Vector3 HeldPosition = Vector3.zero;
        public Vector3 HeldRotation = Vector3.zero;
        public Vector3 HeldScale = Vector3.one;

        private void Start()
        {
            myScale = myTransform.localScale;
        }

        private void OnEnable()
        {
            Initialize();
            MasterItem.EventObjectPickup += SetOnPlayer;
            MasterItem.EventObjectThrow += RestoreScale;
        }

        private void OnDisable()
        {
            MasterItem.EventObjectPickup -= SetOnPlayer;
            MasterItem.EventObjectThrow -= RestoreScale;
        }

        private void Initialize()
        {
            MasterItem = GetComponent<Item_Master>();
            myTransform = transform;
            SetOnPlayer();
        }

        private void SetOnPlayer()
        {
            if (myTransform.root.CompareTag(GameManager.GameManager_References.Instance.PlayerTag))
            {
                myTransform.localPosition = GameManager.GameManager_References.Instance.Player.GetComponent<Player.Player_Inventory>().HandOffset + HeldPosition;
                myTransform.localRotation = Quaternion.Euler(HeldRotation);
                myTransform.localScale = HeldScale;
            }
        }

        private void RestoreScale()
        {
            myTransform.localScale = myScale;
        }

        private void IsStartingItem()
        {
            if (transform.root.CompareTag(GameManager.GameManager_References.Instance.PlayerTag))
            {
                SetOnPlayer();
            }
        }
    }
}
