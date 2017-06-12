using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class NPCState_InvestigateHarm : NPCState_Interface
    {
        private readonly NPC_StatePattern npc;
        private float offset = 0.3f;
        private RaycastHit hit;
        private Vector3 lookAtTarget;

        public NPCState_InvestigateHarm(NPC_StatePattern input)
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
        public void ToAlertState()
        {
            npc.CurrentState = npc.AlertState;
        }
        public void ToPursueState()
        {
            npc.CurrentState = npc.PursueState;
        }
        public void ToMeleeAttackState() { }
        public void ToRangeAttackState() { }

        private void GoToLocationOfIntrest()
        {
            npc.MeshRendererFlag.material.color = Color.black;

            if (npc.myNavMeshAgent.enabled && npc.LocationOfInterest != Vector3.zero)
            {
                npc.myNavMeshAgent.SetDestination(npc.LocationOfInterest);
                npc.myNavMeshAgent.isStopped = false;
                npc.MasterNPC.CallEventNpcWalkAnim();

                if (npc.myNavMeshAgent.remainingDistance <= npc.myNavMeshAgent.stoppingDistance)
                {
                    npc.LocationOfInterest = Vector3.zero;
                    ToPatrolState();
                }
            }
            else
            {
                ToPatrolState();
            }
        }

        private void CheckSight()
        {
            lookAtTarget = new Vector3(npc.TargetPursue.position.x, npc.TargetPursue.position.y + offset, npc.TargetPursue.position.z);

            if (Physics.Linecast(npc.Head.position, lookAtTarget, out hit, npc.SightLayers))
            {
                if (hit.transform.root == npc.TargetPursue)
                {
                    npc.LocationOfInterest = npc.TargetPursue.position;
                    GoToLocationOfIntrest();

                    if (Vector3.Distance(npc.transform.position, lookAtTarget) <= npc.SightRange)
                    {
                        ToPursueState();
                    }
                }
                else
                {
                    ToAlertState();
                }
            }
            else
            {
                ToAlertState();
            }
        }

        private void Look()
        {
            if (npc.TargetPursue == null)
            {
                ToPatrolState();
                return;
            }

            CheckSight();
        }
    }
}
