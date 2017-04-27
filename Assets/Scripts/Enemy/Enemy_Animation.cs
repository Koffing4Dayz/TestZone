using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Enemy_Animation : MonoBehaviour
    {
        private Enemy_Master MasterEnemy;
        private Animator myAnimator;

        private void OnEnable()
        {
            Initialize();
            MasterEnemy.EventEnemyDie += DisableAnimator;
            MasterEnemy.EventEnemyReachedNavTarget += SetAnimToIdle;
            MasterEnemy.EventEnemyWalking += SetAnimToWalk;
            MasterEnemy.EventEnemyAttack += SetAnimToAttack;
            MasterEnemy.EventEnemyHealthDeduction += SetAnimToStruck;
        }

        private void OnDisable()
        {
            MasterEnemy.EventEnemyDie -= DisableAnimator;
            MasterEnemy.EventEnemyReachedNavTarget -= SetAnimToIdle;
            MasterEnemy.EventEnemyWalking -= SetAnimToWalk;
            MasterEnemy.EventEnemyAttack -= SetAnimToAttack;
            MasterEnemy.EventEnemyHealthDeduction -= SetAnimToStruck;
        }

        private void Initialize()
        {
            MasterEnemy = GetComponent<Enemy_Master>();
            myAnimator = GetComponent<Animator>();
        }

        private void SetAnimToIdle()
        {
            if (myAnimator != null)
            {
                if (myAnimator.enabled)
                {
                    myAnimator.SetBool("IsPursuing", false);
                }
            }
        }

        private void SetAnimToWalk()
        {
            if (myAnimator != null)
            {
                if (myAnimator.enabled)
                {
                    myAnimator.SetBool("IsPursuing", true);
                }
            }
        }

        private void SetAnimToAttack()
        {
            if (myAnimator != null)
            {
                if (myAnimator.enabled)
                {
                    myAnimator.SetTrigger("Attack");
                }
            }
        }

        private void SetAnimToStruck(int dummy)
        {
            if (myAnimator != null)
            {
                if (myAnimator.enabled)
                {
                    myAnimator.SetTrigger("Struck");
                }
            }
        }

        private void DisableAnimator()
        {
            if (myAnimator != null)
            {
                myAnimator.enabled = false;
            }
        }
    }
}
