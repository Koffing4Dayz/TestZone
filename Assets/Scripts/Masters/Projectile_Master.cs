using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectile
{
    public class Projectile_Master : MonoBehaviour
    {
        public delegate void GeneralEventHandler();
        public event GeneralEventHandler EventRunUpdate;
        public event GeneralEventHandler EventLaunched;
        public event GeneralEventHandler EventCollision;
        public event GeneralEventHandler EventReset;

        public delegate void SpellHitEventHandler(Transform hitTransform);
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

        public void CallLaunchedEvent()
        {
            if (EventLaunched != null)
            {
                EventLaunched();
            }
        }

        public void CallCollisionEvent()
        {
            if (EventCollision != null)
            {
                EventCollision();
            }
        }

        public void CallResetEvent()
        {
            if (EventReset != null)
            {
                EventReset();
            }
        }

        public void CallHitDefultEvent(Transform hitTransform)
        {
            if (EventHitDefult != null)
            {
                EventHitDefult(hitTransform);
            }
        }

        public void CallHitEnemyEvent(Transform hitTransform)
        {
            if (EventHitEnemy != null)
            {
                EventHitEnemy(hitTransform);
            }
        }
    }
}
