using UnityEngine;
using System.Collections;

namespace S3
{
    public class Item_Throw : MonoBehaviour
    {
        Item_Master itemMaster;
        Transform myTransform;
        Rigidbody myRigidbody;
        Vector3 throwDirection;

        public bool canBeThrown;
        public string throwButtonName;
        public float throwForce;

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
            itemMaster = GetComponent<Item_Master>();
            myTransform = transform;
            myRigidbody = GetComponent<Rigidbody>();
        }

        void CheckForThrowInput()
        {
            if(throwButtonName != null)
            {
                if(Input.GetButtonDown(throwButtonName) && Time.timeScale > 0 && myTransform.root.CompareTag(GameManager.GameManager_References.Instance.PlayerTag))
                {
                    CarryOutThrowActions();
                }
            }
        }

        void CarryOutThrowActions()
        {
            throwDirection = myTransform.parent.forward;
            myTransform.parent = null;
            itemMaster.CallEventObjectThrow();
            HurlItem();
        }

        void HurlItem()
        {
            myRigidbody.AddForce(throwDirection * throwForce,ForceMode.Impulse);
        }
    }
}