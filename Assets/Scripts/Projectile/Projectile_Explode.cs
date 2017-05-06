using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectile
{
    public class Projectile_Explode : MonoBehaviour
    {
        private Projectile_Master MasterProjectile;
        private SphereCollider myCollider;
        private bool exploded = false;

        public ParticleSystem MainEffect;
        public ParticleSystem ExplodeEffect;
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
            myCollider = GetComponent<SphereCollider>();
            ExplosionArea.isTrigger = true;
            ExplosionArea.enabled = false;
        }

        private void Explode()
        {
            if (exploded) return;

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
            ExplosionArea.enabled = false;
            ExplosionArea.isTrigger = true;
            exploded = false;
            MainEffect.Play();
        }
    }
}
