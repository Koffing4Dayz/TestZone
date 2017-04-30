using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GizmoHelper
{
    private static Dictionary<PrimitiveType, Mesh> primitiveMeshes = new Dictionary<PrimitiveType, Mesh>();

    public static GameObject CreatePrimitive(PrimitiveType type, bool withCollider)
    {
        if (withCollider) { return GameObject.CreatePrimitive(type); }

        GameObject gameObject = new GameObject(type.ToString());
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.sharedMesh = GizmoHelper.GetPrimitiveMesh(type);
        gameObject.AddComponent<MeshRenderer>();

        return gameObject;
    }

    public static Mesh GetPrimitiveMesh(PrimitiveType type)
    {
        if (!GizmoHelper.primitiveMeshes.ContainsKey(type))
        {
            GizmoHelper.CreatePrimitiveMesh(type);
        }

        return GizmoHelper.primitiveMeshes[type];
    }

    private static Mesh CreatePrimitiveMesh(PrimitiveType type)
    {
        GameObject gameObject = GameObject.CreatePrimitive(type);
        Mesh mesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
        GameObject.DestroyImmediate(gameObject);

        GizmoHelper.primitiveMeshes[type] = mesh;
        return mesh;
    }

    public static Color LowAlpha(Color input)
    {
        return new Color(input.r, input.g, input.b, 0.15f);
    }
}
