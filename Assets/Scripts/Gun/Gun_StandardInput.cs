using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gun
{
    public class Gun_StandardInput : MonoBehaviour
    {
        private Gun_Master MasterGun;
        private Item.Item_Master MasterItem;
        private float nextAttack;
        public float AttackRate = 0.5f;
        private Transform myTransform;
        public bool IsAutomatic;
        public bool HasAltFire;
        private bool isBurstFireActive;
        public string FireKeyBind = "Fire1";
        public string ReloadKeyBind = "Reload";
        public string AltFireKeyBind = "Fire2";

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            CheckIfFired();
            CheckReloadRequest();
            CheckBurstFireToggle();
        }

        private void Initialize()
        {
            MasterGun = GetComponent<Gun_Master>();
            myTransform = transform;
            MasterGun.IsGunLoaded = true;
            MasterItem = GetComponent<Item.Item_Master>();
            MasterItem.EventObjectPickup += EnableThis;
            MasterItem.EventObjectThrow += DisableThis;
            IsStartingItem();
        }

        private void CheckIfFired()
        {
            if (Time.time > nextAttack && Time.timeScale > 0)//  && myTransform.root.CompareTag(GameManager.GameManager_References.Instance.PlayerTag))
            {
                if (IsAutomatic && !isBurstFireActive)
                {
                    if (Input.GetButton(FireKeyBind))
                    {
                        //Debug.Log("Auto Bang");
                        AttemptFire();
                    }
                }
                else if (IsAutomatic && isBurstFireActive)
                {
                    if (Input.GetButtonDown(FireKeyBind))
                    {
                        //Debug.Log("Burst");
                        StartCoroutine(RunBurstFire());
                    }
                }
                else if (!IsAutomatic)
                {
                    if (Input.GetButtonDown(FireKeyBind))
                    {
                        AttemptFire();
                    }
                }
            }
        }

        private void AttemptFire()
        {
            nextAttack = Time.time + AttackRate;

            if (MasterGun.IsGunLoaded)
            {
                //Debug.Log("Bang");
                MasterGun.CallEventPlayerInput();
            }
            else
            {
                MasterGun.CallEventGunNotUsable();
            }
        }

        private void CheckReloadRequest()
        {
            if (Input.GetButtonDown(ReloadKeyBind) && Time.timeScale > 0)// && myTransform.root.CompareTag(GameManager.GameManager_References.Instance.PlayerTag))
            {
                MasterGun.CallEventRequestReload();
            }
        }

        private void CheckBurstFireToggle()
        {
            if (!HasAltFire) return;

            if (Input.GetButtonDown(AltFireKeyBind) && Time.timeScale > 0)//  && myTransform.root.CompareTag(GameManager.GameManager_References.Instance.PlayerTag))
            {
                //Debug.Log("Burst Fire Toggled");
                isBurstFireActive = !isBurstFireActive;
                MasterGun.CallEventToggleFireMode();
            }
        }

        private IEnumerator RunBurstFire()
        {
            AttemptFire();
            yield return new WaitForSeconds(AttackRate);
            AttemptFire();
            yield return new WaitForSeconds(AttackRate);
            AttemptFire();
            yield return new WaitForSeconds(AttackRate);
        }

        private void EnableThis()
        {
            enabled = true;
        }

        private void DisableThis()
        {
            enabled = false;
        }

        private void IsStartingItem()
        {
            if (myTransform.root.CompareTag(GameManager.GameManager_References.Instance.PlayerTag))
            {
                EnableThis();
            }
            else
            {
                DisableThis();
            }
        }
    }
}
