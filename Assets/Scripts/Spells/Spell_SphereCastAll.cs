using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spell
{
    public class Spell_SphereCastAll : MonoBehaviour
    {
        private Spell_Master MasterSpell;
        private RaycastHit[] hits;
        private Transform camTransform;

        public float Radius = 2.5f;
        public float Range = 100;

        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            MasterSpell.EventFireInput += RunCast;
            camTransform = transform.parent;
        }

        private void OnDisable()
        {
            MasterSpell.EventFireInput -= RunCast;
        }

        private void Initialize()
        {
            MasterSpell = GetComponent<Spell_Master>();
        }

        private void RunCast()
        {
            hits = Physics.SphereCastAll(camTransform.position, Radius, camTransform.forward, Range);

            if (hits == null) return;

            foreach (RaycastHit item in hits) 
            {
                MasterSpell.CallShotDefultEvent(item.point, item.transform);

                if (item.transform.CompareTag(GameManager.GameManager_References.Instance.EnemyTag))
                {
                    MasterSpell.CallShotEnemyEvent(item.point, item.transform);
                }
#if UNITY_EDITOR
                VisulizeCast();
#endif
            }
        }

#if UNITY_EDITOR
        public bool Visulize = false;
        private Vector3 drawPos = Vector3.zero;

        private void VisulizeCast()
        {
            if (Visulize)
            {
                drawPos = camTransform.position;
                Debug.DrawRay(drawPos, camTransform.forward.normalized * Range, GizmoHelper.LowAlpha(Color.red), 10);
                drawPos.x += Radius;
                Debug.DrawRay(drawPos, camTransform.forward.normalized * Range, GizmoHelper.LowAlpha(Color.red), 10);
                drawPos.y += Radius;
                Debug.DrawRay(drawPos, camTransform.forward.normalized * Range, GizmoHelper.LowAlpha(Color.red), 10);
                drawPos.x -= Radius * 2;
                Debug.DrawRay(drawPos, camTransform.forward.normalized * Range, GizmoHelper.LowAlpha(Color.red), 10);
                drawPos.y -= Radius * 2;
                Debug.DrawRay(drawPos, camTransform.forward.normalized * Range, GizmoHelper.LowAlpha(Color.red), 10);

                drawPos = Vector3.right * 0.5f;
                foreach (RaycastHit item in hits)
                {
                    Debug.DrawLine(item.point + drawPos, item.point - drawPos, GizmoHelper.LowAlpha(Color.green), 10);
                }

                drawPos = Vector3.up * 0.5f;
                foreach (RaycastHit item in hits)
                {
                    Debug.DrawLine(item.point + drawPos, item.point - drawPos, GizmoHelper.LowAlpha(Color.green), 10);
                }

                drawPos = Vector3.forward * 0.5f;
                foreach (RaycastHit item in hits)
                {
                    Debug.DrawLine(item.point + drawPos, item.point - drawPos, GizmoHelper.LowAlpha(Color.green), 10);
                }
            }
        }
#endif
    }
}
