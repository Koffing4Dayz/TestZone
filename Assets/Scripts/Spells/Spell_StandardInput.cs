using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spell
{
    public class Spell_StandardInput : MonoBehaviour
    {
        private Spell_Master MasterSpell;
        public string FireKeyBind = "Fire1";
        public bool IsAutomatic = true;

        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            MasterSpell.EventRunUpdate += RunUpdate;
        }

        private void OnDisable()
        {
            MasterSpell.EventRunUpdate -= RunUpdate;
        }

        private void Initialize()
        {
            MasterSpell = GetComponent<Spell_Master>();
        }

        private void RunUpdate()
        {
            if (IsAutomatic && Input.GetButton(FireKeyBind))
            {
                MasterSpell.CallFireInputEvent();
            }
            else if (Input.GetButtonDown(FireKeyBind))
            {
                MasterSpell.CallFireInputEvent();
            }
        }
    }
}
