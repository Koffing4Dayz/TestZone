using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Crosshair
{
    public class Crosshair_ApplyHit : MonoBehaviour
    {
        public Crosshair_Master MasterCrosshair;
        public float DecayRate = 0.1f;
        public Color InactiveColor;
        private Color defualtColor;
        private Image myImage;

        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            MasterCrosshair.EventActivateHitMarker += ApplyHit;
            myImage.color = InactiveColor;
        }

        private void OnDisable()
        {
            MasterCrosshair.EventActivateHitMarker -= ApplyHit;
            myImage.color = InactiveColor;
        }

        private void Initialize()
        {
            if (MasterCrosshair == null)
            {
                MasterCrosshair = GetComponent<Crosshair_Master>();
            }
            myImage = GetComponent<Image>();
            defualtColor = myImage.color;
            myImage.color = InactiveColor;
        }

        private void ApplyHit()
        {
            StopAllCoroutines();
            myImage.color = defualtColor;
            StartCoroutine(Decay());
        }

        private IEnumerator Decay()
        {
            yield return new WaitForSeconds(0.1f);
            myImage.color = Vector4.MoveTowards(myImage.color, InactiveColor, DecayRate);
            if (myImage.color != InactiveColor)
            {
                StartCoroutine(Decay());
            }
        }
    }
}
