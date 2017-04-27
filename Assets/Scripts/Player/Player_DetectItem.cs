using UnityEngine;
using System.Collections;

namespace Player
{
    public class Player_DetectItem : MonoBehaviour
    {
        public LayerMask ItemLayer;
        public Transform RayTransform;
        public string MappedKey;

        private Transform activeItem;
        private RaycastHit hit;
        private bool itemInRange;

        public float DetectRange = 3;
        public float DetectRadius = 0.7f;
        public float LabelWidth = 200;
        public float LabelHeight = 50;

        void Update()
        {
            DetectItems();
            CheckPickupAttempt();
        }

        void DetectItems()
        {
            if (Physics.SphereCast(RayTransform.position, DetectRadius, RayTransform.forward, out hit, DetectRange, ItemLayer))
            {
                activeItem = hit.transform;
                itemInRange = true;
            }
            else
            {
                itemInRange = false;
            }
        }

        void CheckPickupAttempt()
        {
            if (Input.GetButtonDown(MappedKey) && Time.timeScale > 0 && itemInRange && activeItem.root.tag != GameManager.GameManager_References.Instance.PlayerTag)
            {
                //Debug.Log("Pickup attempted");
                activeItem.GetComponent<Item.Item_Master>().CallEventPickupAction(RayTransform);
            }
        }

        void OnGUI()
        {
            if (itemInRange && activeItem != null)
            {
                GUI.Label(new Rect(Screen.width / 2 - LabelWidth / 2,Screen.height / 2,LabelWidth,LabelHeight),activeItem.name);
            }
        }
    }
}