using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Player
{
    public class Player_Health : MonoBehaviour
    {
        private Player_Master MasterPlayer;
        public int MaxHealth = 100;
        public int Health = 100;
        public Text HealthText;

        void OnEnable()
        {
            Initialize();
            SetUI();
            MasterPlayer.EventPlayerHealthDeduction += DeductHealth;
            MasterPlayer.EventPlayerHealthIncrease += IncreaseHealth;
        }

        void OnDisable()
        {
            MasterPlayer.EventPlayerHealthDeduction -= DeductHealth;
            MasterPlayer.EventPlayerHealthIncrease -= IncreaseHealth;

        }

        void Initialize()
        {
            MasterPlayer = GetComponent<Player_Master>();
        }

        void DeductHealth(int amount)
        {
            Health -= amount;

            if (Health <= 0)
            {
                Health = 0;
                GameManager.GameManager_References.Instance.MasterGameManager.CallEventGameOver();
            }

            SetUI();
        }

        void IncreaseHealth(int amount)
        {
            Health += amount;

            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }

            SetUI();
        }

        void SetUI()
        {
            if (HealthText != null)
            {
                HealthText.text = Health.ToString();
            }
        }
    }
}