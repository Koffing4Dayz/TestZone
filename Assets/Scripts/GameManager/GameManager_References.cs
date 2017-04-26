using UnityEngine;
using System.Collections;

namespace GameManager
{
    public class GameManager_References : MonoBehaviour
    {
        private GameManager_References() { }

        private static GameManager_References TheInstance;
        public static GameManager_References Instance
        {
            get
            {
                if (TheInstance == null)
                {
                    //TheInstance = new GameManager_References();
                    TheInstance = FindObjectOfType<GameManager_References>();
                    TheInstance.Initialize();
                }

                return TheInstance;
            }
        }

        [SerializeField]
        private string playerTag;
        private static string ThePlayerTag;
        public string PlayerTag
        {
            get
            {
                return ThePlayerTag;
            }
            set
            {
                ThePlayerTag = value;
            }
        }

        [SerializeField]
        private string enemyTag;
        private static string TheEnemyTag;
        public string EnemyTag
        {
            get
            {
                return TheEnemyTag;
            }
        }

        [SerializeField]
        private string itemTag;
        private static string TheItemTag;
        public string ItemTag
        {
            get
            {
                return TheItemTag;
            }
        }

        private static GameObject ThePlayer;
        public GameObject Player
        {
            get
            {
                return ThePlayer;
            }
        }

        private static GameManager_Master TheMasterGameManager;
        public GameManager_Master MasterGameManager
        {
            get
            {
                return TheMasterGameManager;
            }
        }

        void Awake()
        {
            TheInstance = this;
            Initialize();
        }

        private void Initialize()
        {
            if (playerTag == "")
            {
                Debug.LogWarning("[GameManager_References] - player tag not set");
            }
            if (enemyTag == "")
            {
                Debug.LogWarning("[GameManager_References] - enemy tag not set");
            }
            if (itemTag == "")
            {
                Debug.LogWarning("[GameManager_References] - item tag not set");
            }

            ThePlayerTag = playerTag;
            TheEnemyTag = enemyTag;
            TheItemTag = itemTag;

            ThePlayer = GameObject.FindGameObjectWithTag(ThePlayerTag);

            TheMasterGameManager = GetComponent<GameManager_Master>();
        }
    }
}
