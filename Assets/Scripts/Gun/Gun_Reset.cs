using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gun
{
    public class Gun_Reset : MonoBehaviour
    {
        private Gun_Master MasterGun;
        private Item.Item_Master MasterItem;

        private void OnEnable()
        {
            Initialize();
            if (MasterItem != null)
            {
                MasterItem.EventObjectThrow += ResetGun;
            }
        }

        private void OnDisable()
        {
            if (MasterItem != null)
            {
                MasterItem.EventObjectThrow -= ResetGun;
            }
        }

        private void Initialize()
        {
            MasterGun = GetComponent<Gun_Master>();
            MasterItem = GetComponent<Item.Item_Master>();
        }

        void ResetGun()
        {
            MasterGun.CallEventRequestGunReset();
        }
    }
}
