using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class Item_Tag : MonoBehaviour
    {
        [Tooltip("Leave empty to use GameManager's Item tag")]
        public string Tag = "";

        private void OnEnable()
        {
            if (Tag == "")
            {
                Tag = GameManager.GameManager_References.Instance.ItemTag;
            }

            if (Tag == "")
            {
                Tag = "Untagged";
            }

            transform.tag = Tag;
        }
    }
}
