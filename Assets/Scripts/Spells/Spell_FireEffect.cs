using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spell
{
    public class Spell_FireEffect : MonoBehaviour
    {
        private Spell_Master MasterSpell;
        public ParticleSystem myEffect;
        public float StopDelay = 0.5f;

        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            MasterSpell.EventFireInput += PlayParticle;
        }

        private void OnDisable()
        {
            MasterSpell.EventFireInput -= PlayParticle;
            myEffect.Stop();
        }

        private void Initialize()
        {
            MasterSpell = GetComponent<Spell_Master>();
        }

        private void PlayParticle()
        {
            myEffect.Play();
            StopAllCoroutines();
            StartCoroutine(WaitToStop());
        }

        private IEnumerator WaitToStop()
        {
            yield return new WaitForSeconds(StopDelay);
            myEffect.Stop();
        }
    }
}
