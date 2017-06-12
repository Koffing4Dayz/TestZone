using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class NPCState_Follow : NPCState_Interface
    {
        private readonly NPC_StatePattern npc;
        private Collider[] colliders;
        private Vector3 lookAtPoint;
        private Vector3 heading;
        private float dotProd;

        public NPCState_Follow(NPC_StatePattern input)
        {
            npc = input;
        }

        public void UpdateState()
        {
            Look();
            FollowTarget();
        }
        public void ToPatrolState()
        {
            npc.CurrentState = npc.PatrolState;
        }
        public void ToAlertState()
        {
            npc.CurrentState = npc.AlertState;
        }
        public void ToPursueState() { }
        public void ToMeleeAttackState() { }
        public void ToRangeAttackState() { }

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

        private void Look()
        {
            colliders = Physics.OverlapSphere(npc.transform.position, npc.SightRange * 0.33f, npc.EnemyLayers);

            if (colliders.Length > 0)
            {
                AlertStateActions(colliders[0].transform);
                return;
            }

            colliders = Physics.OverlapSphere(npc.transform.position, npc.SightRange * 0.5f, npc.EnemyLayers);

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
                    foreach (string tag in npc.EnemyTags)
                    {
                        if (hit.transform.CompareTag(tag))
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

        private void FollowTarget()
        {
            npc.MeshRendererFlag.material.color = Color.blue;

            if (!npc.myNavMeshAgent.enabled)
            {
                return;
            }

            if (npc.TargetFollow != null)
            {
                npc.myNavMeshAgent.SetDestination(npc.TargetFollow.position);
                KeepWalking();
            }
            else
            {
                ToPatrolState();
            }

            if (HaveIReachedDestination())
            {
                StopWalking();
            }
        }
    }
}
