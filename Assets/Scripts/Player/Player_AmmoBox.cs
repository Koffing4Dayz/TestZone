using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Player
{
    public class Player_AmmoBox : MonoBehaviour
    {
        private Player_Master MasterPlayer;

        [System.Serializable]
        public class AmmoTypes
        {
            public string ammoName;
            public int ammoMaxQuantity;
            public int ammoCurrentCarried;

            public AmmoTypes(string aName,int aMaxQuantity,int aCurrentCarried)
            {
                ammoName = aName;
                ammoMaxQuantity = aMaxQuantity;
                ammoCurrentCarried = aCurrentCarried;
            }
        }

        public List<AmmoTypes> typesOfAmmunition = new List<AmmoTypes>();

        void OnEnable()
        {
            Initialize();
            MasterPlayer.EventPickedUpAmmo += PickedUpAmmo;
        }

        void OnDisable()
        {
            MasterPlayer.EventPickedUpAmmo -= PickedUpAmmo;

        }

        void Initialize()
        {
            MasterPlayer = GetComponent<Player_Master>();
        }

        void PickedUpAmmo(string ammoName,int quantity)
        {
            for (int i = 0; i < typesOfAmmunition.Count; i++)
            {
                if (typesOfAmmunition[i].ammoName == ammoName)
                {
                    typesOfAmmunition[i].ammoCurrentCarried += quantity;
                    if (typesOfAmmunition[i].ammoCurrentCarried > typesOfAmmunition[i].ammoMaxQuantity)
                    {
                        typesOfAmmunition[i].ammoCurrentCarried = typesOfAmmunition[i].ammoMaxQuantity;
                    }
                    MasterPlayer.CallEventAmmoChanged();
                    break;
                }
            }
        }
    }
}