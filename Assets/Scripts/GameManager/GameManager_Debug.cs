using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManager
{
    public class GameManager_Debug : MonoBehaviour
    {
        public GameObject alpha;
        public GameObject beta;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                GameManager.GameManager_References.Instance.MasterGameManager.CallEventGameOver();
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                FindObjectOfType<Player.Player_Master>().CallEventPlayerHealthDeduction(10);
            }

            if (Input.GetKeyDown(KeyCode.F3))
            {
                foreach (Enemy.Enemy_Master item in FindObjectsOfType<Enemy.Enemy_Master>())
                {
                    item.CallEventEnemyDie();
                }
            }

            if (Input.GetKeyDown(KeyCode.F4))
            {
                foreach (Enemy.Enemy_Master item in FindObjectsOfType<Enemy.Enemy_Master>())
                {
                    item.CallEventEnemyHealthDeduction(20);
                }
            }

            if (Input.GetKeyDown(KeyCode.F5))
            {
                foreach (Enemy.Enemy_Master item in FindObjectsOfType<Enemy.Enemy_Master>())
                {
                    item.CallEventEnemyHealthIncrease(20);
                }
            }

            if (Input.GetKeyDown(KeyCode.F6))
            {
                alpha.SetActive(true);
                beta.SetActive(true);
            }
        }
    }
}
