using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crosshair
{
    public class Crosshair_CaptureSpread : MonoBehaviour
    {
        private Crosshair_Master MasterCrosshair;
        private Gun.Gun_Shoot myGunShoot;
        private float nextCaptureTime;
        public float CaptureInterval = 0.1f;

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            if (myGunShoot != null)
            {
                if (Time.time > nextCaptureTime)
                {
                    nextCaptureTime = Time.time + CaptureInterval;
                    MasterCrosshair.CallEventSpreadCaptured(myGunShoot.GetSpread());
                }
            }
        }

        private void Initialize()
        {
            MasterCrosshair = GetComponent<Crosshair_Master>();
            myGunShoot = MasterCrosshair.myGun.GetComponent<Gun.Gun_Shoot>();
        }
    }
}
