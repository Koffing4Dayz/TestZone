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

        void OnEnable()
        {
            Initialize();
        }

        public void CallEventObjectThrow()
        {
            if(EventObjectThrow != null)
            {
                EventObjectThrow();
            }
            MasterPlayer.CallEventHandsEmpty();
            MasterPlayer.CallEventInventoryChanged();
        }

        public void CallEventObjectPickup()
        {
            if (EventObjectPickup != null)
            {
                EventObjectPickup();
            }
            MasterPlayer.CallEventInventoryChanged();
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
    }
}