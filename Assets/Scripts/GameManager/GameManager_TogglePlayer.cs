using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

namespace GameManager
{
    public class GameManager_TogglePlayer : MonoBehaviour
    {
        public FirstPersonController playerController;

        void OnEnable()
        {
            GameManager.GameManager_References.Instance.MasterGameManager.EventMenuToggle += TogglePlayerController;
            GameManager.GameManager_References.Instance.MasterGameManager.EventInventoryUIToggle += TogglePlayerController;
        }

        void OnDisable()
        {
            GameManager.GameManager_References.Instance.MasterGameManager.EventMenuToggle -= TogglePlayerController;
            GameManager.GameManager_References.Instance.MasterGameManager.EventInventoryUIToggle -= TogglePlayerController;
        }

        void TogglePlayerController()
        {
            if (playerController != null)
            {
                playerController.enabled = !playerController.enabled;
            }
        }
    }
}
