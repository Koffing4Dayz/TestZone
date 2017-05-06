using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UI_ManaPool : MonoBehaviour
    {
        private RectTransform[] transforms;
        private Image[] images;
        private float fillTotal = 0;

        private void Awake()
        {
            transforms = GetComponentsInChildren<RectTransform>();
            images = GetComponentsInChildren<Image>();
        }

        private void Update()
        {
            fillTotal = 0;

            for (int i = 1; i < images.Length; i++)
            {
                transforms[i].localRotation = Quaternion.Euler(0, 0, -360 * fillTotal);
                fillTotal += images[i].fillAmount;
            }
        }
    }
}
