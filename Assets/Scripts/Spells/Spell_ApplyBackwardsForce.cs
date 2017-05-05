using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spell
{
    public class Spell_ApplyBackwardsForce : MonoBehaviour
    {
        private Spell_Master MasterSpell;
        private Rigidbody playerRigid;
        private Transform camTransform;
        public float Power = 10;

        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            MasterSpell.EventFireInput += ApplyForce;
            camTransform = transform.parent;
        }

        private void OnDisable()
        {
            MasterSpell.EventFireInput -= ApplyForce;
        }

        private void Initialize()
        {
            MasterSpell = GetComponent<Spell_Master>();
            playerRigid = GameManager.GameManager_References.Instance.Player.GetComponent<Rigidbody>();
        }

        private void ApplyForce()
        {
            playerRigid.AddForce(-camTransform.forward * Power);
        }
    }
}
