using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class NPCState_Alert : NPCState_Interface
    {
        private readonly NPC_StatePattern npc;
        private float informRate = 3;
        private float nextInform;
        private float offset = 0.3f;
        private Vector3 targetPosition;
        private RaycastHit hit;
        private Collider[] colliders;
        private Collider[] friendlyColliders;
        private Vector3 lookAtTarget;
        private int detectionCount;
        private int lastDetectionCount;
        private Transform possibleTarget;

        public NPCState_Alert(NPC_StatePattern input)
        {
            npc = input;
        }

        public void UpdateState()
        {
            Look();
        }
        public void ToPatrolState()
        {
            npc.CurrentState = npc.PatrolState;
        }
        public void ToAlertState() { }
        public void ToPursueState()
        {
            npc.CurrentState = npc.PursueState;
        }
        public void ToMeleeAttackState() { }
        public void ToRangeAttackState() { }

        private void Look()
        {
            colliders = Physics.OverlapSphere(npc.transform.position, npc.SightRange, npc.EnemyLayers);

            lastDetectionCount = detectionCount;

            foreach (Collider col in colliders)
            {
                lookAtTarget = new Vector3(col.transform.position.x, col.transform.position.y + offset, col.transform.position.z);

                if (Physics.Linecast(npc.Head.position, lookAtTarget, out hit, npc.SightLayers))
                {
                    foreach (string tag in npc.EnemyTags)
                    {
                        if (hit.transform.CompareTag(tag))
                        {
                            detectionCount++;
                            possibleTarget = col.transform;
                            break;
                        }
                    }
                }
            }

            if (detectionCount == lastDetectionCount)
            {
                detectionCount = 0;
            }

            if (detectionCount >= npc.RequiredDetectCount)
            {
                detectionCount = 0;
                npc.LocationOfInterest = possibleTarget.position;
                npc.TargetPursue = possibleTarget.root;
                InformNearbyAllies();
                ToPursueState();
            }

            GoToLocationOfIntrest();
        }

        private void GoToLocationOfIntrest()
        {
            npc.MeshRendererFlag.material.color = Color.yellow;

            if (npc.myNavMeshAgent.enabled && npc.LocationOfInterest != Vector3.zero)
            {
                npc.myNavMeshAgent.SetDestination(npc.LocationOfInterest);
                npc.myNavMeshAgent.isStopped = false;
                npc.MasterNPC.CallEventNpcWalkAnim();

                if (npc.myNavMeshAgent.remainingDistance <= npc.myNavMeshAgent.stoppingDistance && !npc.myNavMeshAgent.pathPending)
                {
                    npc.MasterNPC.CallEventNpcIdleAnim();
                    npc.LocationOfInterest = Vector3.zero;
                    ToPatrolState();
                }
            }
        }

        private void InformNearbyAllies()
        {
            if (Time.time > nextInform)
            {
                nextInform = Time.time + informRate;

                friendlyColliders = Physics.OverlapSphere(npc.transform.position, npc.SightRange, npc.FriendlyLayer);

                if (friendlyColliders.Length == 0)
                {
                    return;
                }

                foreach (Collider ally in friendlyColliders)
                {
                    NPC_StatePattern allyState = ally.transform.root.GetComponent<NPC_StatePattern>();

                    if (allyState != null)
                    {
                        if (allyState.CurrentState == allyState.PatrolState)
                        {
                            allyState.TargetPursue = npc.TargetPursue;
                            allyState.LocationOfInterest = npc.TargetPursue.position;
                            allyState.CurrentState = allyState.AlertState;
                            allyState.MasterNPC.CallEventNpcWalkAnim();
                        }
                    }
                }
            }
        }
    }
}
