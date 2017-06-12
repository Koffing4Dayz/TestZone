using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gun
{
    public class Gun_Animator : MonoBehaviour
    {
        private Gun_Master MasterGun;
        private Animator myAnimator;

        private void OnEnable()
        {
            Initialize();
            MasterGun.EventPlayerInput += PlayShootAnimation;
            MasterGun.EventNpcInput += PlayShootAnimationNPC;
        }

        private void OnDisable()
        {
            MasterGun.EventPlayerInput -= PlayShootAnimation;
            MasterGun.EventNpcInput -= PlayShootAnimationNPC;
        }

        private void Initialize()
        {
            MasterGun = GetComponent<Gun_Master>();
            myAnimator = GetComponent<Animator>();
        }

        private void PlayShootAnimation()
        {
            myAnimator.SetTrigger("Shoot");
        }

        private void PlayShootAnimationNPC(float dummy)
        {
            myAnimator.SetTrigger("Shoot");
        }
    }
}
