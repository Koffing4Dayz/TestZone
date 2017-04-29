using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gun
{
    public class Gun_AmmoUI : MonoBehaviour
    {
        private Gun_Master MasterGun;
        public InputField currentAmmoField;
        public InputField carriedAmmoField;

        private void OnEnable()
        {
            Initialize();
            MasterGun.EventAmmoChanged += UpdateAmmoUI;
        }

        private void OnDisable()
        {
            MasterGun.EventAmmoChanged -= UpdateAmmoUI;
        }

        private void Initialize()
        {
            MasterGun = GetComponent<Gun_Master>();
        }

        private void UpdateAmmoUI(int currentAmmo, int carriedAmmo)
        {
            if (currentAmmoField != null)
            {
                currentAmmoField.text = currentAmmo.ToString();
            }

            if (carriedAmmoField != null)
            {
                carriedAmmoField.text = carriedAmmo.ToString();
            }
        }
    }
}
