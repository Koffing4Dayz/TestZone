using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class NPC_HoldRangeWeapon : MonoBehaviour
    {
        private NPC_StatePattern npc;
        private Animator myAnimator;
        public Transform rightHand;
        public Transform leftHand;

        private void Start()
        {
            Initialize();
        }

        private void OnAnimatorIK(int layerIndex)
        {
            if (npc.RangeWeapon == null)
            {
                return;
            }

            if (npc.RangeWeapon.activeSelf)
            {
                if (rightHand != null)
                {
                    myAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    myAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    myAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHand.position);
                    myAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHand.rotation);
                }

                if (leftHand != null)
                {
                    myAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                    myAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                    myAnimator.SetIKPosition(AvatarIKGoal.LeftHand, rightHand.position);
                    myAnimator.SetIKRotation(AvatarIKGoal.LeftHand, rightHand.rotation);
                }
            }
        }

        private void Initialize()
        {
            npc = GetComponent<NPC_StatePattern>();
            myAnimator = GetComponent<Animator>();
        }
    }
}
