using UnityEngine;
using System.Collections;

namespace GameManager
{
    public class GameManager_ToggleCursor : MonoBehaviour
    {
        bool isCursorLocked = true;

        void OnEnable()
        {
            GameManager.GameManager_References.Instance.MasterGameManager.EventMenuToggle += ToggleCursorState;
            GameManager.GameManager_References.Instance.MasterGameManager.EventInventoryUIToggle += ToggleCursorState;
        }

        void OnDisable()
        {
            GameManager.GameManager_References.Instance.MasterGameManager.EventMenuToggle -= ToggleCursorState;
            GameManager.GameManager_References.Instance.MasterGameManager.EventInventoryUIToggle -= ToggleCursorState;
        }
        
        void Update()
        {
            CheckIfCursorLocked();
        }

        void ToggleCursorState()
        {
            isCursorLocked = !isCursorLocked;
        }

        void CheckIfCursorLocked()
        {
            if (isCursorLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
