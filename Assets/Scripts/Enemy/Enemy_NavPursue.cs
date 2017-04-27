using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class Enemy_NavPursue : MonoBehaviour
    {
        private Enemy_Master MasterEnemy;
        private NavMeshAgent myNavMeshAgent;
        private float checkRate;
        private float nextCheck;

        private void OnEnable()
        {
            Initialize();
            MasterEnemy.EventEnemyDie += DisableThis;
        }

        private void OnDisable()
        {
            MasterEnemy.EventEnemyDie -= DisableThis;
        }

        private void Update()
        {
            if (Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;
                TryToChase();
            }
        }

        private void Initialize()
        {
            MasterEnemy = GetComponent<Enemy_Master>();
            myNavMeshAgent = GetComponent<NavMeshAgent>();
            checkRate = Random.Range(0.1f, 0.2f);
        }

        private void TryToChase()
        {
            if (MasterEnemy.myTarget != null && myNavMeshAgent != null && !MasterEnemy.IsNavPaused)
            {
                myNavMeshAgent.SetDestination(MasterEnemy.myTarget.position);
                if (myNavMeshAgent.remainingDistance > myNavMeshAgent.stoppingDistance)
                {
                    MasterEnemy.CallEventEnemyWalking();
                    MasterEnemy.IsOnRoute = true;
                }
            }
        }

        private void DisableThis()
        {
            if (myNavMeshAgent != null)
            {
                myNavMeshAgent.enabled = false;
            }

            this.enabled = false;
        }
    }
}
