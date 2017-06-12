using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gun
{
    public class Gun_MuzzleFlash : MonoBehaviour
    {
        private Gun_Master MasterGun;
        public ParticleSystem MuzzleFlash;

        private void OnEnable()
        {
            Initialize();
            MasterGun.EventPlayerInput += PlayMuzzleFlash;
            MasterGun.EventNpcInput += PlayMuzzleFlashNPC;
        }

        private void OnDisable()
        {
            MasterGun.EventPlayerInput -= PlayMuzzleFlash;
            MasterGun.EventNpcInput -= PlayMuzzleFlashNPC;
        }

        private void Initialize()
        {
            MasterGun = GetComponent<Gun_Master>();
        }

        private void PlayMuzzleFlash()
        {
            if (MuzzleFlash != null)
            {
                MuzzleFlash.Play();
            }
        }

        private void PlayMuzzleFlashNPC(float dummy)
        {
            if (MuzzleFlash != null)
            {
                MuzzleFlash.Play();
            }
        }
    }
}
