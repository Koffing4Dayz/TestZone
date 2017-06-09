using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    public class NPCState_Flee : NPCState_Interface
    {
        private Vector3 dirToEnemy;
        private NavMeshHit navHit;
        private readonly NPC_StatePattern npc;

        public NPCState_Flee(NPC_StatePattern input)
        {
            npc = input;
        }

        public void UpdateState()
        {
            CheckFlight();
            CheckFight();
        }
        public void ToPatrolState()
        {
            KeepWalking();
            npc.CurrentState = npc.PatrolState;
        }
        public void ToAlertState() { }
        public void ToPursueState() { }
        public void ToMeleeAttackState()
        {
            KeepWalking();
            npc.CurrentState = npc.MeleeAttackState;
        }
        public void ToRangeAttackState()
        {
            KeepWalking();
            npc.CurrentState = npc.RangeAttackState;
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

        private void CheckFlight()
        {
            npc.MeshRendererFlag.material.color = Color.gray;

            Collider[] colliders = Physics.OverlapSphere(npc.transform.position, npc.SightRange, npc.EnemyLayers);

            if (colliders.Length == 0)
            {
                ToPatrolState();
                return;
            }

            dirToEnemy = npc.transform.position - colliders[0].transform.position;
            Vector3 checkPos = npc.transform.position + dirToEnemy;

            if (NavMesh.SamplePosition(checkPos, out navHit, 3.0f, NavMesh.AllAreas))
            {
                npc.myNavMeshAgent.destination = navHit.position;
                KeepWalking();
            }
            else
            {
                StopWalking();
            }
        }

        private void CheckFight()
        {
            if (npc.TargetPursue == null)
            {
                return;
            }

            float distToEnemy = Vector3.Distance(npc.transform.position, npc.TargetPursue.position);

            if (npc.canMeleeAttack && distToEnemy <= npc.MeleeAttackRange)
            {
                ToMeleeAttackState();
            }
        }
    }
}
