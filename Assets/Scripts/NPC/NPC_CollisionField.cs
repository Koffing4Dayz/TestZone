using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class NPC_CollisionField : MonoBehaviour
    {
        private NPC_Master MasterNPC;
        private Rigidbody rigidStrike;
        private int damageToApply;
        public float MassRequriement = 50;
        public float SpeedRequirement = 5;
        public float DamageFactor = 0.1f;

        private void OnEnable()
        {
            MasterNPC.EventNpcDie += DisableThis;
        }

        private void OnDisable()
        {
            MasterNPC.EventNpcDie -= DisableThis;
        }

        private void Awake()
        {
            Initialize();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Rigidbody>() != null)
            {
                rigidStrike = other.GetComponent<Rigidbody>();

                if (rigidStrike.mass >= MassRequriement && rigidStrike.velocity.sqrMagnitude >= SpeedRequirement * SpeedRequirement)
                {
                    damageToApply = (int)(rigidStrike.mass * rigidStrike.velocity.magnitude * DamageFactor);
                    MasterNPC.CallEventNpcDeductHealth(damageToApply);
                }
            }
        }

        private void Initialize()
        {
            MasterNPC = transform.root.GetComponent<NPC_Master>();
        }

        private void DisableThis()
        {
            gameObject.SetActive(false);
        }
    }
}
