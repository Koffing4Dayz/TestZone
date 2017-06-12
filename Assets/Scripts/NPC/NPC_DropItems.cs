using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class NPC_DropItems : MonoBehaviour
    {
        private NPC_Master MasterNPC;
        public GameObject[] itemsToDrop;

        private void OnEnable()
        {
            MasterNPC.EventNpcDie += DropItems;
        }

        private void OnDisable()
        {
            MasterNPC.EventNpcDie -= DropItems;
        }

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            MasterNPC = GetComponent<NPC_Master>();
        }

        private void DropItems()
        {
            if (itemsToDrop.Length > 0)
            {
                foreach (GameObject item in itemsToDrop)
                {
                    StartCoroutine(PauseBeforeDrop(item));
                }
            }
        }

        private IEnumerator PauseBeforeDrop(GameObject itemToDrop)
        {
            yield return new WaitForSeconds(0.05f);
            itemToDrop.SetActive(true);
            itemToDrop.transform.parent = null;
            yield return new WaitForSeconds(0.05f);
            if (itemToDrop.GetComponent<Item.Item_Master>() != null)
            {
                itemToDrop.GetComponent<Item.Item_Master>().CallEventObjectThrow();
            }
        }
    }
}
