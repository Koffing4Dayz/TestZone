using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class NPC_TurnOffStatePattern : MonoBehaviour
    {
        private NPC_Master MasterNPC;
        private NPC_StatePattern myStatePattern;

        private void OnEnable()
        {
            Initialize();
            MasterNPC.EventNpcDie += TurnOffStatePattern;
        }

        private void OnDisable()
        {
            MasterNPC.EventNpcDie -= TurnOffStatePattern;
        }

        private void Initialize()
        {
            MasterNPC = GetComponent<NPC_Master>();
            myStatePattern = GetComponent<NPC_StatePattern>();
        }

        private void TurnOffStatePattern()
        {
            if (myStatePattern != null)
            {
                myStatePattern.enabled = false;
            }
        }
    }
}
