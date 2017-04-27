using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Enemy_TakeDamage : MonoBehaviour
    {
        private Enemy_Master MasterEnemy;
        public int DamageMultiplier = 1;
        public bool RemoveCollider;

        private void OnEnable()
        {
            Initialize();
            MasterEnemy.EventEnemyDie += RemoveThis;
        }

        private void OnDisable()
        {
            MasterEnemy.EventEnemyDie -= RemoveThis;
        }

        private void Initialize()
        {
            MasterEnemy = transform.root.GetComponent<Enemy_Master>();
        }

        public void ProcessDamage(int damage)
        {
            int amount = damage * DamageMultiplier;
            MasterEnemy.CallEventEnemyHealthDeduction(amount);
        }

        private void RemoveThis()
        {
            if (RemoveCollider)
            {
                if (GetComponent<Collider>() != null)
                {
                    Destroy(GetComponent<Collider>());
                }

                if (GetComponent<Rigidbody>() != null)
                {
                    Destroy(GetComponent<Rigidbody>());
                }

                Destroy(this);
            }
        }
    }
}
