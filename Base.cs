using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    const string ENEMYTAG = "Enemy";

    public static int lives;
    [SerializeField ]private int startingLives;

    // Start is called before the first frame update
    void Start()
    {
        lives = startingLives;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(lives);
    }

    private void GameOver() 
    {
    
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == ENEMYTAG)
        {
            other.GetComponent<Enemy>().Die(false);
            lives--;

            if(lives < 1) 
            {
                GameOver();
            }
        }
    }
}
