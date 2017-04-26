using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManager
{
    public class GameManager_Debug : MonoBehaviour
    {
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
        }
    }
}
