using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crosshair
{
    public class Crosshair_ApplyHit : MonoBehaviour
    {
        public Crosshair_Master MasterCrosshair;

        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            MasterCrosshair.EventActivateHitMarker += ApplyHit;
        }

        private void OnDisable()
        {
            MasterCrosshair.EventActivateHitMarker -= ApplyHit;
        }

        private void Initialize()
        {
            MasterCrosshair = GetComponent<Crosshair_Master>();
        }

        private void ApplyHit()
        {

        }
    }
}
