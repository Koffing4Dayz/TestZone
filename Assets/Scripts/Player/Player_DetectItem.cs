using UnityEngine;
using System.Collections;

namespace S3
{
    public class Player_DetectItem : MonoBehaviour
    {
        //rework
        [Tooltip("What layer is being used for items.")]
        public LayerMask layerToDetect;
        [Tooltip("What transform will the ray be fired from?")]
        public Transform rayTransformPivot;
        [Tooltip("The editor input button that will be uses for picking up items.")]
        public string buttonPickup;

        Transform itemAvailableForPickup;
        RaycastHit hit;
        float detectRange = 3;
        float detectRadius = 0.7f;
        bool itemInRange;

        float labelWidth = 200;
        float labelHeight = 50;

        void Update()
        {
            CastRayForDetectingItems();
            CheckForItemPickupAttempt();
        }

        void CastRayForDetectingItems()
        {
            if (Physics.SphereCast(rayTransformPivot.position,detectRadius,rayTransformPivot.forward,out hit,detectRange,layerToDetect))
            {
                itemAvailableForPickup = hit.transform;
                itemInRange = true;
            }
            else
            {
                itemInRange = false;
            }
        }

        void CheckForItemPickupAttempt()
        {
            if (Input.GetButtonDown(buttonPickup) && Time.timeScale > 0 && itemInRange && itemAvailableForPickup.root.tag != GameManager.GameManager_References.Instance.PlayerTag)
            {
                Debug.Log("Pickup attempted");
                //itemAvailableForPickup.GetComponent<Item_Master>().CallEventPickUpAction(rayTransformPivot);
            }
        }

        void OnGUI()
        {
            if (itemInRange && itemAvailableForPickup != null)
            {
                GUI.Label(new Rect(Screen.width / 2 - labelWidth / 2,Screen.height / 2,labelWidth,labelHeight),itemAvailableForPickup.name);
            }
        }
    }
}