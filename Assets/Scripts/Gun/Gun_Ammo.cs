using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gun
{
    public class Gun_Ammo : MonoBehaviour
    {
        private Gun_Master MasterGun;
        private Player.Player_Master myMasterPlayer;
        private Player.Player_AmmoBox myAmmoBox;
        private Animator myAnimator;

        public int ClipSize;
        public int CurrentAmmo;
        public string AmmoName;
        public float ReloadTime;

        private void OnEnable()
        {
            Initialize();
            StartingSanityCheck();
            CheckAmmoStatus();

            MasterGun.EventPlayerInput += DeductAmmo;
            MasterGun.EventPlayerInput += CheckAmmoStatus;
            MasterGun.EventRequestReload += TryReload;
            MasterGun.EventGunNotUsable += TryReload;
            MasterGun.EventRequestGunReset += ResetGunReloading;

            if (myMasterPlayer != null)
            {
                myMasterPlayer.EventAmmoChanged += UIAmmoUpdateRequest;
            }

            if (myAmmoBox != null)
            {
                StartCoroutine(UpdateAmmoUIWhenEnabling());
            }
        }

        private void OnDisable()
        {
            MasterGun.EventPlayerInput -= DeductAmmo;
            MasterGun.EventPlayerInput -= CheckAmmoStatus;
            MasterGun.EventRequestReload -= TryReload;
            MasterGun.EventGunNotUsable -= TryReload;
            MasterGun.EventRequestGunReset -= ResetGunReloading;

            if (myMasterPlayer != null)
            {
                myMasterPlayer.EventAmmoChanged -= UIAmmoUpdateRequest;
            }
        }

        private void Start()
        {
            Initialize();
            StartCoroutine(UpdateAmmoUIWhenEnabling());
        }

        private void Initialize()
        {
            MasterGun = GetComponent<Gun_Master>();
            myAnimator = GetComponent<Animator>();
            myMasterPlayer = GameManager.GameManager_References.Instance.MasterPlayer;
            myAmmoBox = myMasterPlayer.GetComponent<Player.Player_AmmoBox>();
        }

        private void DeductAmmo()
        {
            CurrentAmmo--;
            UIAmmoUpdateRequest();
        }

        private void TryReload()
        {
            for (int i = 0; i < myAmmoBox.typesOfAmmunition.Count; i++)
            {
                if (myAmmoBox.typesOfAmmunition[i].ammoName == AmmoName)
                {
                    if (myAmmoBox.typesOfAmmunition[i].ammoCurrentCarried > 0 && CurrentAmmo != ClipSize && !MasterGun.IsReloading)
                    {
                        MasterGun.IsReloading = true;
                        MasterGun.IsGunLoaded = false;

                        if (myAnimator != null)
                        {
                            myAnimator.SetTrigger("Reload");
                        }
                        else
                        {
                            ReloadWithoutAnimation();
                        }
                    }
                    break;
                }
            }
        }

        private void CheckAmmoStatus()
        {
            if (CurrentAmmo <= 0)
            {
                CurrentAmmo = 0;
                MasterGun.IsGunLoaded = false;
            }
            else if (CurrentAmmo > 0)
            {
                MasterGun.IsGunLoaded = true;
            }
        }

        private void StartingSanityCheck()
        {
            if (CurrentAmmo > ClipSize)
            {
                CurrentAmmo = ClipSize;
            }
        }

        private void UIAmmoUpdateRequest()
        {
            for (int i = 0; i < myAmmoBox.typesOfAmmunition.Count; i++)
            {
                if (myAmmoBox.typesOfAmmunition[i].ammoName == AmmoName)
                {
                    MasterGun.CallEventAmmoChanged(CurrentAmmo, myAmmoBox.typesOfAmmunition[i].ammoCurrentCarried);
                    break;
                }
            }
        }

        private void ResetGunReloading()
        {
            MasterGun.IsReloading = false;
            CheckAmmoStatus();
            UIAmmoUpdateRequest();
        }

        private void OnReloadComplete()
        {
            for (int i = 0; i < myAmmoBox.typesOfAmmunition.Count; i++)
            {
                if (myAmmoBox.typesOfAmmunition[i].ammoName == AmmoName)
                {
                    int ammoFill = ClipSize - CurrentAmmo;

                    if (myAmmoBox.typesOfAmmunition[i].ammoCurrentCarried >= ammoFill)
                    {
                        CurrentAmmo += ammoFill;
                        myAmmoBox.typesOfAmmunition[i].ammoCurrentCarried -= ammoFill;
                    }
                    else if (myAmmoBox.typesOfAmmunition[i].ammoCurrentCarried < ammoFill
                          && myAmmoBox.typesOfAmmunition[i].ammoCurrentCarried != 0)
                    {
                        CurrentAmmo += myAmmoBox.typesOfAmmunition[i].ammoCurrentCarried;
                        myAmmoBox.typesOfAmmunition[i].ammoCurrentCarried = 0;
                    }
                    break;
                }
            }

            ResetGunReloading();
        }

        IEnumerator ReloadWithoutAnimation()
        {
            yield return new WaitForSeconds(ReloadTime);
            OnReloadComplete();
        }

        IEnumerator UpdateAmmoUIWhenEnabling()
        {
            yield return new WaitForSeconds(0.05f);
            UIAmmoUpdateRequest();
        }
    }
}
