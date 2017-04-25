using UnityEngine;
using System.Collections;

namespace GameManager
{
    public class GameManager_GameOver : MonoBehaviour
    {
        public GameObject panelGameOver;

        void OnEnable()
        {
            GameManager.GameManager_References.Instance.MasterGameManager.EventGameOver += TurnOnGameOverPanel;
        }

        void OnDisable()
        {
            GameManager.GameManager_References.Instance.MasterGameManager.EventGameOver -= TurnOnGameOverPanel;
        }

        void TurnOnGameOverPanel()
        {
            if(panelGameOver != null)
            {
                panelGameOver.SetActive(true);
            }
        }
    }
}
