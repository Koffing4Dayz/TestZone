using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gun
{
    public class Gun_Sounds : MonoBehaviour
    {
        private Gun_Master MasterGun;
        private Transform myTransform;
        public float ShootVolume;
        public AudioClip[] ShootSounds;
        public float ReloadVolume;
        public AudioClip ReloadSound;

        private void OnEnable()
        {
            Initialize();
            MasterGun.EventPlayerInput += PlayShootSound;
            MasterGun.EventNpcInput += PlayShootSoundNPC;
        }

        private void OnDisable()
        {
            MasterGun.EventPlayerInput -= PlayShootSound;
            MasterGun.EventNpcInput -= PlayShootSoundNPC;
        }

        private void Initialize()
        {
            MasterGun = GetComponent<Gun_Master>();
            myTransform = transform;
        }

        private void PlayShootSound()
        {
            if (ShootSounds.Length > 0)
            {
                int index = Random.Range(0, ShootSounds.Length);
                AudioSource.PlayClipAtPoint(ShootSounds[index], myTransform.position, ShootVolume);
            }
        }

        public void PlayReloadSound()
        {
            if (ReloadSound != null)
            {
                AudioSource.PlayClipAtPoint(ReloadSound, myTransform.position, ReloadVolume);
            }
        }

        private void PlayShootSoundNPC(float dummy)
        {
            if (ShootSounds.Length > 0)
            {
                int index = Random.Range(0, ShootSounds.Length);
                AudioSource.PlayClipAtPoint(ShootSounds[index], myTransform.position, ShootVolume);
            }
        }
    }
}
