using UnityEngine;
using System.Collections;

namespace Item
{
    public class Item_Throw : MonoBehaviour
    {
        private Item_Master MasterItem;
        private Transform myTransform;
        private Rigidbody myRigidbody;
        private Vector3 throwDirection;

        public bool CanThrow = true;
        public string KeyBind = "Throw";
        public float ThrowForce = 500;

        void Start()
        {
            Initialize();
        }

        void Update()
        {
            CheckForThrowInput();
        }

        void Initialize()
        {
            MasterItem = GetComponent<Item_Master>();
            myTransform = transform;
            myRigidbody = GetComponent<Rigidbody>();
        }

        void CheckForThrowInput()
        {
            if(KeyBind != null)
            {
                if(Input.GetButtonDown(KeyBind) && Time.timeScale > 0 && myTransform.root.CompareTag(GameManager.GameManager_References.Instance.PlayerTag))
                {
                    CarryOutThrowActions();
                }
            }
        }

        void CarryOutThrowActions()
        {
            throwDirection = myTransform.parent.forward;
            myTransform.parent = null;
            MasterItem.CallEventObjectThrow();
            HurlItem();
        }

        void HurlItem()
        {
            myRigidbody.AddForce(throwDirection * ThrowForce,ForceMode.Impulse);
        }
    }
}