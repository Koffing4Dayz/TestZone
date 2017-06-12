using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class NPC_Health : MonoBehaviour
    {
        private NPC_Master MasterNPC;
        public int MaxHealth = 100;
        public int CurrentHealth = 100;
        private bool healthCritical;
        public int LowHealth = 25;

        private void OnEnable()
        {
            Initialize();
            MasterNPC.EventNpcIncreaseHealth += IncreaseHealth;
            MasterNPC.EventNpcDeductHealth += DecreaseHealth;
        }

        private void OnDisable()
        {
            MasterNPC.EventNpcIncreaseHealth -= IncreaseHealth;
            MasterNPC.EventNpcDeductHealth -= DecreaseHealth;
        }

        private void Initialize()
        {
            MasterNPC = GetComponent<NPC_Master>();
        }

        private void CheckLowHealth()
        {
            if (CurrentHealth <= LowHealth && CurrentHealth > 0)
            {
                MasterNPC.CallEventNpcLowHealth();
                healthCritical = true;
            }
            else if (CurrentHealth > LowHealth && healthCritical)
            {
                MasterNPC.CallEventNpcHealthRecovered();
                healthCritical = false;
            }
        }

        private void IncreaseHealth(int amount)
        {
            CurrentHealth += amount;

            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }

            CheckLowHealth();
        }

        private void DecreaseHealth(int amount)
        {
            CurrentHealth -= amount;

            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                MasterNPC.CallEventNpcDie();
                Destroy(gameObject, Random.Range(10, 20));
            }

            CheckLowHealth();
        }
    }
}
