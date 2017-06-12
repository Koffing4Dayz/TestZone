using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class NPCState_RangeAttack : NPCState_Interface
    {
        private readonly NPC_StatePattern npc;
        private RaycastHit hit;

        public NPCState_RangeAttack(NPC_StatePattern input)
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
            npc.TargetPursue = null;
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
        public void ToMeleeAttackState()
        {
            npc.CurrentState = npc.MeleeAttackState;
        }
        public void ToRangeAttackState() { }

        private bool IsTargetInSight()
        {
            RaycastHit hit;

            Vector3 weaponLookAtPoint = new Vector3(npc.TargetPursue.position.x, npc.TargetPursue.position.y + npc.Offset, npc.TargetPursue.position.z);
            npc.RangeWeapon.transform.LookAt(weaponLookAtPoint);

            if (Physics.Raycast(npc.RangeWeapon.transform.position, npc.RangeWeapon.transform.forward, out hit))
            {
                foreach (string tag in npc.EnemyTags)
                {
                    if (hit.transform.root.CompareTag(tag))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void Look()
        {
            if (npc.TargetPursue != null)
            {
                ToPatrolState();
                return;
            }

            Collider[] colliders = Physics.OverlapSphere(npc.transform.position, npc.SightRange, npc.EnemyLayers);

            if (colliders.Length == 0)
            {
                ToPatrolState();
                return;
            }

            foreach (Collider col in colliders)
            {
                if (col.transform.root == npc.TargetPursue)
                {
                    return;
                }
            }
            
            ToPatrolState();
        }

        private void TryToAttack()
        {
            if (npc.TargetPursue != null)
            {
                npc.MeshRendererFlag.material.color = Color.cyan;

                if (!IsTargetInSight())
                {
                    ToPursueState();
                    return;
                }

                if (Time.time > npc.NextAttack)
                {
                    npc.NextAttack = Time.time + npc.AttackRate;

                    float distToTarg = Vector3.Distance(npc.transform.position, npc.TargetPursue.position);

                    Vector3 newPos = new Vector3(npc.TargetPursue.position.x, npc.transform.position.y, npc.TargetPursue.position.z);
                    npc.transform.LookAt(newPos);
                    if (distToTarg <= npc.RangeAttackRange)
                    {
                        StopWalking();

                        if (npc.RangeWeapon.GetComponent<Gun.Gun_Master>() != null)
                        {
                            npc.RangeWeapon.GetComponent<Gun.Gun_Master>().CallEventNpcInput(npc.RangeAttackSpread);
                            return;
                        }
                    }

                    if (distToTarg <= npc.MeleeAttackRange && npc.canMeleeAttack)
                    {
                        ToMeleeAttackState();
                    }
                }
            }
            else
            {
                ToPatrolState();
            }
        }

        private void KeepWalking()
        {
            if (npc.myNavMeshAgent.enabled)
            {
                npc.myNavMeshAgent.isStopped = false;
                npc.MasterNPC.CallEventNpcWalkAnim();
            }
        }

        private void StopWalking()
        {
            if (npc.myNavMeshAgent.enabled)
            {
                npc.myNavMeshAgent.isStopped = true;
                npc.MasterNPC.CallEventNpcIdleAnim();
            }
        }
    }
}
