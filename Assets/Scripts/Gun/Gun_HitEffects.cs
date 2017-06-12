using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gun
{
    public class Gun_HitEffects : MonoBehaviour
    {
        private Gun_Master MasterGun;
        public GameObject DefultEffect;
        public GameObject EnemyEffect;

        private void OnEnable()
        {
            Initialize();
            MasterGun.EventShotDefult += DoDefultEffect;
            MasterGun.EventShotEnemy += DoEnemyEffect;
        }

        private void OnDisable()
        {
            MasterGun.EventShotDefult -= DoDefultEffect;
            MasterGun.EventShotEnemy -= DoEnemyEffect;
        }

        private void Initialize()
        {
            MasterGun = GetComponent<Gun_Master>();
        }

        private void DoDefultEffect(RaycastHit hitPosition, Transform hitTransform)
        {
            if (DefultEffect != null)
            {
                Quaternion quat = Quaternion.LookRotation(hitPosition.normal);
                Instantiate(DefultEffect, hitPosition.point, quat);
            }
        }

        private void DoEnemyEffect(RaycastHit hitPosition, Transform hitTransform)
        {
            if (EnemyEffect != null)
            {
                Quaternion quat = Quaternion.LookRotation(hitPosition.normal);
                Instantiate(EnemyEffect, hitPosition.point, quat);
            }
        }
    }
}
