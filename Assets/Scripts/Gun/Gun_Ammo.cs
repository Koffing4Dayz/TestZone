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

        }

        private void TryReload()
        {

        }

        private void CheckAmmoStatus()
        {

        }

        private void StartingSanityCheck()
        {

        }

        private void UIAmmoUpdateRequest()
        {

        }

        private void ResetGunReloading()
        {

        }

        private void OnReloadComplete()
        {

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
