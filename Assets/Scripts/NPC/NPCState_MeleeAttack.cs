using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class NPCState_MeleeAttack : NPCState_Interface
    {
        private readonly NPC_StatePattern npc;
        private float distToTarg;

        public NPCState_MeleeAttack(NPC_StatePattern input)
        {
            npc = input;
        }

        public void UpdateState()
        {
            Look();
            TryToAttack();
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
        public void ToPursueState()
        {
            KeepWalking();
            npc.CurrentState = npc.PursueState;
        }
        public void ToMeleeAttackState() { }
        public void ToRangeAttackState() { }

        private void Look()
        {
            if (npc.TargetPursue != null)
            {
                ToPatrolState();
                return;
            }

            Collider[] colliders = Physics.OverlapSphere(npc.transform.position, npc.MeleeAttackRange, npc.EnemyLayers);

            if (colliders.Length == 0)
            {
                //npc.TargetPursue = null;
                //ToPatrolState();
                ToPursueState();
                return;
            }

            foreach (Collider col in colliders)
            {
                if (col.transform.root == npc.TargetPursue)
                {
                    return;
                }
            }

            //npc.TargetPursue = null;
            //ToPatrolState();
            ToPursueState();
        }

        private void TryToAttack()
        {
            if (npc.TargetPursue != null)
            {
                npc.MeshRendererFlag.material.color = Color.magenta;

                if (Time.time > npc.NextAttack && !npc.isMeleeAttacking)
                {
                    npc.NextAttack = Time.deltaTime + npc.AttackRate;

                    if (Vector3.Distance(npc.transform.position, npc.TargetPursue.position) <= npc.MeleeAttackRange)
                    {
                        Vector3 newPos = new Vector3(npc.TargetPursue.position.x, npc.TargetPursue.position.y, npc.TargetPursue.position.z);
                        npc.transform.LookAt(newPos);
                        npc.MasterNPC.CallEventNpcAttackAnim();
                        npc.isMeleeAttacking = true;
                    }
                    else
                    {
                        ToPursueState();
                    }
                }
            }
            else
            {
                //ToPatrolState();
                ToPursueState();
            }
        }

        private void KeepWalking()
        {
            npc.myNavMeshAgent.isStopped = false;
            npc.MasterNPC.CallEventNpcWalkAnim();
        }
    }
}
