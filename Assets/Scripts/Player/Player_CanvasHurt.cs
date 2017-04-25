using UnityEngine;
using System.Collections;

namespace S3
{
    public class Player_CanvasHurt : MonoBehaviour
    {
        public GameObject hurtCanvas;
        Player_Master playerMaster;
        float secondsTillHide = 2;

        void OnEnable()
        {
            Initialize();
            playerMaster.EventPlayerHealthDeduction += TurnOnHurtEffect;
        }

        void OnDisable()
        {
            playerMaster.EventPlayerHealthDeduction -= TurnOnHurtEffect;

        }

        void Initialize()
        {
            playerMaster = GetComponent<Player_Master>();
        }

        void TurnOnHurtEffect(int dummy)
        {
            if (hurtCanvas != null)
            {
                StopAllCoroutines();
                hurtCanvas.SetActive(true);
                StartCoroutine(ResetHurtCanvas());
            }
        }

        IEnumerator ResetHurtCanvas()
        {
            yield return new WaitForSeconds(secondsTillHide);
            hurtCanvas.SetActive(false);
        }
    }
}