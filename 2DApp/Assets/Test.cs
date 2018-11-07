using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float[] list;

    public float value;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float num = 0;
        for (int i = 0; i < list.Length; i++)
        {
            if (value < num)
            {
                Debug.log(i);
            }
            num += list[i];
        }
    }
}
