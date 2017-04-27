using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class Enemy_NavPause : MonoBehaviour
    {
        private Enemy_Master MasterEnemy;
        private NavMeshAgent myNavMeshAgent;
        private float waitTime = 1;

        private void OnEnable()
        {
            Initialize();
            MasterEnemy.EventEnemyDie += DisableThis;
            MasterEnemy.EventEnemyHealthDeduction += PauseNav;
        }

        private void OnDisable()
        {
            MasterEnemy.EventEnemyDie -= DisableThis;
            MasterEnemy.EventEnemyHealthDeduction -= PauseNav;
        }

        private void Initialize()
        {
            MasterEnemy = GetComponent<Enemy_Master>();
            myNavMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void PauseNav(int dummy)
        {
            if (myNavMeshAgent != null)
            {
                if (myNavMeshAgent.enabled)
                {
                    myNavMeshAgent.ResetPath();
                    MasterEnemy.IsNavPaused = true;
                    StartCoroutine(RestartNav());
                }
            }
        }

        private IEnumerator RestartNav()
        {
            yield return new WaitForSeconds(waitTime);
            MasterEnemy.IsNavPaused = false;
        }

        private void DisableThis()
        {
            StopAllCoroutines();
        }
    }
}