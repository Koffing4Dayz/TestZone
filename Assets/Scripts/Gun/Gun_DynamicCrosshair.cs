using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gun
{
    public class Gun_DynamicCrosshair : MonoBehaviour
    {
        private Gun_Master MasterGun;
        private Item.Item_Master ItemMaster;
        public Transform CanvasDynamicCrosshair;
        private Transform playerTransform;
        private Transform weaponCamera;
        private float playerSpeed;
        private float nextCaptureTime;
        private float captureInterval = 0.5f;
        private Vector3 lastPosition;
        private Animator crosshairAnimator;
        public string weaponCameraName;
        private bool IsUIActive;
        public float HitMarkDuration = 0.1f;

        private void OnEnable()
        {
            Initialize();
            ItemMaster.EventObjectPickup += EnableUI;
            ItemMaster.EventObjectThrow += DisableUI;
        }

        private void OnDisable()
        {
            ItemMaster.EventObjectPickup -= EnableUI;
            ItemMaster.EventObjectThrow -= DisableUI;
        }

        private void Start()
        {
            IsStartingItem();
        }

        private void Update()
        {
            if (!IsUIActive) return;
            CapturePlayerSpeed();
            ApplySpeedToAnimation();
        }

        private void Initialize()
        {
            MasterGun = GetComponent<Gun_Master>();
            ItemMaster = GetComponent<Item.Item_Master>();
            playerTransform = GameManager.GameManager_References.Instance.Player.transform;
            crosshairAnimator = CanvasDynamicCrosshair.GetComponent<Animator>();
            FindWeaponCamera(playerTransform);
            SetCameraOnDynamicCrosshairCanvas();
            SetPlaneOnDynamicCrosshairCanvas();
        }

        private void CapturePlayerSpeed()
        {
            if (Time.time > nextCaptureTime)
            {
                nextCaptureTime = Time.time + captureInterval;
                playerSpeed = (playerTransform.position - lastPosition).magnitude / captureInterval;
                lastPosition = playerTransform.position;
                MasterGun.CallEventSpeedCaptured(playerSpeed);
            }
        }

        private void ApplySpeedToAnimation()
        {
            if (crosshairAnimator != null)
            {
                crosshairAnimator.SetFloat("Speed", playerSpeed);
            }
        }

        private void FindWeaponCamera(Transform searchTransform)
        {
            if (searchTransform != null)
            {
                if (searchTransform.name == weaponCameraName)
                {
                    weaponCamera = searchTransform;
                    return;
                }

                foreach (Transform item in searchTransform)
                {
                    FindWeaponCamera(item);
                }
            }
        }

        private void SetCameraOnDynamicCrosshairCanvas()
        {
            if (CanvasDynamicCrosshair != null && weaponCamera != null)
            {
                CanvasDynamicCrosshair.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
                CanvasDynamicCrosshair.GetComponent<Canvas>().worldCamera = weaponCamera.GetComponent<Camera>();
            }
        }

        private void SetPlaneOnDynamicCrosshairCanvas()
        {
            if (CanvasDynamicCrosshair != null)
            {
                CanvasDynamicCrosshair.GetComponent<Canvas>().planeDistance = 1;
            }
        }

        private void IsStartingItem()
        {
            if (transform.root.CompareTag(GameManager.GameManager_References.Instance.PlayerTag))
            {
                EnableUI();
            }
            else
            {
                DisableUI();
            }
        }

        private void EnableUI()
        {
            CanvasDynamicCrosshair.gameObject.SetActive(true);
            crosshairAnimator.enabled = true;
            IsUIActive = true;
        }

        private void DisableUI()
        {
            CanvasDynamicCrosshair.gameObject.SetActive(false);
            crosshairAnimator.enabled = false;
            IsUIActive = false;
        }

        private IEnumerator ActivateHitMarker()
        {
            yield return new WaitForSeconds(HitMarkDuration);
        }
    }
}
