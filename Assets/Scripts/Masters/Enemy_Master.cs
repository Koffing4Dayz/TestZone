using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Enemy_Master : MonoBehaviour
    {
        public Transform myTarget;
        public bool IsOnRoute;
        public bool IsNavPaused;

        public delegate void GeneralEventHandler();
        public event GeneralEventHandler EventEnemyDie;
        public event GeneralEventHandler EventEnemyWalking;
        public event GeneralEventHandler EventEnemyReachedNavTarget;
        public event GeneralEventHandler EventEnemyAttack;
        public event GeneralEventHandler EventEnemyLostTarget;
        public event GeneralEventHandler EventEnemyHealthLow;
        public event GeneralEventHandler EventEnemyHealthRecovered;

        public delegate void HealthEventHandler(int amount);
        public event HealthEventHandler EventEnemyHealthIncrease;
        public event HealthEventHandler EventEnemyHealthDeduction;

        public delegate void NavTargetEventHandler(Transform target);
        public event NavTargetEventHandler EventEnemySetNavTarget;

        public void CallEventEnemyDie()
        {
            if (EventEnemyDie != null)
            {
                EventEnemyDie();
            }
        }

        public void CallEventEnemyWalking()
        {
            if (EventEnemyWalking != null)
            {
                EventEnemyWalking();
            }
        }

        public void CallEventEnemyReachedNavTarget()
        {
            if (EventEnemyReachedNavTarget != null)
            {
                EventEnemyReachedNavTarget();
            }
        }

        public void CallEventEnemyAttack()
        {
            if (EventEnemyAttack != null)
            {
                EventEnemyAttack();
            }
        }

        public void CallEventEnemyLostTarget()
        {
            if (EventEnemyLostTarget != null)
            {
                EventEnemyLostTarget();
            }

            myTarget = null;
        }

        public void CallEventEnemyHealthLow()
        {
            if (EventEnemyHealthLow != null)
            {
                EventEnemyHealthLow();
            }
        }
        public void CallEventEnemyHealthRecovered()
        {
            if (EventEnemyHealthRecovered != null)
            {
                EventEnemyHealthRecovered();
            }
        }

        public void CallEventEnemyHealthIncrease(int amount)
        {
            if (EventEnemyHealthIncrease != null)
            {
                EventEnemyHealthIncrease(amount);
            }
        }

        public void CallEventEnemyHealthDeduction(int amount)
        {
            if (EventEnemyHealthDeduction != null)
            {
                EventEnemyHealthDeduction(amount);
            }
        }

        public void CallEventEnemySetNavTarget(Transform target)
        {
            if (EventEnemySetNavTarget != null)
            {
                EventEnemySetNavTarget(target);
            }

            myTarget = target;
        }
    }
}
