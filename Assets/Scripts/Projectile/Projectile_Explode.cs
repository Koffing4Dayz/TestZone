using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectile
{
    public class Projectile_Explode : MonoBehaviour
    {
        private Projectile_Master MasterProjectile;
        private Rigidbody myRigidbody;
        private bool exploded = false;

        public ParticleSystem MainEffect;
        public ParticleSystem ExplodeEffect;
        public Collider MainCollider;
        public Collider ExplosionArea;
        public float Duration = 3;

        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            MasterProjectile.EventCollision += Explode;
            MasterProjectile.EventReset += Restart;
        }

        private void OnDisable()
        {
            MasterProjectile.EventCollision -= Explode;
            MasterProjectile.EventReset -= Restart;
        }

        private void Initialize()
        {
            MasterProjectile = GetComponent<Projectile_Master>();
            myRigidbody = GetComponent<Rigidbody>();
            ExplosionArea.isTrigger = true;
            ExplosionArea.enabled = false;
        }

        private void Explode()
        {
            if (exploded) return;

            MainCollider.enabled = false;
            myRigidbody.isKinematic = true;
            exploded = true;
            ExplodeEffect.Play();
            ExplosionArea.enabled = true;
            MainEffect.Stop();
            StartCoroutine(UpTime());
        }

        private IEnumerator UpTime()
        {
            yield return new WaitForSeconds(Duration);
            ExplosionArea.enabled = false;
            MasterProjectile.CallResetEvent();
        }

        private void Restart()
        {
            MainCollider.enabled = true;
            myRigidbody.isKinematic = false;
            ExplosionArea.enabled = false;
            ExplosionArea.isTrigger = true;
            exploded = false;
            MainEffect.Play();
        }
    }
}
