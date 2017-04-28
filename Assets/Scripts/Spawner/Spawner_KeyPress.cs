using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spawner
{
    public class Spawner_KeyPress : MonoBehaviour
    {
        public GameObject ObjectToSpawn;
        public int NumberToSpawn = 3;
        public float SpawnRadius = 5;
        private Vector3 spawnPosition;
        public string KeyBind;

        private void Update()
        {
            if (KeyBind != "")
            {
                if (Input.GetButtonDown(KeyBind))
                {
                    SpawnObjects();
                }
            }
        }

        private void SpawnObjects()
        {
            for (int i = 0; i < NumberToSpawn; ++i)
            {
                spawnPosition = transform.position + Random.insideUnitSphere * SpawnRadius;
                Instantiate(ObjectToSpawn, spawnPosition, transform.rotation);
            }
        }
    }
}
