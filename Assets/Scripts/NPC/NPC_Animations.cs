using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class NPC_Animations : MonoBehaviour
    {
        private NPC_Master MasterNPC;
        private Animator myAnimator;

        private void OnEnable()
        {
            Initialize();
            MasterNPC.EventNpcAttackAnim += ActivateAttackAnim;
            MasterNPC.EventNpcWalkAnim += ActivateWalkAnim;
            MasterNPC.EventNpcIdleAnim += ActivateIdleAnim;
            MasterNPC.EventNpcRecoveredAnim += ActivateRecoverAnim;
            MasterNPC.EventNpcStruckAnim += ActivateStruckAnim;
        }

        private void OnDisable()
        {
            MasterNPC.EventNpcAttackAnim -= ActivateAttackAnim;
            MasterNPC.EventNpcWalkAnim -= ActivateWalkAnim;
            MasterNPC.EventNpcIdleAnim -= ActivateIdleAnim;
            MasterNPC.EventNpcRecoveredAnim -= ActivateRecoverAnim;
            MasterNPC.EventNpcStruckAnim -= ActivateStruckAnim;
        }

        private void Initialize()
        {
            MasterNPC = GetComponent<NPC_Master>();
            myAnimator = GetComponent<Animator>();
        }

        private void ActivateWalkAnim()
        {
            if (myAnimator != null)
            {
                if (myAnimator.enabled)
                {
                    myAnimator.SetBool(MasterNPC.animBoolPursuing, true);
                }
            }
        }

        private void ActivateIdleAnim()
        {
            if (myAnimator != null)
            {
                if (myAnimator.enabled)
                {
                    myAnimator.SetBool(MasterNPC.animBoolPursuing, false);
                }
            }
        }

        private void ActivateAttackAnim()
        {
            if (myAnimator != null)
            {
                if (myAnimator.enabled)
                {
                    myAnimator.SetTrigger(MasterNPC.animTriggerMelee);
                }
            }
        }

        private void ActivateRecoverAnim()
        {
            if (myAnimator != null)
            {
                if (myAnimator.enabled)
                {
                    myAnimator.SetTrigger(MasterNPC.animTriggerRecovered);
                }
            }
        }

        private void ActivateStruckAnim()
        {
            if (myAnimator != null)
            {
                if (myAnimator.enabled)
                {
                    myAnimator.SetTrigger(MasterNPC.animTriggerStruck);
                }
            }
        }
    }
}
