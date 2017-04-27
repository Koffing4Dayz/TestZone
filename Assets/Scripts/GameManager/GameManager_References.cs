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

        [SerializeField]
        private string pickupLayer;
        private static LayerMask ThePickupLayer;
        public LayerMask PickupLayer
        {
            get
            {
                return ThePickupLayer;
            }
        }

        [SerializeField]
        private string holdLayer;
        private static LayerMask TheHoldLayer;
        public LayerMask HoldLayer
        {
            get
            {
                return TheHoldLayer;
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

        private static Player.Player_Master TheMasterPlayer;
        public Player.Player_Master MasterPlayer
        {
            get
            {
                return TheMasterPlayer;
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
            if (pickupLayer == "")
            {
                Debug.LogWarning("[GameManager_References] - pickup layer not set (using defult: Item)");
                pickupLayer = "Item";
            }
            if (holdLayer == "")
            {
                Debug.LogWarning("[GameManager_References] - hold layer not set (using defult: Weapon)");
                holdLayer = "Weapon";
            }

            ThePlayerTag = playerTag;
            TheEnemyTag = enemyTag;
            TheItemTag = itemTag;

            ThePickupLayer = LayerMask.NameToLayer(pickupLayer);
            TheHoldLayer = LayerMask.NameToLayer(holdLayer);

            ThePlayer = GameObject.FindGameObjectWithTag(ThePlayerTag);
            TheMasterPlayer = ThePlayer.GetComponent<Player.Player_Master>();

            TheMasterGameManager = GetComponent<GameManager_Master>();
        }
    }
}
