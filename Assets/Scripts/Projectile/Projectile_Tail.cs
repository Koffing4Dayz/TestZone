using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectile
{
    public class Projectile_Tail : MonoBehaviour
    {
        private Projectile_Master MasterProjectile;
        private Rigidbody myRigidbody;

        public ParticleSystem myTail;
        public float Dampener = 0.1f;

        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            MasterProjectile.EventRunUpdate += RunUpdate;
        }

        private void OnDisable()
        {
            MasterProjectile.EventRunUpdate -= RunUpdate;
        }

        private void Initialize()
        {
            MasterProjectile = GetComponent<Projectile_Master>();
            myRigidbody = GetComponent<Rigidbody>();
        }

        private void RunUpdate()
        {
            var temp = myTail.main;
            temp.startLifetime = myRigidbody.velocity.magnitude * Dampener;
        }
    }
}
