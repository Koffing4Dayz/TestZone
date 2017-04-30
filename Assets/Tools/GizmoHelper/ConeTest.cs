using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeTest : MonoBehaviour {

    public Mesh myMesh;
    public Vector3[] verts;

    private void Start()
    {
        myMesh = GetComponent<MeshFilter>().mesh;
        verts = myMesh.vertices;
    }

    private void Update()
    {
        myMesh.vertices = verts;
    }
}
