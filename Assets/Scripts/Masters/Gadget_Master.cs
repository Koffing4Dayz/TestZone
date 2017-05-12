using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gadget
{
    public class Gadget_Master : MonoBehaviour
    {
        Item.Item_Master MasterItem;

        public delegate void GeneralEventHandler();
        public event GeneralEventHandler EventActivateAbility;
        public event GeneralEventHandler EventDeactivateAbility;
        public event GeneralEventHandler EventRunUpdate;

        public delegate void EffectObjectEventHandler(GameObject obj);
        public event EffectObjectEventHandler EventEffectedObject;

        private void Awake()
        {
            Initialize();
            MasterItem.EventObjectPickup += EnableThis;
            MasterItem.EventObjectThrow += DisableThis;
            IsStartingItem();
        }

        private void OnDestroy()
        {
            MasterItem.EventObjectPickup -= EnableThis;
            MasterItem.EventObjectThrow -= DisableThis;
        }

        private void Update()
        {
            if (Time.timeScale <= 0) return;

            if (EventRunUpdate != null)
            {
                EventRunUpdate();
            }
        }

        public void CallActivateAbilityEvent()
        {
            if (EventActivateAbility != null)
            {
                EventActivateAbility();
            }
        }

        public void CallDeactivateAbilityEvent()
        {
            if (EventDeactivateAbility != null)
            {
                EventDeactivateAbility();
            }
        }

        public void CallEffectedObjectEvent(GameObject obj)
        {
            if (EventEffectedObject != null)
            {
                EventEffectedObject(obj);
            }
        }

        void Initialize()
        {
            MasterItem = GetComponent<Item.Item_Master>();
        }

        private void EnableThis()
        {
            enabled = true;
        }

        private void DisableThis()
        {
            enabled = false;
        }

        private void IsStartingItem()
        {
            if (transform.root.CompareTag(GameManager.GameManager_References.Instance.PlayerTag))
            {
                EnableThis();
            }
            else
            {
                DisableThis();
            }
        }
    }
}
