using UnityEngine;
using System.Collections;

namespace GameManager
{
    public class GameManager_TogglePause : MonoBehaviour
    {
        bool isPaused;

        void OnEnable()
        {
            GameManager.GameManager_References.Instance.MasterGameManager.EventMenuToggle += TogglePause;
            GameManager.GameManager_References.Instance.MasterGameManager.EventInventoryUIToggle += TogglePause;
        }

        void OnDisable()
        {
            GameManager.GameManager_References.Instance.MasterGameManager.EventMenuToggle -= TogglePause;
            GameManager.GameManager_References.Instance.MasterGameManager.EventInventoryUIToggle -= TogglePause;
        }

        void TogglePause()
        {
            if (isPaused)
            {
                Time.timeScale = 1;
                isPaused = false;
            }
            else
            {
                Time.timeScale = 0;
                isPaused = true;
            }
        }
    }
}
