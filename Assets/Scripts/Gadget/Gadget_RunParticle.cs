using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gadget
{
    public class Gadget_RunParticle : MonoBehaviour
    {
        private Gadget_Master MasterGadget;

        public ParticleSystem myParticle;

        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            MasterGadget.EventActivateAbility += StartParticle;
            MasterGadget.EventDeactivateAbility += EndParticle;
        }

        private void OnDisable()
        {
            MasterGadget.EventActivateAbility -= StartParticle;
            MasterGadget.EventDeactivateAbility -= EndParticle;
        }

        private void Initialize()
        {
            MasterGadget = GetComponent<Gadget_Master>();
        }

        private void StartParticle()
        {
            myParticle.Play();
        }

        private void EndParticle()
        {
            myParticle.Stop();
        }
    }
}
