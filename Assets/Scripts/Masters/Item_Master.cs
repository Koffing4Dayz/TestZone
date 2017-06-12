using UnityEngine;
using System.Collections;

namespace Item
{
    public class Item_Master : MonoBehaviour
    {
        Player.Player_Master MasterPlayer;
        
        public delegate void GeneralEventHandler();
        public event GeneralEventHandler EventObjectThrow;
        public event GeneralEventHandler EventObjectPickup;

        public delegate void PickupActionEventHandler(Transform item);
        public event PickupActionEventHandler EventPickupAction;

        private bool isOnPlayer;

        void Start()
        {
            Initialize();
            CheckIfIsOnPlayer();
        }

        public void CallEventObjectThrow()
        {
            if(EventObjectThrow != null)
            {
                EventObjectThrow();
            }

            if (isOnPlayer)
            {
                MasterPlayer.CallEventHandsEmpty();
                MasterPlayer.CallEventInventoryChanged();
                CheckIfIsOnPlayer();
            }
        }

        public void CallEventObjectPickup()
        {
            if (EventObjectPickup != null)
            {
                EventObjectPickup();
            }

            if (!isOnPlayer)
            {
                MasterPlayer.CallEventInventoryChanged();
                CheckIfIsOnPlayer();
            }
        }

        public void CallEventPickupAction(Transform item)
        {
            if(EventPickupAction != null)
            {
                EventPickupAction(item);
            }
        }

        void Initialize()
        {
            if(GameManager.GameManager_References.Instance.Player != null)
            {
                MasterPlayer = GameManager.GameManager_References.Instance.MasterPlayer;
            }
        }

        private void CheckIfIsOnPlayer()
        {
            if (transform.root == GameManager.GameManager_References.Instance.Player.transform)
            {
                isOnPlayer = true;
            }
            else
            {
                isOnPlayer = false;
            }
        }
    }
}