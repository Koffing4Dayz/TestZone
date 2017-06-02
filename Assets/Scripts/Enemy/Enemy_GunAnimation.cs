using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Enemy_GunAnimation : MonoBehaviour
    {
        private Enemy_Master MasterEnemy;
        private Animator myAnimator;

        private void OnEnable()
        {
            Initialize();
            MasterEnemy.EventEnemyDie += SetAnimToDie;
            MasterEnemy.EventEnemyReachedNavTarget += SetAnimToIdle;
            MasterEnemy.EventEnemyWalking += SetAnimToWalk;
            MasterEnemy.EventEnemyHealthDeduction += SetAnimToStruck;
            MasterEnemy.EventEnemyAim += SetAnimToAim;
            MasterEnemy.EventEnemyLostTarget += SetAnimToStopAim;
        }

        private void OnDisable()
        {
            MasterEnemy.EventEnemyDie -= SetAnimToDie;
            MasterEnemy.EventEnemyReachedNavTarget -= SetAnimToIdle;
            MasterEnemy.EventEnemyWalking -= SetAnimToWalk;
            MasterEnemy.EventEnemyHealthDeduction -= SetAnimToStruck;
            MasterEnemy.EventEnemyAim -= SetAnimToAim;
            MasterEnemy.EventEnemyLostTarget -= SetAnimToStopAim;
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

        private void SetAnimToAim()
        {
            if (myAnimator != null)
            {
                if (myAnimator.enabled)
                {
                    myAnimator.SetBool("IsAiming", true);
                }
            }
        }

        private void SetAnimToStopAim()
        {
            if (myAnimator != null)
            {
                if (myAnimator.enabled)
                {
                    myAnimator.SetBool("IsAiming", false);
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

        private void SetAnimToDie()
        {
            if (myAnimator != null)
            {
                if (myAnimator.enabled)
                {
                    myAnimator.SetFloat("Dead", Random.Range(1, 3));
                }
            }
        }
    }
}
