using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UI_Crosshair : MonoBehaviour
    {
        private Gun.Gun_Master MasterGun;
        public GameObject myGun;

        private void OnEnable()
        {
            Initialize();
        }

        private void OnDisable()
        {

        }

        private void Initialize()
        {
            MasterGun = myGun.GetComponent<Gun.Gun_Master>();
        }

        private void UpdateUI()
        {

        }
    }
}
