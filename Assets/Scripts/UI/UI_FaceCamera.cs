using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UI_FaceCamera : MonoBehaviour
    {
        private void Update()
        {
            transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Vector3.up);
        }
    }
}
