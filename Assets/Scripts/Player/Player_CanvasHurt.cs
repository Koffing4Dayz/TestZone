using UnityEngine;
using System.Collections;

namespace Player
{
    public class Player_CanvasHurt : MonoBehaviour
    {
        public GameObject HurtCanvas;
        private Player_Master MasterPlayer;
        public float Duration = 2;

        void OnEnable()
        {
            Initialize();
            MasterPlayer.EventPlayerHealthDeduction += TurnOnHurtEffect;
        }

        void OnDisable()
        {
            MasterPlayer.EventPlayerHealthDeduction -= TurnOnHurtEffect;
        }

        void Initialize()
        {
            MasterPlayer = GetComponent<Player_Master>();
        }

        void TurnOnHurtEffect(int dummy)
        {
            if (HurtCanvas != null)
            {
                StopAllCoroutines();
                HurtCanvas.SetActive(true);
                StartCoroutine(ResetHurtCanvas());
            }
        }

        IEnumerator ResetHurtCanvas()
        {
            yield return new WaitForSeconds(Duration);
            HurtCanvas.SetActive(false);
        }
    }
}