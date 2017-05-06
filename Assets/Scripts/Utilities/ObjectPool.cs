using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject myObject;
    public int NumberPooled;

    private GameObject[] pool;
    private Transform myTransform;
    private int index;

    private void Awake()
    {
        if (myObject != null)
        {
            myTransform = transform;
            pool = new GameObject[NumberPooled];

            for (int i = 0; i < NumberPooled; i++)
            {
                pool[i] = Instantiate(myObject, myTransform);
                pool[i].SetActive(false);
            }
        }
    }

    public bool GetNext(out GameObject obj)
    {
        if (pool[index].activeSelf == false)
        {
            obj = pool[index];
            ++index;

            if (index >= NumberPooled)
                index = 0;

            return true;
        }
        else
        {
            int current;
            for (int i = 1; i < NumberPooled; i++)
            {
                current = index + i;
                if (index + i >= NumberPooled)
                    current = current - NumberPooled;

                if (pool[current].activeSelf == false)
                {
                    obj = pool[current];
                    index = current++;

                    if (index >= NumberPooled)
                        index = 0;

                    return true;
                }
            }
        }

        obj = null;
        return false;
    }
}
