using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class Item_Name : MonoBehaviour
    {
        private Item_Master MasterItem;
        public string Name;

        private void Start()
        {
            transform.name = Name;
        }
    }
}
