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
        public float SpreadSpeedFactor = 0.1f;
        public float SpreadSpeedCap = 0.1f;
        public float Recoil = 0.05f;
        public float RecoilCap = 0.1f;
        public float RecoilDecay = 0.05f;
        public float MinSpread = 0;
        public float MaxSpread = 0.2f;
        private float currentRecoil = 0;
        private float spread = 0;
        private Vector3 spreadAngle = Vector3.zero;

        private void OnEnable()
        {
            Initialize();
            MasterGun.EventPlayerInput += OpenFire;
        }

        private void OnDisable()
        {
            MasterGun.EventPlayerInput -= OpenFire;
        }

        private void Update()
        {
            //GetSpread();
            if (Time.timeScale <= 0) return;
            currentRecoil -= RecoilDecay * Time.deltaTime;
            if (currentRecoil < 0) currentRecoil = 0;
        }

        private void Initialize()
        {
            MasterGun = GetComponent<Gun_Master>();
            myTransform = transform;
            camTransform = myTransform.parent;
        }

        public float GetSpread()
        {
            spread = GameManager.GameManager_References.Instance.MasterPlayer.Speed * SpreadSpeedFactor;
            if (spread > SpreadSpeedCap)
            {
                spread = SpreadSpeedCap;
            }

            spread += currentRecoil;

            if (spread > MaxSpread)
            {
                spread = MaxSpread;
            }
            else if (spread < MinSpread)
            {
                spread = MinSpread;
            }

            return spread;
        }

        private void OpenFire()
        {
            GetSpread();
            spreadAngle = camTransform.forward;
            spreadAngle.x += Random.Range(-spread, spread);
            spreadAngle.y += Random.Range(-spread, spread);
            spreadAngle.z += Random.Range(-spread, spread);

            if (Physics.Raycast(camTransform.position, spreadAngle, out hit, Range))
            {
                MasterGun.CallEventShotDefult(hit.point, hit.transform);
                
                if (hit.transform.CompareTag(GameManager.GameManager_References.Instance.EnemyTag))
                {
                    MasterGun.CallEventShotEnemy(hit.point, hit.transform);
                }
            }

            currentRecoil += Recoil;
            if (currentRecoil > RecoilCap)
            {
                currentRecoil = RecoilCap;
            }
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
                float offset = GizmoSpeed * SpreadSpeedFactor;
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
