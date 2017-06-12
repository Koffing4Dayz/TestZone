using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class NPC_TakeDamage : MonoBehaviour
    {
        private NPC_Master MasterNPC;
        public int DamageMultiplier = 1;
        public bool RemoveCollider;

        private void OnEnable()
        {
            MasterNPC.EventNpcDie += RemoveThis;
        }

        private void OnDisable()
        {
            MasterNPC.EventNpcDie -= RemoveThis;
        }

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            MasterNPC = transform.root.GetComponent<NPC_Master>();
        }

        public void ProcessDamage(int damage)
        {
            int finalDmg = damage * DamageMultiplier;
            MasterNPC.CallEventNpcDeductHealth(damage);
        }

        private void RemoveThis()
        {
            if (RemoveCollider)
            {
                if (GetComponent<Collider>() != null)
                {
                    Destroy(GetComponent<Collider>());
                }

                if (GetComponent<Rigidbody>() != null)
                {
                    Destroy(GetComponent<Rigidbody>());
                }

                gameObject.layer = LayerMask.NameToLayer("Default");

                Destroy(this);
            }
        }
    }
}
