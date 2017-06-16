using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class NPCState_Pursue : NPCState_Interface
    {
        private readonly NPC_StatePattern npc;
        private float capturedDistance;

        public NPCState_Pursue(NPC_StatePattern input)
        {
            npc = input;
        }

        public void UpdateState()
        {
            Look();
            Pursue();
        }
        public void ToPatrolState()
        {
            KeepWalking();
            npc.CurrentState = npc.PatrolState;
        }
        public void ToAlertState()
        {
            KeepWalking();
            npc.CurrentState = npc.AlertState;
        }
        public void ToPursueState() { }
        public void ToMeleeAttackState()
        {
            npc.CurrentState = npc.MeleeAttackState;
        }
        public void ToRangeAttackState()
        {
            npc.CurrentState = npc.RangeAttackState;
        }

        private void Look()
        {
            if (npc.TargetPursue == null)
            {
                ToPatrolState();
                return;
            }

            Collider[] colliders = Physics.OverlapSphere(npc.transform.position, npc.SightRange, npc.EnemyLayers);

            if (colliders.Length == 0)
            {
                npc.TargetPursue = null;
                ToPatrolState();
                return;
            }

            capturedDistance = npc.SightRange * 2;

            foreach (Collider col in colliders)
            {
                float distToTarg = Vector3.Distance(npc.transform.position, col.transform.position);

                if (distToTarg < capturedDistance)
                {
                    capturedDistance = distToTarg;
                    npc.TargetPursue = col.transform.root;
                }
            }
        }

        private void Pursue()
        {
            npc.MeshRendererFlag.material.color = Color.red;

            if (npc.myNavMeshAgent.enabled && npc.TargetPursue != null)
            {
                npc.myNavMeshAgent.SetDestination(npc.TargetPursue.position);
                npc.LocationOfInterest = npc.TargetPursue.position;
                KeepWalking();

                float distToTarg = Vector3.Distance(npc.transform.position, npc.TargetPursue.position);

                if (distToTarg <= npc.RangeAttackRange && distToTarg > npc.MeleeAttackRange)
                {
                    if (npc.canRangeAttack)
                    {
                        ToRangeAttackState();
                    }
                }
                else if (distToTarg <= npc.MeleeAttackRange)
                {
                    if (npc.canMeleeAttack)
                    {
                        ToMeleeAttackState();
                    }
                    else if (npc.canRangeAttack)
                    {
                        ToRangeAttackState();
                    }
                }
                else
                {
                    ToAlertState();
                }
            }
        }

        private void KeepWalking()
        {
            npc.myNavMeshAgent.isStopped = false;
            npc.MasterNPC.CallEventNpcWalkAnim();
        }
    }
}
