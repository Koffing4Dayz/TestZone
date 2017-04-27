using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class Enemy_NavDestinationReached : MonoBehaviour
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
                CheckIfDestinationReached();
            }
        }

        private void Initialize()
        {
            MasterEnemy = GetComponent<Enemy_Master>();
            myNavMeshAgent = GetComponent<NavMeshAgent>();
            checkRate = Random.Range(0.3f, 0.4f);
        }

        private void CheckIfDestinationReached()
        {
            if (MasterEnemy.IsOnRoute)
            {
                if (myNavMeshAgent.remainingDistance < myNavMeshAgent.stoppingDistance)
                {
                    MasterEnemy.IsOnRoute = false;
                    MasterEnemy.CallEventEnemyReachedNavTarget();
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
