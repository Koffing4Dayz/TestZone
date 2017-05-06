using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spell
{
    public class Spell_Master : MonoBehaviour
    {
        public delegate void GeneralEventHandler();
        public event GeneralEventHandler EventRunUpdate;
        public event GeneralEventHandler EventFireInput;
        public event GeneralEventHandler EventSelected;

        public delegate void SpellHitEventHandler(Vector3 hitPosition, Transform hitTransform);
        public event SpellHitEventHandler EventHitDefult;
        public event SpellHitEventHandler EventHitEnemy;

        private void Update()
        {
            if (Time.timeScale <= 0) return;

            if (EventRunUpdate != null)
            {
                EventRunUpdate();
            }
        }

        public void CallFireInputEvent()
        {
            if (EventFireInput != null)
            {
                EventFireInput();
            }
        }

        public void CallSelectedEvent()
        {
            if (EventSelected != null)
            {
                EventSelected();
            }
        }

        public void CallHitDefultEvent(Vector3 hitPosition, Transform hitTransform)
        {
            if (EventHitDefult != null)
            {
                EventHitDefult(hitPosition, hitTransform);
            }
        }

        public void CallHitEnemyEvent(Vector3 hitPosition, Transform hitTransform)
        {
            if (EventHitEnemy != null)
            {
                EventHitEnemy(hitPosition, hitTransform);
            }
        }            
    }
}
