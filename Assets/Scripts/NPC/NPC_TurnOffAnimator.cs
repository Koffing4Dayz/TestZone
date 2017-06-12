using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class NPC_TurnOffAnimator : MonoBehaviour
    {
        private NPC_Master MasterNPC;
        private Animator myAnimator;

        private void OnEnable()
        {
            Initialize();
            MasterNPC.EventNpcDie += TurnOffAnimator;
        }

        private void OnDisable()
        {
            MasterNPC.EventNpcDie -= TurnOffAnimator;
        }

        private void Initialize()
        {
            MasterNPC = GetComponent<NPC_Master>();
            myAnimator = GetComponent<Animator>();
        }

        private void TurnOffAnimator()
        {
            if (myAnimator != null)
            {
                myAnimator.enabled = false;
            }
        }
    }
}
