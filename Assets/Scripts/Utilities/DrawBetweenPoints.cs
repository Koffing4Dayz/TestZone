using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBetweenPoints : MonoBehaviour
{
    public struct Region
    {
        float start;
        float end;
        float lifetime;
    }

    public Vector3 StartPoint;
    public Vector3 EndPoint;
    public float RegionDecay = 0.1f;

    private List<Region> regions = new List<Region>();

    private void Update()
    {

    }
}
