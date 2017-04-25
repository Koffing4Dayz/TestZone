using UnityEngine;
using System.Collections;

namespace S3
{
    public class Item_Master : MonoBehaviour
    {
        Player_Master playerMaster;
        
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
                playerMaster.CallEventHandsEmpty();
                playerMaster.CallEventInventoryChanged();
            }
        }

        public void CallEventObjectPickup()
        {
            if (EventObjectPickup != null)
            {
                EventObjectPickup();
                playerMaster.CallEventInventoryChanged();
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
                playerMaster = GameManager.GameManager_References.Instance.Player.GetComponent<Player_Master>();
            }
        }
    }
}