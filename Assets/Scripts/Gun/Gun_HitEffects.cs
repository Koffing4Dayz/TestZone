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

        private void DoDefultEffect(Vector3 hitPosition, Transform hitTransform)
        {
            if (DefultEffect != null)
            {
                Instantiate(DefultEffect, hitPosition, Quaternion.identity);
            }
        }

        private void DoEnemyEffect(Vector3 hitPosition, Transform hitTransform)
        {
            if (EnemyEffect != null)
            {
                Instantiate(EnemyEffect, hitPosition, Quaternion.identity);
            }
        }
    }
}
