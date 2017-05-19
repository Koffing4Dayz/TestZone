using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnorePlayerCollision : MonoBehaviour
{
    public Collider myCollider;

	void Start ()
    {
        Physics.IgnoreCollision(myCollider, GameManager.GameManager_References.Instance.Player.GetComponent<Collider>());
	}
}
