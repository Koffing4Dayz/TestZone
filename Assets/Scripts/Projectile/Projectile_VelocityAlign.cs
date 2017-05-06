using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectile
{
    public class Projectile_VelocityAlign : MonoBehaviour
    {
        private Projectile_Master MasterProjectile;
        private Rigidbody myRigidbody;
        private Transform myTransform;

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
            myTransform = transform;
        }

        private void RunUpdate()
        {
            if (myRigidbody.velocity == Vector3.zero) return;
            transform.rotation = Quaternion.LookRotation(myRigidbody.velocity);
        }
    }
}
