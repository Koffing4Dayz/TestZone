using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectile
{
    public class Projectile_OnCollision : MonoBehaviour
    {
        Projectile_Master MasterProjectile;

        private void Awake()
        {
            MasterProjectile = GetComponent<Projectile_Master>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(GameManager.GameManager_References.Instance.EnemyTag))
            {
                MasterProjectile.CallHitEnemyEvent(collision.gameObject.transform);
            }
            MasterProjectile.CallCollisionEvent();
        }
    }
}
