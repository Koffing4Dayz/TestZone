using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectile
{
    public class Projectile_OnCollision : MonoBehaviour
    {
        Projectile_Master MasterProjectile;
        public bool IgnorePlayer = true;

        private void Awake()
        {
            MasterProjectile = GetComponent<Projectile_Master>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (IgnorePlayer && collision.gameObject.CompareTag(GameManager.GameManager_References.Instance.PlayerTag))
            {
                return;
            }
            else if (collision.gameObject.CompareTag(GameManager.GameManager_References.Instance.EnemyTag))
            {
                MasterProjectile.CallHitEnemyEvent(collision.gameObject.transform);
            }
            else if (collision.gameObject.CompareTag("Projectile")) 
            {
                return;
            }
            MasterProjectile.CallCollisionEvent();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(GameManager.GameManager_References.Instance.EnemyTag)
             || other.gameObject.CompareTag(GameManager.GameManager_References.Instance.PlayerTag))
            {
                MasterProjectile.CallHitEnemyEvent(other.gameObject.transform);
            }
        }
    }
}
