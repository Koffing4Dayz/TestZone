using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gadget
{
    public class Gadget_StandardInput : MonoBehaviour
    {
        private Gadget_Master MasterGadget;

        public string KeyBind = "Fire1";

        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            MasterGadget.EventRunUpdate += RunUpdate;
        }

        private void OnDisable()
        {
            MasterGadget.EventRunUpdate -= RunUpdate;
        }

        private void Initialize()
        {
            MasterGadget = GetComponent<Gadget_Master>();
        }

        private void RunUpdate()
        {
            if (Input.GetButtonDown(KeyBind))
            {
                MasterGadget.CallActivateAbilityEvent();
            }
            else if (Input.GetButtonUp(KeyBind))
            {
                MasterGadget.CallDeactivateAbilityEvent();
            }
        }
    }
}
