using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class NPC_Master : MonoBehaviour
    {
        public delegate void GeneralEventHandler();
        public event GeneralEventHandler UpdateLoop;
        public event GeneralEventHandler EventNpcDie;
        public event GeneralEventHandler EventNpcLowHealth;
        public event GeneralEventHandler EventNpcHealthRecovered;
        public event GeneralEventHandler EventNpcWalkAnim;
        public event GeneralEventHandler EventNpcStruckAnim;
        public event GeneralEventHandler EventNpcRecoveredAnim;
        public event GeneralEventHandler EventNpcIdleAnim;
        public event GeneralEventHandler EventNpcAttackAnim;

        public delegate void HealthEventHandler(int health);
        public event HealthEventHandler EventNpcDeductHealth;
        public event HealthEventHandler EventNpcIncreaseHealth;

        public string animBoolPursuing = "IsPursuing";
        public string animTriggerStruck = "Struck";
        public string animTriggerMelee = "Attack";
        public string animTriggerRecovered = "Recovered";

        private void Update()
        {
            if (UpdateLoop != null)
            {
                UpdateLoop();
            }
        }

        public void CallEventNpcDie()
        {
            if (EventNpcDie != null)
            {
                EventNpcDie();
            }
        }

        public void CallEventNpcLowHealth()
        {
            if (EventNpcLowHealth != null)
            {
                EventNpcLowHealth();
            }
        }

        public void CallEventNpcHealthRecovered()
        {
            if (EventNpcHealthRecovered != null)
            {
                EventNpcHealthRecovered();
            }
        }

        public void CallEventNpcWalkAnim()
        {
            if (EventNpcWalkAnim != null)
            {
                EventNpcWalkAnim();
            }
        }

        public void CallEventNpcStruckAnim()
        {
            if (EventNpcStruckAnim != null)
            {
                EventNpcStruckAnim();
            }
        }

        public void CallEventNpcRecoveredAnim()
        {
            if (EventNpcRecoveredAnim != null)
            {
                EventNpcRecoveredAnim();
            }
        }

        public void CallEventNpcIdleAnim()
        {
            if (EventNpcIdleAnim != null)
            {
                EventNpcIdleAnim();
            }
        }

        public void CallEventNpcAttackAnim()
        {
            if (EventNpcAttackAnim != null)
            {
                EventNpcAttackAnim();
            }
        }

        public void CallEventNpcDeductHealth(int health)
        {
            if (EventNpcDeductHealth != null)
            {
                EventNpcDeductHealth(health);
            }
        }

        public void CallEventNpcIncreaseHealth(int health)
        {
            if (EventNpcIncreaseHealth != null)
            {
                EventNpcIncreaseHealth(health);
            }
        }
    }
}
