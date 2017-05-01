using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crosshair
{
    public class Crosshair_HitMark : MonoBehaviour
    {
        private Crosshair_Master MasterCrosshair;
        Transform playTransform;
        public float HitMarkVolume;
        public AudioClip HitMarkSound;

        private void OnEnable()
        {
            Initialize();
            MasterCrosshair.myGun.EventShotEnemy += ActivateHitMark;
        }

        private void OnDisable()
        {
            MasterCrosshair.myGun.EventShotEnemy -= ActivateHitMark;
        }

        private void Initialize()
        {
            MasterCrosshair = GetComponent<Crosshair_Master>();
            playTransform = MasterCrosshair.myGun.transform;
        }

        private void ActivateHitMark(Vector3 hitPosition, Transform hitTransform)
        {
            MasterCrosshair.CallEventActivateHitMarker();

            if (HitMarkSound != null)
            {
                AudioSource.PlayClipAtPoint(HitMarkSound, playTransform.position, HitMarkVolume);
            }
        }
    }
}
