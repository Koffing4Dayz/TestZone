using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace Spell
{
    public class Spell_ApplyBackwardsForce : MonoBehaviour
    {
        private Spell_Master MasterSpell;
        private Rigidbody playerRigid;
        private RigidbodyFirstPersonController playerCon;
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
            playerCon = GameManager.GameManager_References.Instance.Player.GetComponent<RigidbodyFirstPersonController>();
        }

        private void ApplyForce()
        {
            if (playerCon.Grounded) return;

            playerRigid.AddForce(-camTransform.forward * Power);

            if (playerRigid.velocity.sqrMagnitude > (playerCon.movementSettings.FlySpeed * playerCon.movementSettings.FlySpeed))
            {
                playerRigid.velocity = playerRigid.velocity.normalized * playerCon.movementSettings.FlySpeed;
            }
        }
    }
}
