using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public static void DeactivateObject(GameObject obj) 
    {
        obj.SetActive(false);
    }
    public static void ActivateObject(GameObject obj)
    {
        obj.SetActive(true);
    }
}
