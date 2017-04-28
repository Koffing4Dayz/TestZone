using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UI_TrackHealth : MonoBehaviour
    {
        private Enemy.Enemy_Master MasterEnemy;
        public Enemy.Enemy_Health Owner;
        public Image myImage;

        private void OnEnable()
        {
            Initialize();
            if (MasterEnemy != null)
            {
                if (Owner != null)
                {
                    MasterEnemy.EventEnemyHealthIncrease += UpdateUI;
                    MasterEnemy.EventEnemyHealthDeduction += UpdateUI;
                    MasterEnemy.EventEnemyDie += DisableThis;
                }
                else
                {
                    Debug.LogWarning("[UI_TrackHealth] Owner not set");
                }
            }
            else
            {
                Debug.LogWarning("[UI_TrackHealth] No valid Master Enemy");
            }
        }

        private void OnDisable()
        {
            if (MasterEnemy == null) return; 
            MasterEnemy.EventEnemyHealthIncrease -= UpdateUI;
            MasterEnemy.EventEnemyHealthDeduction -= UpdateUI;
            MasterEnemy.EventEnemyDie -= DisableThis;
        }

        private void Initialize()
        {
            if (Owner == null)
            {
                Owner = transform.root.GetComponent<Enemy.Enemy_Health>();
                MasterEnemy = Owner.transform.root.GetComponent<Enemy.Enemy_Master>();
            }
            if (myImage == null)
            {
                myImage = GetComponent<Image>();
            }
            UpdateUI(0);
        }

        private void UpdateUI(int amount)
        {
            myImage.fillAmount = (float)(Owner.Health) / (float)Owner.MaxHealth;
        }

        private void DisableThis()
        {
            gameObject.SetActive(false);
        }
    }
}
