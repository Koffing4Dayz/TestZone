using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Player_CaptureSpeed : MonoBehaviour
    {
        private Player_Master MasterPlayer;
        private Transform myTransform;
        private float nextCaptureTime;
        private float captureInterval = 0.5f;
        private Vector3 lastPosition;

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            CapturePlayerSpeed();
        }

        private void Initialize()
        {
            MasterPlayer = GetComponent<Player_Master>();
            myTransform = transform;
        }

        private void CapturePlayerSpeed()
        {
            if (Time.time > nextCaptureTime)
            {
                nextCaptureTime = Time.time + captureInterval;
                MasterPlayer.Speed = (myTransform.position - lastPosition).magnitude / captureInterval;
                lastPosition = myTransform.position;
            }
        }
    }
}
