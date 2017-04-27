using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class Enemy_NavFlee : MonoBehaviour
    {
        private Enemy_Master MasterEnemy;
        private NavMeshAgent myNavMeshAgent;
        private Transform myTransform;
        private NavMeshHit navHit;
        public bool IsFleeing;
        public Transform FleeTarget;
        private Vector3 runPosition;
        private Vector3 dirToPlayer;
        public float FleeRange = 25;
        private float checkRate;
        private float nextCheck;

        private void OnEnable()
        {
            Initialize();
            MasterEnemy.EventEnemyDie += DisableThis;
            MasterEnemy.EventEnemySetNavTarget += SetFleeTarget;
            MasterEnemy.EventEnemyHealthLow += StartFlee;
            MasterEnemy.EventEnemyHealthRecovered += StopFlee;
        }

        private void OnDisable()
        {
            MasterEnemy.EventEnemyDie -= DisableThis;
            MasterEnemy.EventEnemySetNavTarget -= SetFleeTarget;
            MasterEnemy.EventEnemyHealthLow -= StartFlee;
            MasterEnemy.EventEnemyHealthRecovered -= StopFlee;
        }

        private void Update()
        {
            if (Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;
                CheckFlee();
            }
        }

        private void Initialize()
        {
            MasterEnemy = GetComponent<Enemy_Master>();
            myNavMeshAgent = GetComponent<NavMeshAgent>();
            myTransform = transform;
            checkRate = Random.Range(0.3f, 0.4f);
        }

        private void SetFleeTarget(Transform target)
        {
            FleeTarget = target;
        }

        private void StartFlee()
        {
            IsFleeing = true;

            if (GetComponent<Enemy_NavPursue>() != null)
            {
                GetComponent<Enemy_NavPursue>().enabled = false;
            }
        }

        private void StopFlee()
        {
            IsFleeing = false;

            if (GetComponent<Enemy_NavPursue>() != null)
            {
                GetComponent<Enemy_NavPursue>().enabled = true;
            }
        }

        private bool FindFleeTarget(out Vector3 result)
        {
            dirToPlayer = myTransform.position - FleeTarget.position;
            Vector3 checkPos = myTransform.position + dirToPlayer;

            if (NavMesh.SamplePosition(checkPos, out navHit, 1.0f, NavMesh.AllAreas))
            {
                result = navHit.position;
                return true;
            }
            else
            {
                result = myTransform.position;
                return false;
            }
        }

        private void CheckFlee()
        {
            if (IsFleeing)
            {
                if (FleeTarget != null && !MasterEnemy.IsOnRoute && !MasterEnemy.IsNavPaused)
                {
                    if (FindFleeTarget(out runPosition) && Vector3.Distance(myTransform.position, FleeTarget.position) < FleeRange)
                    {
                        myNavMeshAgent.SetDestination(runPosition);
                        MasterEnemy.CallEventEnemyWalking();
                        MasterEnemy.IsOnRoute = true;
                    }
                }
            }
        }

        private void DisableThis()
        {
            this.enabled = false;
        }
    }
}
