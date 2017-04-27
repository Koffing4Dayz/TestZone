using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Enemy_Attack : MonoBehaviour
    {
        private Enemy_Master MasterEnemy;
        private Transform myTransform;
        private Transform attackTarget;
        public float AttackRate = 1;
        private float nextAttack;
        public float AttackRange = 3.5f;
        public int AttackDamage = 10;

        private void OnEnable()
        {
            Initialize();
            MasterEnemy.EventEnemyDie += DisableThis;
            MasterEnemy.EventEnemySetNavTarget += SetTarget;
        }

        private void OnDisable()
        {
            MasterEnemy.EventEnemyDie -= DisableThis;
            MasterEnemy.EventEnemySetNavTarget -= SetTarget;
        }

        private void Update()
        {
            TryToAttack();
        }

        private void Initialize()
        {
            MasterEnemy = GetComponent<Enemy_Master>();
            myTransform = transform;
        }

        private void SetTarget(Transform target)
        {
            attackTarget = target;
        }

        private void TryToAttack()
        {
            if (attackTarget != null)
            {
                if (Time.time > nextAttack)
                {
                    nextAttack = Time.time + AttackRate;
                    if (Vector3.Distance(myTransform.position, attackTarget.position) <= AttackRange)
                    {
                        Vector3 lookAt = new Vector3(attackTarget.position.x, myTransform.position.y, attackTarget.position.z);
                        myTransform.LookAt(lookAt);
                        MasterEnemy.CallEventEnemyAttack();
                        MasterEnemy.IsOnRoute = false;
                    }
                }
            }
        }

        public void OnEnemyAttack() // Call on attack animation
        {
            if (attackTarget != null)
            {
                if (Vector3.Distance(myTransform.position, attackTarget.position) <= AttackRange
                    && attackTarget.GetComponent<Player.Player_Master>() != null)
                {
                    Vector3 toOther = attackTarget.position - myTransform.position;
                    if (Vector3.Dot(toOther, myTransform.forward) > 0.5f)
                    {
                        attackTarget.GetComponent<Player.Player_Master>().CallEventPlayerHealthDeduction(AttackDamage);
                    }
                }
            }
        }

        void DisableThis()
        {
            this.enabled = false;
        }
    }
}
