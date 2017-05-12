using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOnLaser : MonoBehaviour
{
    public class Hit
    {
        public Vector3 pos;
        public float life;

        public Hit(Vector3 location)
        {
            pos = location;
            life = 5;
        }
    }

    private ParticleSystem myParticle;
    private List<ParticleCollisionEvent> collisionEvents;
    private List<Hit> hits;
    private Vector2 top = new Vector2(0.2f, 0.2f);
    private Vector2 bot = new Vector2(-0.2f, -0.2f);

    public Mesh hitMesh;
    public Material hitMat;

    private void Awake()
    {
        myParticle = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        hits = new List<Hit>();
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("HA");

        if (other.CompareTag("Laser"))
        {
            Debug.Log("HO");
            int numCollisionEvents = myParticle.GetCollisionEvents(other, collisionEvents);

            foreach (ParticleCollisionEvent item in collisionEvents)
            {
                Debug.Log("HI");
                hits.Add(new Hit(item.intersection));
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < hits.Count; i++)
        {
            Graphics.DrawMesh(hitMesh, hits[i].pos, Quaternion.identity, hitMat, 0);
            hits[i].life -= 0.1f;

            if (hits[i].life < 0)
            {
                hits.RemoveAt(i);
            }
        }
    }
}
