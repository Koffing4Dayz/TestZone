using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Hi");
        if (collision.gameObject.CompareTag(GameManager.GameManager_References.Instance.PlayerTag))
        {
            Debug.Log("Hit Laser");
        }
    }
}
