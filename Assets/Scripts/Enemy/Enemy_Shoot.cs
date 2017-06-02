using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class Enemy_Shoot : MonoBehaviour
    {
        private Enemy_Master MasterEnemy;
        private NavMeshAgent myNavMeshAgent;
        private int count = 0;
        private RaycastHit hit;

        public Transform GunTransform;
        public int Damage = 5;
        public float SpreadAngle = 0.1f;
        public float FireRate = 1;
        public int BurstAmount = 3;
        public float Range;
        public float GunVolume = 1;
        public AudioClip Gunshot;

        private void OnEnable()
        {
            Initialize();
            MasterEnemy.EventEnemyAim += OpenFire;
        }

        private void OnDisable()
        {
            MasterEnemy.EventEnemyAim -= OpenFire;
        }

        private void Initialize()
        {
            MasterEnemy = GetComponent<Enemy_Master>();
            myNavMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void OpenFire()
        {
            WaitABit();
        }

        private IEnumerator ShootOne()
        {
            Debug.Log("Bang");

            count++;

            Vector3 shot = transform.forward;
            shot += new Vector3(Random.Range(-SpreadAngle, SpreadAngle), Random.Range(-SpreadAngle, SpreadAngle), Random.Range(-SpreadAngle, SpreadAngle));

            if (Physics.Raycast(GunTransform.position, shot, out hit, Range))
            {
                if (hit.transform.CompareTag(GameManager.GameManager_References.Instance.PlayerTag))
                {
                    hit.transform.gameObject.GetComponent<Player.Player_Master>().CallEventPlayerHealthDeduction(Damage);
                }
            }

            if (count < BurstAmount)
            {
                ShootOne();
            }
            else
            {
                MasterEnemy.IsNavPaused = false;
            }

            yield return new WaitForSeconds(FireRate);
        }

        private IEnumerator WaitABit()
        {
            yield return new WaitForSeconds(0.5f);
            MasterEnemy.IsNavPaused = true;
            myNavMeshAgent.ResetPath();
            count = 0;
            ShootOne();
        }
    }
}
