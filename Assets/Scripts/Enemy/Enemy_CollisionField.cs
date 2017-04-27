using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Enemy_CollisionField : MonoBehaviour
    {
        private Enemy_Master MasterEnemy;
        private Rigidbody rigidbodyHit;
        private int damage;
        public float MassRequirement = 50;
        public float SpeedRequirement = 5;
        public float DamageFactor = 0.1f;

        private void OnEnable()
        {
            Initialize();
            MasterEnemy.EventEnemyDie += DisableThis;
        }

        private void OnDisable()
        {
            MasterEnemy.EventEnemyDie -= DisableThis;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Rigidbody>() != null)
            {
                rigidbodyHit = other.GetComponent<Rigidbody>();

                if (rigidbodyHit.mass >= MassRequirement
                 && rigidbodyHit.velocity.sqrMagnitude >= SpeedRequirement * SpeedRequirement)
                {
                    damage = (int)(DamageFactor * rigidbodyHit.mass * rigidbodyHit.velocity.magnitude);
                    MasterEnemy.CallEventEnemyHealthDeduction(damage);
                }
            }
        }

        private void Initialize()
        {
            MasterEnemy = transform.root.GetComponent<Enemy_Master>();
        }

        private void DisableThis()
        {
            gameObject.SetActive(false);
        }
    }
}
