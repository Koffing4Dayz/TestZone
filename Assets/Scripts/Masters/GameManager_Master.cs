using UnityEngine;
using System.Collections;

namespace GameManager
{
    public class GameManager_Master : MonoBehaviour
    {
        public delegate void GameManagerEventHandler();
        public event GameManagerEventHandler EventMenuToggle;
        public event GameManagerEventHandler EventInventoryUIToggle;
        public event GameManagerEventHandler EventRestartLevel;
        public event GameManagerEventHandler EventGoToMenuScene;
        public event GameManagerEventHandler EventGameOver;

        public bool isGameOver;
        public bool isInventoryUIOn;
        public bool isMenuOn;

        public void CallEventMenuToggle()
        {
            if (EventMenuToggle != null)
            {
                EventMenuToggle();
            }
        }

        public void CallEventInventoryUIToggle()
        {
            if (EventInventoryUIToggle != null)
            {
                EventInventoryUIToggle();
            }
        }

        public void CallEventRestartLevel()
        {
            if (EventRestartLevel != null)
            {
                EventRestartLevel();
            }
        }

        public void CallEventGoToMenuScene()
        {
            if (EventGoToMenuScene != null)
            {
                EventGoToMenuScene();
            }
        }

        public void CallEventGameOver()
        {
            if (EventGameOver != null)
            {
                if (isGameOver) return;

                isGameOver = true;
                EventGameOver();
            }
        }
    }
}
