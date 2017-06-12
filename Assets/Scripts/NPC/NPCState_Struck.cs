using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class NPCState_Struck : NPCState_Interface
    {
        private readonly NPC_StatePattern npc;
        private float informRate = 0.5f;
        private float nextInform;
        private Collider[] colliders;
        private Collider[] friendlyColliders;

        public NPCState_Struck(NPC_StatePattern input)
        {
            npc = input;
        }

        public void UpdateState()
        {
            InformAllies();
        }
        public void ToPatrolState() { }
        public void ToAlertState()
        {
            npc.CurrentState = npc.AlertState;
        }
        public void ToPursueState() { }
        public void ToMeleeAttackState() { }
        public void ToRangeAttackState() { }

        private void SetMyselfToInvestigate()
        {
            npc.TargetPursue = npc.Attacker;
            npc.LocationOfInterest = npc.Attacker.position;

            if (npc.CapturedState == npc.PatrolState)
            {
                npc.CapturedState = npc.InvestigateHarmState;
            }
        }
        
        private void AlertNearbyAllies()
        {
            foreach (Collider ally in friendlyColliders)
            {
                if (ally.transform.root.GetComponent<NPC_StatePattern>() != null)
                {
                    NPC_StatePattern allyState = ally.transform.root.GetComponent<NPC_StatePattern>();

                    if (allyState.CurrentState == allyState.PatrolState)
                    {
                        allyState.TargetPursue = npc.Attacker;
                        allyState.LocationOfInterest = npc.Attacker.position;
                        allyState.CurrentState = allyState.InvestigateHarmState;
                        allyState.MasterNPC.CallEventNpcWalkAnim();
                    }
                }
            }
        }

        private void InformAllies()
        {
            if (Time.time > nextInform)
            {
                nextInform = Time.time + informRate;
            }
            else
            {
                return;
            }

            if (npc.Attacker != null)
            {
                friendlyColliders = Physics.OverlapSphere(npc.transform.position, npc.SightRange, npc.FriendlyLayer);

                if (IsAttackerClose())
                {
                    AlertNearbyAllies();
                    SetMyselfToInvestigate();
                }
            }
        }

        private bool IsAttackerClose()
        {
            if (Vector3.Distance(npc.transform.position, npc.Attacker.position) <= npc.SightRange * 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
