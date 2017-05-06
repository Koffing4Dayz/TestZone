using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spell
{
    public class Spell_LaunchProjectile : MonoBehaviour
    {
        private Spell_Master MasterSpell;
        private Transform camTransform;
        private ObjectPool myProjectiles;
        private GameObject current;

        public float Rate = 1;
        private float nextLaunch;
        public float LaunchPower = 50;

        private void Awake()
        {
            Initialize();   
        }

        private void OnEnable()
        {
            MasterSpell.EventFireInput += LaunchProjectile;
        }

        private void OnDisable()
        {
            MasterSpell.EventFireInput -= LaunchProjectile;
        }

        private void Initialize()
        {
            MasterSpell = GetComponent<Spell_Master>();
            camTransform = transform.parent;
            myProjectiles = GetComponent<ObjectPool>();
        }

        private void LaunchProjectile()
        {
            if (Time.time > nextLaunch)
            {
                nextLaunch = Time.time + Rate;
                if (myProjectiles.GetNext(out current))
                {
                    current.SetActive(true);
                    current.transform.parent = null;
                    current.GetComponent<Rigidbody>().AddForce(camTransform.forward * LaunchPower);
                    current.GetComponent<Projectile.Projectile_Master>().CallLaunchedEvent();
                }
            }
        }
    }
}
