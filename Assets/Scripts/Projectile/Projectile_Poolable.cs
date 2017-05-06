using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectile
{
    public class Projectile_Poolable : MonoBehaviour
    {
        private Projectile_Master MasterProjectile;
        private Transform myTransform;
        private Transform homeTransform;

        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            MasterProjectile.EventReset += Restart;
        }

        private void OnDisable()
        {
            MasterProjectile.EventReset -= Restart;
        }

        private void Initialize()
        {
            MasterProjectile = GetComponent<Projectile_Master>();
            myTransform = transform;
            homeTransform = myTransform.parent;
        }

        private void Restart()
        {
            gameObject.SetActive(false);
            myTransform.parent = homeTransform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }
}
