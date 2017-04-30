using UnityEngine;
using System.Collections;

namespace Player
{
    public class Player_Master : MonoBehaviour
    {
        public float Speed;

        public delegate void GeneralEventHandler();
        public event GeneralEventHandler EventInventoryChanged;
        public event GeneralEventHandler EventHandsEmpty;
        public event GeneralEventHandler EventAmmoChanged;

        public delegate void AmmoPickupEventHandler(string ammoName,int quantity);
        public event AmmoPickupEventHandler EventPickedUpAmmo;

        public delegate void PlayerHealthEventHandler(int amount);
        public event PlayerHealthEventHandler EventPlayerHealthDeduction;
        public event PlayerHealthEventHandler EventPlayerHealthIncrease;

        public void CallEventInventoryChanged()
        {
            if (EventInventoryChanged != null)
            {
                EventInventoryChanged();
            }
        }

        public void CallEventHandsEmpty()
        {
            if (EventHandsEmpty != null)
            {
                EventHandsEmpty();
            }
        }

        public void CallEventAmmoChanged()
        {
            if (EventAmmoChanged != null)
            {
                EventAmmoChanged();
            }
        }

        public void CallEventPickUpAmmo(string ammoName,int quantity)
        {
            if (EventPickedUpAmmo != null)
            {
                EventPickedUpAmmo(ammoName, quantity);
            }
        }

        public void CallEventPlayerHealthDeduction(int amount)
        {
            if (EventPlayerHealthDeduction != null)
            {
                EventPlayerHealthDeduction(amount);
            }
        }

        public void CallEventPlayerHealthIncrease(int amount)
        {
            if (EventPlayerHealthIncrease != null)
            {
                EventPlayerHealthIncrease(amount);
            }
        }
    }
}