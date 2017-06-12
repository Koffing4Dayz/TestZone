using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class NPC_RagdollActivation : MonoBehaviour
    {
        private NPC_Master MasterNPC;
        private Rigidbody myRigidbody;
        private Collider myCollider;

        private void OnEnable()
        {
            MasterNPC.EventNpcDie += ActivateRagdoll;
        }

        private void OnDisable()
        {
            MasterNPC.EventNpcDie -= ActivateRagdoll;
        }

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            MasterNPC = transform.root.GetComponent<NPC_Master>();
            myRigidbody = GetComponent<Rigidbody>();
            myCollider = GetComponent<Collider>();
        }

        private void ActivateRagdoll()
        {
            if (myCollider != null)
            {
                myCollider.enabled = true;
                myCollider.isTrigger = false;
            }

            if (myRigidbody != null)
            {
                myRigidbody.isKinematic = false;
                myRigidbody.useGravity = true;
            }

            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
}
