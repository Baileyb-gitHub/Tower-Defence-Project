using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] points;
    // Start is called before the first frame update
    void Awake()
    {
        points = new Transform[transform.childCount]; // for each child create a points value
        for(int i = 0; i < points.Length; i++) 
        {
            points[i] = transform.GetChild(i);
        }
    }

  
}