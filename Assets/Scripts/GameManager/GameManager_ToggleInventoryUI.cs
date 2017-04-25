using UnityEngine;
using System.Collections;

namespace GameManager
{
    public class GameManager_ToggleInventoryUI : MonoBehaviour
    {
        public bool HasInventory;
        public GameObject InventoryUI;
        public string KeyBind;
        
        void Start()
        {
            Initialize();
        }
        
        void Update()
        {
            CheckForInventoryUIToggleRequest();
        }

        void Initialize()
        {
            if (KeyBind == "")
            {
                Debug.LogWarning("[GameManager_ToggleInventoryUI] - KeyBind is not set");
                this.enabled = false;
            }
        }

        void CheckForInventoryUIToggleRequest()
        {
            if (Input.GetButtonUp(KeyBind) && !GameManager.GameManager_References.Instance.MasterGameManager.isMenuOn
                && !GameManager.GameManager_References.Instance.MasterGameManager.isGameOver && HasInventory)
            {
                ToggleInventoryUI();
            }
        }

        public void ToggleInventoryUI()
        {
            if (InventoryUI != null)
            {
                InventoryUI.SetActive(!InventoryUI.activeSelf);
                GameManager.GameManager_References.Instance.MasterGameManager.isInventoryUIOn = !GameManager.GameManager_References.Instance.MasterGameManager.isInventoryUIOn;
                GameManager.GameManager_References.Instance.MasterGameManager.CallEventInventoryUIToggle();
            }
        }
    }
}
