using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gun
{
    public class Gun_NPCInput : MonoBehaviour
    {
        private Gun_Master MasterGun;
        private Transform myTransform;
        private RaycastHit hit;
        public LayerMask layerToDamage;

        private Gun_Shoot myShoot;

        private void OnEnable()
        {
            MasterGun.EventNpcInput += NpcFireGun;
        }

        private void OnDisable()
        {
            MasterGun.EventNpcInput -= NpcFireGun;
        }

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            MasterGun = GetComponent<Gun_Master>();
            myTransform = transform;
            myShoot = GetComponent<Gun_Shoot>();
        }

        private void NpcFireGun(float rand)
        {
            Vector3 startPos = new Vector3(Random.Range(-rand, rand), Random.Range(-rand, rand), 0.5f);

            if (Physics.Raycast(myTransform.TransformPoint(startPos), myTransform.forward, out hit, myShoot.Range, layerToDamage))
            {
                if (hit.transform.gameObject.GetComponent<NPC.NPC_TakeDamage>() != null || hit.transform == GameManager.GameManager_References.Instance.Player.transform)
                {
                    MasterGun.CallEventShotEnemy(hit, hit.transform);
                }
                else
                {
                    MasterGun.CallEventShotDefult(hit, hit.transform);
                }
            }
        }
    }
}
