using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Enemy_Detection : MonoBehaviour
    {
        private Enemy_Master MasterEnemy;
        private Transform myTransform;
        public Transform Head;
        public LayerMask PlayerLayer;
        public LayerMask SightLayer;
        private float checkRate;
        private float nextCheck;
        public float detectRadius = 80;
        private RaycastHit hit;
        public float fireRange = 40;

        private void OnEnable()
        {
            Initialize();
            MasterEnemy.EventEnemyDie += DisableThis;
        }

        private void OnDisable()
        {
            MasterEnemy.EventEnemyDie -= DisableThis;
        }

        private void Update()
        {
            RunDetection();
        }

        private void Initialize()
        {
            MasterEnemy = GetComponent<Enemy_Master>();
            myTransform = transform;

            if (Head == null)
            {
                Head = myTransform;
            }

            checkRate = Random.Range(0.8f, 1.2f);
        }

        private void RunDetection()
        {
            if (Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;

                Collider[] colliders = Physics.OverlapSphere(myTransform.position, detectRadius, PlayerLayer);

                if (colliders.Length > 0)
                {
                    foreach (Collider item in colliders)
                    {
                        if (item.CompareTag(GameManager.GameManager_References.Instance.PlayerTag))
                        {
                            if (CanTargetBeSeen(item.transform))
                            {
                                if (Vector3.Distance(myTransform.position, MasterEnemy.myTarget.position) < fireRange)
                                {
                                    MasterEnemy.CallEventEnemyAim();
                                }
                                else
                                {
                                    MasterEnemy.CallEventEnemyLostTarget();
                                }
                                break;
                            }
                        }
                    }
                }
                else
                {
                    MasterEnemy.CallEventEnemyLostTarget();
                }
            }
        }

        bool CanTargetBeSeen(Transform target)
        {
            if (Physics.Linecast(Head.position, target.position, out hit, SightLayer))
            {
                if (hit.transform == target)
                {
                    MasterEnemy.CallEventEnemySetNavTarget(target);
                    return true;
                }
                else
                {
                    MasterEnemy.CallEventEnemyLostTarget();
                    return false;
                }
            }
            else
            {
                MasterEnemy.CallEventEnemyLostTarget();
                return false;
            }
        }

        private void DisableThis()
        {
            this.enabled = false;
        }
    }
}
