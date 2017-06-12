using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    public class NPC_TurnOffNavMeshAgent : MonoBehaviour
    {
        private NPC_Master MasterNPC;
        private NavMeshAgent myNavMeshAgent;

        private void OnEnable()
        {
            Initialize();
            MasterNPC.EventNpcDie += TurnOffNavMeshAgent;
        }

        private void OnDisable()
        {
            MasterNPC.EventNpcDie -= TurnOffNavMeshAgent;
        }

        private void Initialize()
        {
            MasterNPC = GetComponent<NPC_Master>();
            myNavMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void TurnOffNavMeshAgent()
        {
            if (myNavMeshAgent != null)
            {
                myNavMeshAgent.enabled = false;
            }
        }
    }
}
