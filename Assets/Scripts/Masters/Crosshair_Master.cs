using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crosshair
{
    public class Crosshair_Master : MonoBehaviour
    {
        public Gun.Gun_Master myGun;

        public delegate void GeneralEventHandler();
        public GeneralEventHandler EventActivateHitMarker;

        public delegate void SpreadEventHandler(float spread);
        public event SpreadEventHandler EventSpreadCaptured;

        public void CallEventActivateHitMarker()
        {
            if (EventActivateHitMarker != null)
            {
                EventActivateHitMarker();
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
