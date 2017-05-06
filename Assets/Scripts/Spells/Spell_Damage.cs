using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spell
{
    public class Spell_Damage : MonoBehaviour
    {
        private Spell_Master MasterSpell;
        public int Damage = 10;

        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            MasterSpell.EventHitEnemy += ApplyDamage;
        }

        private void OnDisable()
        {
            MasterSpell.EventHitEnemy -= ApplyDamage;
        }

        private void Initialize()
        {
            MasterSpell = GetComponent<Spell_Master>();
        }

        void ApplyDamage(Vector3 hitPosition, Transform hitTransform)
        {
            if (hitTransform.GetComponent<Enemy.Enemy_TakeDamage>() != null)
            {
                hitTransform.GetComponent<Enemy.Enemy_TakeDamage>().ProcessDamage(Damage);
            }
        }
    }
}
