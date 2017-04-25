using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace GameManager
{
    public class GameManager_GoToMenuScene : MonoBehaviour
    {
        void OnEnable()
        {
            GameManager.GameManager_References.Instance.MasterGameManager.EventGoToMenuScene += GoToMenuScene;
        }

        void OnDisable()
        {
            GameManager.GameManager_References.Instance.MasterGameManager.EventGoToMenuScene -= GoToMenuScene;
        }

        void GoToMenuScene()
        {
            SceneManager.LoadScene(0);
        }
    }
}
