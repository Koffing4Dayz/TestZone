using UnityEngine;
using System.Collections;

namespace GameManager
{
    public class GameManager_PanelInstructions : MonoBehaviour
    {
        public GameObject panelInstructions;

        void OnEnable()
        {
            GameManager.GameManager_References.Instance.MasterGameManager.EventGameOver += TurnOffPanelInstructions;
        }

        void OnDisable()
        {
            GameManager.GameManager_References.Instance.MasterGameManager.EventGameOver -= TurnOffPanelInstructions;

        }

        void TurnOffPanelInstructions()
        {
            if(panelInstructions != null)
            {
                panelInstructions.SetActive(false);
            }
        }
    }
}