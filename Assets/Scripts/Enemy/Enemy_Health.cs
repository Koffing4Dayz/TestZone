using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Enemy_Health : MonoBehaviour
    {
        private Enemy_Master MasterEnemy;
        public int MaxHealth = 100;
        public int Health = 100;
        public int LowHealth = 25;
        private bool IsLow = false;

        private void OnEnable()
        {
            Initialize();
            MasterEnemy.EventEnemyHealthDeduction += DeductHealth;
            MasterEnemy.EventEnemyHealthIncrease += IncreaseHealth;
        }

        private void OnDisable()
        {
            MasterEnemy.EventEnemyHealthDeduction -= DeductHealth;
            MasterEnemy.EventEnemyHealthIncrease -= IncreaseHealth;
        }

        private void Initialize()
        {
            MasterEnemy = GetComponent<Enemy_Master>();
        }

        private void DeductHealth(int amount)
        {
            Health -= amount;

            if (Health <= 0)
            {
                Health = 0;
                MasterEnemy.CallEventEnemyDie();
                Destroy(gameObject, Random.Range(10, 20));
            }

            CheckHealth();
        }

        private void IncreaseHealth(int amount)
        {
            Health += amount;

            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }

            CheckHealth();
        }

        private void CheckHealth()
        {
            if (Health <= LowHealth && Health > 0)
            {
                MasterEnemy.CallEventEnemyHealthLow();
                IsLow = true;
            }
            else if (IsLow && Health > LowHealth)
            {
                MasterEnemy.CallEventEnemyHealthRecovered();
                IsLow = false;
            }
        }
    }
}
