using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace GameManager
{
    public class GameManager_RestartLevel : MonoBehaviour
    {
        void OnEnable()
        {
            GameManager.GameManager_References.Instance.MasterGameManager.EventRestartLevel += RestartLevel;
        }

        void OnDisable()
        {
            GameManager.GameManager_References.Instance.MasterGameManager.EventRestartLevel -= RestartLevel;
        }

        void RestartLevel()
        {
            SceneManager.LoadScene(1);
        }
    }
}
