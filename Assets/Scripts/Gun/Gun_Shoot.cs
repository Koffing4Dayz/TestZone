using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gun
{
    public class Gun_Shoot : MonoBehaviour
    {
        private Gun_Master MasterGun;
        private Transform myTransform;
        private Transform camTransform;
        private RaycastHit hit;
        public float Range = 400;
        private float offsetFactor = 7;
        private Vector3 startPosition;

        private void OnEnable()
        {
            Initialize();
            MasterGun.EventPlayerInput += OpenFire;
            MasterGun.EventSpeedCaptured += SetStartPosition;
        }

        private void OnDisable()
        {
            MasterGun.EventPlayerInput -= OpenFire;
            MasterGun.EventSpeedCaptured -= SetStartPosition;
        }

        private void Initialize()
        {
            MasterGun = GetComponent<Gun_Master>();
            myTransform = transform;
            camTransform = myTransform.parent;
        }

        private void OpenFire()
        {
            //Debug.Log("Open Fire");
            if (Physics.Raycast(camTransform.TransformPoint(startPosition), camTransform.forward, out hit, Range))
            {
                MasterGun.CallEventShotDefult(hit.point, hit.transform);
                
                if (hit.transform.CompareTag(GameManager.GameManager_References.Instance.EnemyTag))
                {
                    //Debug.Log("Shot Enemy");
                    MasterGun.CallEventShotEnemy(hit.point, hit.transform);
                }
            }
        }

        void SetStartPosition(float speed)
        {
            float offset = speed / offsetFactor;
            startPosition = new Vector3(Random.Range(-offset, offset), Random.Range(-offset, offset), 1);
        }
    }
}
