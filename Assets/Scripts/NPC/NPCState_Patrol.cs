using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    public class NPCState_Patrol : NPCState_Interface
    {
        private readonly NPC_StatePattern npc;
        private int nextWayPoint;
        private Collider[] colliders;
        private Vector3 lookAtPoint;
        private Vector3 heading;
        private float dotProd;

        public NPCState_Patrol(NPC_StatePattern input)
        {
            npc = input;
        }

        public void UpdateState()
        {
            Look();
            Patrol();
        }
        public void ToPatrolState() { }
        public void ToAlertState()
        {
            npc.CurrentState = npc.AlertState;
        }
        public void ToPursueState() { }
        public void ToMeleeAttackState() { }
        public void ToRangeAttackState() { }

        private void Look()
        {
            colliders = Physics.OverlapSphere(npc.transform.position, npc.SightRange / 3, npc.EnemyLayers);

            if (colliders.Length > 0)
            {
                VisibilityCalc(colliders[0].transform);

                if (dotProd > 0)
                {
                    AlertStateActions(colliders[0].transform);
                    return;
                }
            }

            colliders = Physics.OverlapSphere(npc.transform.position, npc.SightRange, npc.EnemyLayers);

            foreach (Collider col in colliders)
            {
                RaycastHit hit;

                VisibilityCalc(col.transform);

                if (Physics.Linecast(npc.Head.position, lookAtPoint, out hit, npc.SightLayers))
                {
                    foreach (string tags in npc.EnemyTags)
                    {
                        if (hit.transform.CompareTag(tags))
                        {
                            if (dotProd > 0)
                            {
                                AlertStateActions(col.transform);
                                return;
                            }
                        }
                    }
                }
            }
        }

        private void Patrol()
        {
            npc.MeshRendererFlag.material.color = Color.green;

            if (npc.TargetFollow != null)
            {
                npc.CurrentState = npc.FollowState;
            }

            if (!npc.myNavMeshAgent.enabled)
            {
                return;
            }

            if (npc.Waypoints.Length > 0)
            {
                MoveTo(npc.Waypoints[nextWayPoint].position);

                if (HaveIReachedDestination())
                {
                    nextWayPoint = (nextWayPoint + 1) % npc.Waypoints.Length;
                }
            }
            else
            {
                if (HaveIReachedDestination())
                {
                    StopWalking();

                    if (RandomWanderTarget(npc.transform.position, npc.SightRange, out npc.TargetWander))
                    {
                        MoveTo(npc.TargetWander);
                    }
                }
            }
        }

        private void AlertStateActions(Transform target)
        {
            npc.LocationOfInterest = target.position;
            ToAlertState();
        }

        private void VisibilityCalc(Transform target)
        {
            lookAtPoint = new Vector3(target.position.x, target.position.y + npc.Offset, target.position.z);
            heading = lookAtPoint - npc.transform.position;
            dotProd = Vector3.Dot(heading, npc.transform.forward);
        }

        private bool RandomWanderTarget(Vector3 center, float range, out Vector3 result)
        {
            NavMeshHit navHit;

            Vector3 randPoint = center + Random.insideUnitSphere * npc.SightRange;
            if (NavMesh.SamplePosition(randPoint, out navHit, 3.0f, NavMesh.AllAreas))
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

        private bool HaveIReachedDestination()
        {
            if (npc.myNavMeshAgent.remainingDistance <= npc.myNavMeshAgent.stoppingDistance && !npc.myNavMeshAgent.pathPending)
            {
                StopWalking();
                return true;
            }
            else
            {
                KeepWalking();
                return false;
            }
        }

        private void MoveTo(Vector3 targetPos)
        {
            if (Vector3.Distance(npc.transform.position, targetPos) > npc.myNavMeshAgent.stoppingDistance + 1)
            {
                npc.myNavMeshAgent.SetDestination(targetPos);
                KeepWalking();
            }
        }

        private void KeepWalking()
        {
            npc.myNavMeshAgent.isStopped = false;
            npc.MasterNPC.CallEventNpcWalkAnim();
        }

        private void StopWalking()
        {
            npc.myNavMeshAgent.isStopped = true;
            npc.MasterNPC.CallEventNpcIdleAnim();
        }
    }
}
