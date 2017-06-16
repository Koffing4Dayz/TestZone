using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gun
{
    public class Gun_ApplyDamage : MonoBehaviour
    {
        private Gun_Master MasterGun;
        public int Damage = 10;

        private void OnEnable()
        {
            Initialize();
            MasterGun.EventShotEnemy += ApplyDamage;
        }

        private void OnDisable()
        {
            MasterGun.EventShotEnemy -= ApplyDamage;
        }

        private void Initialize()
        {
            MasterGun = GetComponent<Gun_Master>();
        }

        void ApplyDamage(RaycastHit hitPosition, Transform hitTransform)
        {
            //if (hitTransform.GetComponent<Enemy.Enemy_TakeDamage>() != null)
            //{
            //    hitTransform.GetComponent<Enemy.Enemy_TakeDamage>().ProcessDamage(Damage);
            //}
            hitTransform.SendMessage("ProcessDamage", Damage, SendMessageOptions.DontRequireReceiver);
            hitTransform.SendMessage("CallEventPlayerHealthDeduction", Damage, SendMessageOptions.DontRequireReceiver);
            hitTransform.root.SendMessage("SetMyAttacker", transform.root, SendMessageOptions.DontRequireReceiver);

            Debug.Log(transform.root.name);
        }
    }
}
