using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOnLaser : MonoBehaviour
{
    public class Hit
    {
        public Matrix4x4 drawMatrix;
        public float life;

        public Hit(Matrix4x4 location)
        {
            drawMatrix = location;
            life = 10;
        }
    }

    private ParticleSystem myParticle;
    private List<ParticleCollisionEvent> collisionEvents;
    private List<Hit> hits;
    private Vector3 myScale = new Vector3(0.1f, 0.01f, 0.01f);

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
        if (other.CompareTag("Laser"))
        {
            int numCollisionEvents = myParticle.GetCollisionEvents(other, collisionEvents);

            foreach (ParticleCollisionEvent item in collisionEvents)
            {
                hits.Add(new Hit(Matrix4x4.TRS(item.intersection, other.transform.rotation, myScale)));
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < hits.Count; i++)
        {
            Graphics.DrawMesh(hitMesh, hits[i].drawMatrix, hitMat, 0, null, 0, null, false, false, false);
            hits[i].life -= 0.1f;

            if (hits[i].life < 0)
            {
                hits.RemoveAt(i);
            }
        }
    }
}
