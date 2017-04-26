using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Player
{
    public class Player_Health : MonoBehaviour
    {
        private Player_Master MasterPlayer;
        public int PlayerHealth;
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

        void DeductHealth(int healthChange)
        {
            PlayerHealth -= healthChange;

            if (PlayerHealth <= 0)
            {
                PlayerHealth = 0;
                GameManager.GameManager_References.Instance.MasterGameManager.CallEventGameOver();
            }

            SetUI();
        }

        void IncreaseHealth(int healthChange)
        {
            PlayerHealth += healthChange;

            if (PlayerHealth > 100)
            {
                PlayerHealth = 100;
            }

            SetUI();
        }

        void SetUI()
        {
            if (HealthText != null)
            {
                HealthText.text = PlayerHealth.ToString();
            }
        }
    }
}