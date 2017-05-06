using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManager
{
    public class GameManager_ManaTypes : MonoBehaviour
    {
        [System.Serializable]
        public class ManaType
        {
            public string name;
            public Color color;

            public ManaType(string _name, Color _color)
            {
                name = _name;
                color = _color;
            }

        }

        public List<ManaType> ManaTypes = new List<ManaType>();
    }
}
