using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class NPC_HeadLook : MonoBehaviour
    {
        private NPC_StatePattern npc;
        private Animator myAnimator;

        private void OnEnable()
        {

        }

        private void OnDisable()
        {

        }

        private void Awake()
        {
            Initialize();
        }

        private void OnAnimatorIK(int layerIndex)
        {
            if (myAnimator.enabled)
            {
                if (npc.TargetPursue != null)
                {
                    myAnimator.SetLookAtWeight(1, 0, 0.5f, 0.7f);
                    myAnimator.SetLookAtPosition(npc.TargetPursue.position);
                }
                else
                {
                    myAnimator.SetLookAtWeight(0);
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
