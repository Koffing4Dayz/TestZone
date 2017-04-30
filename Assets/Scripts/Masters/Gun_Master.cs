using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gun
{
    public class Gun_Master : MonoBehaviour
    {
        public bool IsGunLoaded;
        public bool IsReloading;

        public delegate void GeneralEventHandler();
        public event GeneralEventHandler EventPlayerInput;
        public event GeneralEventHandler EventGunNotUsable;
        public event GeneralEventHandler EventRequestReload;
        public event GeneralEventHandler EventRequestGunReset;
        public event GeneralEventHandler EventToggleFireMode;

        public delegate void GunHitEventHandler(Vector3 hitPosition, Transform hitTransform);
        public event GunHitEventHandler EventShotDefult;
        public event GunHitEventHandler EventShotEnemy;

        public delegate void GunAmmoEventHandler(int currentAmmo, int carriedAmmo);
        public event GunAmmoEventHandler EventAmmoChanged;

        public delegate void GunCrosshairEventHandler(float spread);
        public event GunCrosshairEventHandler EventSpreadCaptured;

        public void CallEventPlayerInput()
        {
            if (EventPlayerInput != null)
            {
                EventPlayerInput();
            }
        }

        public void CallEventGunNotUsable()
        {
            if (EventGunNotUsable != null)
            {
                EventGunNotUsable();
            }
        }

        public void CallEventRequestReload()
        {
            if (EventRequestReload != null)
            {
                EventRequestReload();
            }
        }

        public void CallEventRequestGunReset()
        {
            if (EventRequestGunReset != null)
            {
                EventRequestGunReset();
            }
        }

        public void CallEventToggleFireMode()
        {
            if (EventToggleFireMode != null)
            {
                EventToggleFireMode();
            }
        }

        public void CallEventShotDefult(Vector3 hitPosition, Transform hitTransform)
        {
            if (EventShotDefult != null)
            {
                EventShotDefult(hitPosition, hitTransform);
            }
        }

        public void CallEventShotEnemy(Vector3 hitPosition, Transform hitTransform)
        {
            if (EventShotEnemy != null)
            {
                EventShotEnemy(hitPosition, hitTransform);
            }
        }

        public void CallEventAmmoChanged(int currentAmmo, int carriedAmmo)
        {
            if (EventAmmoChanged != null)
            {
                EventAmmoChanged(currentAmmo, carriedAmmo);
            }
        }

        public void CallEventSpreadCaptured(float spread)
        {
            if (EventSpreadCaptured != null)
            {
                EventSpreadCaptured(spread);
            }
        }
    }
}
