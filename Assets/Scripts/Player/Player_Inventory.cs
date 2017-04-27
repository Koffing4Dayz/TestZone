using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Player
{
    public class Player_Inventory : MonoBehaviour
    {
        public Transform inventoryPlayerParent;
        public Transform inventoryUIParent;
        public GameObject uiButton;
        public float placeDelay = 0.1f;
        public Vector3 HandOffset;

        private Player_Master MasterPlayer;
        private GameManager.GameManager_ToggleInventoryUI inventoryUIScript;
        private Transform activeItem;
        private int counter;
        private string buttonText;
        private List<Transform> listInventory = new List<Transform>();

        private void OnEnable()
        {
            Initialize();
            UpdateInventoryListAndUI();
            CheckIfHandsEmpty();

            MasterPlayer.EventInventoryChanged += UpdateInventoryListAndUI;
            MasterPlayer.EventInventoryChanged += CheckIfHandsEmpty;
            MasterPlayer.EventHandsEmpty += ClearHands;
        }

        private void OnDisable()
        {
            MasterPlayer.EventInventoryChanged -= UpdateInventoryListAndUI;
            MasterPlayer.EventInventoryChanged -= CheckIfHandsEmpty;
            MasterPlayer.EventHandsEmpty -= ClearHands;
        }

        private void Initialize()
        {
            inventoryUIScript = GameObject.Find("GameManager").GetComponent<GameManager.GameManager_ToggleInventoryUI>();
            MasterPlayer = GetComponent<Player_Master>();
        }

        private void UpdateInventoryListAndUI()
        {
            counter = 0;
            listInventory.Clear();
            listInventory.TrimExcess();

            ClearInventoryUI();

            foreach (Transform child in inventoryPlayerParent)
            {
                if (child.CompareTag("Item"))
                {
                    listInventory.Add(child);
                    GameObject go = Instantiate(uiButton) as GameObject;
                    buttonText = child.name;
                    go.GetComponentInChildren<Text>().text = buttonText;
                    int index = counter;
                    go.GetComponent<Button>().onClick.AddListener(delegate { ActivateInventoryItem(index); });
                    go.GetComponent<Button>().onClick.AddListener(inventoryUIScript.ToggleInventoryUI);
                    go.transform.SetParent(inventoryUIParent, false);
                    counter++;
                }
            }
        }

        public void ActivateInventoryItem(int inventoryIndex)
        {
            DeactivateAllInventoryItems();
            StartCoroutine(PlaceItemInHands(listInventory[inventoryIndex]));
        }

        private void DeactivateAllInventoryItems()
        {
            foreach (Transform child in inventoryPlayerParent)
            {
                if (child.CompareTag("Item"))
                {
                    child.gameObject.SetActive(false);
                }
            }
        }

        private void ClearInventoryUI()
        {
            foreach (Transform child in inventoryUIParent)
            {
                Destroy(child.gameObject);
            }
        }

        private void CheckIfHandsEmpty()
        {
            if (activeItem == null && listInventory.Count > 0)
            {
                StartCoroutine(PlaceItemInHands(listInventory[0]));
            }
        }

        private void ClearHands()
        {
            activeItem = null;
        }

        private IEnumerator PlaceItemInHands(Transform itemTransform)
        {
            yield return new WaitForSeconds(placeDelay);
            activeItem = itemTransform;
            activeItem.gameObject.SetActive(true);
        }
    }
}