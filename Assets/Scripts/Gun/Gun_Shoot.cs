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
        public float SpeedFactor = 0.1f;
        public float SpeedInterval = 0.2f;
        public float SpeedCap = 0.1f;
        public float SpeedDecay = 0.05f;
        public float Recoil = 0.05f;
        public float RecoilCap = 0.1f;
        public float RecoilDecay = 0.05f;
        public float MinSpread = 0;
        public float MaxSpread = 0.2f;
        private float targetSpeed = 0;
        private float currentSpeed = 0;
        private float currentRecoil = 0;
        private float spread = 0;
        private Vector3 spreadAngle = Vector3.zero;

        private void OnEnable()
        {
            MasterGun.EventPlayerInput += OpenFire;
            camTransform = myTransform.parent;
        }

        private void OnDisable()
        {
            MasterGun.EventPlayerInput -= OpenFire;
        }

        private void Awake()
        {
            Initialize();
            GetComponent<Item.Item_Master>().EventObjectThrow += DisableThis;
            GetComponent<Item.Item_Master>().EventObjectPickup += EnableThis;
        }

        private void OnDestroy()
        {
            if (GetComponent<Item.Item_Master>() != null)
            {
                GetComponent<Item.Item_Master>().EventObjectPickup -= EnableThis;
                GetComponent<Item.Item_Master>().EventObjectThrow -= DisableThis;
            }
        }

        private void Update()
        {
            if (Time.timeScale <= 0) return;
            CalcSpread();
        }

        private void Initialize()
        {
            MasterGun = GetComponent<Gun_Master>();
            myTransform = transform;
            IsStartingItem();
        }

        public float GetSpread()
        {
            return spread;
        }

        private void CalcSpread()
        {
            targetSpeed = GameManager.GameManager_References.Instance.MasterPlayer.Speed * SpeedFactor;

            if (currentSpeed < targetSpeed)
            {
                currentSpeed += SpeedInterval * Time.deltaTime;
            }
            else
            {
                currentSpeed -= SpeedDecay * Time.deltaTime;
                if (currentSpeed < 0) currentSpeed = 0;
            }

            if (currentSpeed > SpeedCap)
            {
                currentSpeed = SpeedCap;
            }

            spread = currentSpeed + currentRecoil;

            if (spread > MaxSpread)
            {
                spread = MaxSpread;
            }
            else if (spread < MinSpread)
            {
                spread = MinSpread;
            }

            currentRecoil -= RecoilDecay * Time.deltaTime;
            if (currentRecoil < 0) currentRecoil = 0;
        }

        private void OpenFire()
        {
            spreadAngle = camTransform.forward;
            spreadAngle.x += Random.Range(-spread, spread);
            spreadAngle.y += Random.Range(-spread, spread);
            spreadAngle.z += Random.Range(-spread, spread);

            if (Physics.Raycast(camTransform.position, spreadAngle, out hit, Range))
            {
                if (hit.transform.GetComponent<NPC.NPC_TakeDamage>())
                {
                    MasterGun.CallEventShotEnemy(hit, hit.transform);
                }
                else
                {
                    MasterGun.CallEventShotDefult(hit, hit.transform);
                }
            }

            currentRecoil += Recoil;
            if (currentRecoil > RecoilCap)
            {
                currentRecoil = RecoilCap;
            }
        }

        private void IsStartingItem()
        {
            if (transform.root.CompareTag(GameManager.GameManager_References.Instance.PlayerTag))
            {
                EnableThis();
            }
            else
            {
                DisableThis();
            }
        }

        private void EnableThis()
        {
            enabled = true;
        }

        private void DisableThis()
        {
            enabled = false;
        }

#if UNITY_EDITOR
        public bool EnableGizmos;
        public Vector3 GizmoOffset;
        public float GizmoSpeed;
        private Vector3 start;
        private Vector3 angle;

        private void OnDrawGizmos()
        {
            if (EnableGizmos)
            {
                Gizmos.color = GizmoHelper.LowAlpha(Color.green);
                start = transform.position + GizmoOffset;
                float offset = GizmoSpeed * SpeedFactor;
                angle = new Vector3(offset, offset, 1) * Range;

                //Gizmos.matrix = transform.worldToLocalMatrix;
                //Gizmos.DrawFrustum(start, offset, Range, 0, 1);

                Gizmos.DrawRay(start, angle);
                angle.x *= -1;
                Gizmos.DrawRay(start, angle);
                angle.y *= -1;
                Gizmos.DrawRay(start, angle);
                angle.x *= -1;
                Gizmos.DrawRay(start, angle);

                angle = new Vector3(Random.Range(angle.x, -angle.x), Random.Range(angle.y, -angle.y), Range);
                Gizmos.color = GizmoHelper.LowAlpha(Color.red);
                Gizmos.DrawRay(start, angle);

                //end = new Vector3(start.x + OffsetFactor, start.y + OffsetFactor, start.z + Range);
                //Gizmos.DrawLine(start, end);
                //end.x *= -1;
                //Gizmos.DrawLine(start, end);
                //end.y *= -1;
                //Gizmos.DrawLine(start, end);
                //end.x *= -1;
                //Gizmos.DrawLine(start, end);
            }
        }
#endif
    }
}
