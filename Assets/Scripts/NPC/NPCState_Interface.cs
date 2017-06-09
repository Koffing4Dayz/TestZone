using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public interface NPCState_Interface
    {
        void UpdateState();
        void ToPatrolState();
        void ToAlertState();
        void ToPursueState();
        void ToMeleeAttackState();
        void ToRangeAttackState();
    }
}
