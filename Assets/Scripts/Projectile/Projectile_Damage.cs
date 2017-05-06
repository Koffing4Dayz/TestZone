using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectile
{
    public class Projectile_Damage : MonoBehaviour
    {
        private Projectile_Master MasterProjectile;
        public int Damage = 10;

        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            MasterProjectile.EventHitEnemy += ApplyDamage;
        }

        private void OnDisable()
        {
            MasterProjectile.EventHitEnemy -= ApplyDamage;
        }

        private void Initialize()
        {
            MasterProjectile = GetComponent<Projectile_Master>();
        }

        private void ApplyDamage(Transform hitTransform)
        {
            if (hitTransform.GetComponent<Enemy.Enemy_TakeDamage>() != null)
            {
                hitTransform.GetComponent<Enemy.Enemy_TakeDamage>().ProcessDamage(Damage);
            }
            else if (hitTransform.GetComponent<Player.Player_Master>() != null)
            {
                hitTransform.GetComponent<Player.Player_Master>().CallEventPlayerHealthDeduction(Damage);
            }
        }
    }
}
