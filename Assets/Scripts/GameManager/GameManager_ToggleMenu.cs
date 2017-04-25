using UnityEngine;
using System.Collections;

namespace GameManager
{
    public class GameManager_ToggleMenu : MonoBehaviour
    {
        public GameObject menu;
        public string KeyBind;
        
        void Start()
        {
            ToggleMenu();
        }
        
        void Update()
        {
            CheckForMenuToggleRequest();
        }

        void OnEnable()
        {
            GameManager.GameManager_References.Instance.MasterGameManager.EventGameOver += ToggleMenu;
        }

        void OnDisable()
        {
            GameManager.GameManager_References.Instance.MasterGameManager.EventGameOver -= ToggleMenu;
        }

        void CheckForMenuToggleRequest()
        {
            if (Input.GetKeyUp(KeyCode.Escape) && !GameManager.GameManager_References.Instance.MasterGameManager.isGameOver && !GameManager.GameManager_References.Instance.MasterGameManager.isInventoryUIOn)
            {
                ToggleMenu();
            }
        }

        void ToggleMenu()
        {
            if (menu != null)
            {
                menu.SetActive(!menu.activeSelf);
                GameManager.GameManager_References.Instance.MasterGameManager.isMenuOn = !GameManager.GameManager_References.Instance.MasterGameManager.isMenuOn;
                GameManager.GameManager_References.Instance.MasterGameManager.CallEventMenuToggle();
            }
            else
            {
                Debug.LogWarning("[GameManager_ToggleMenu] - must add a menu to toggle");
            }
        }
    }
}
