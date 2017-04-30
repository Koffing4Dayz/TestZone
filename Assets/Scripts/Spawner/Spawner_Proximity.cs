using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spawner
{
    public class Spawner_Proximity : MonoBehaviour
    {
        public GameObject ObjectToSpawn;
        public int NumberToSpawn = 3;
        public float Proximity = 10;
        public float SpawnRadius = 5;
        private float checkRate;
        private float nextCheck;
        private Transform myTransform;
        private Transform playerTransform;
        private Vector3 spawnPosition;

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            CheckDistance();
        }

        private void Initialize()
        {
            myTransform = transform;
            playerTransform = GameManager.GameManager_References.Instance.Player.transform;
            checkRate = Random.Range(0.8f, 1.2f);
        }

        private void CheckDistance()
        {
            if (Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;
                if (Vector3.Distance(myTransform.position, playerTransform.position) < Proximity)
                {
                    SpawnObjects();
                    this.enabled = false;
                }
            }
        }

        private void SpawnObjects()
        {
            for (int i = 0; i < NumberToSpawn; ++i)
            {
                spawnPosition = myTransform.position + Random.insideUnitSphere * SpawnRadius;
                Instantiate(ObjectToSpawn, spawnPosition, myTransform.rotation);
            }
        }

#if UNITY_EDITOR
        public bool EnableGizmos;

        private void OnDrawGizmos()
        {
            if (EnableGizmos)
	        {
                Gizmos.color = GizmoHelper.LowAlpha(Color.green);
                Gizmos.DrawWireMesh(GizmoHelper.GetPrimitiveMesh(PrimitiveType.Cylinder), transform.position, Quaternion.identity, new Vector3(Proximity, 1, Proximity));
                Gizmos.color = GizmoHelper.LowAlpha(Color.red);
                Gizmos.DrawWireMesh(GizmoHelper.GetPrimitiveMesh(PrimitiveType.Cylinder), transform.position, Quaternion.identity, new Vector3(SpawnRadius, 1, SpawnRadius));
            }
        }
#endif
    }
}
