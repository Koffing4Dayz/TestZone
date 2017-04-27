using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Enemy_RagdollActivation : MonoBehaviour
    {
        private Enemy_Master MasterEnemy;
        private Collider myCollider;
        private Rigidbody myRigidbody;

        private void OnEnable()
        {
            Initialize();
            MasterEnemy.EventEnemyDie += ActivateRagdoll;
        }

        private void OnDisable()
        {
            MasterEnemy.EventEnemyDie -= ActivateRagdoll;
        }

        private void Initialize()
        {
            MasterEnemy = transform.root.GetComponent<Enemy_Master>();
            myCollider = GetComponent<Collider>();
            myRigidbody = GetComponent<Rigidbody>();
        }

        private void ActivateRagdoll()
        {
            if (myRigidbody != null)
            {
                myRigidbody.isKinematic = false;
                myRigidbody.useGravity = true;
            }

            if (myCollider != null)
            {
                myCollider.isTrigger = false;
                myCollider.enabled = true;
            }
        }
    }
}
