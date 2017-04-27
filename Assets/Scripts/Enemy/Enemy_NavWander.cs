using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class Enemy_NavWander : MonoBehaviour
    {
        private Enemy_Master MasterEnemy;
        private NavMeshAgent myNavMeshAgent;
        private float checkRate;
        private float nextCheck;
        public float WanderRange = 10;
        private Transform myTransform;
        private NavMeshHit navHit;
        private Vector3 wanderTarget;

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
                CheckIfShouldWander();
            }
        }

        private void Initialize()
        {
            MasterEnemy = GetComponent<Enemy_Master>();
            myNavMeshAgent = GetComponent<NavMeshAgent>();
            checkRate = Random.Range(0.3f, 0.4f);
            myTransform = transform;
        }

        private void CheckIfShouldWander()
        {
            if (MasterEnemy.myTarget == null && !MasterEnemy.IsOnRoute && !MasterEnemy.IsNavPaused)
            {
                if (RandomWander(myTransform.position, WanderRange, out wanderTarget))
                {
                    myNavMeshAgent.SetDestination(wanderTarget);
                    MasterEnemy.IsOnRoute = true;
                    MasterEnemy.CallEventEnemyWalking();
                }
            }
        }

        private bool RandomWander(Vector3 center, float range, out Vector3 result)
        {
            Vector3 randPoint = center + Random.insideUnitSphere * range;
            if (NavMesh.SamplePosition(randPoint, out navHit, 1.0f, NavMesh.AllAreas))
            {
                result = navHit.position;
                return true;
            }
            else
            {
                result = center;
                return false;
            }
        }

        private void DisableThis()
        {
            this.enabled = false;
        }
    }
}
