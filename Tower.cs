using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Tower : MonoBehaviour
{
    const string ENEMYTAG = "Enemy";

    [SerializeField] private string towerName;
    [SerializeField] private float towerRange = 3;
    [SerializeField] private enum targetModeOptions { First, Last, Strong, Weak };
    [SerializeField] private targetModeOptions targetMode;

    [SerializeField] private int shotDamage = 25;
    [SerializeField] private float shotReloadTimeMax = 2.5f;
    [SerializeField] private float shotReloadTimeCurrent;

    [SerializeField] private SphereCollider rangeCollider;

    [SerializeField] private Enemy targetEnemy;
    [SerializeField] private List<Enemy> enemyList;

    [Header("Audio")]
    [SerializeField] private AudioSource myAudioPlayer;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip sellSound;
    [SerializeField] private AudioClip placeSound;

    // Start is called before the first frame update
    void Start()
    {
        rangeCollider = gameObject.GetComponent<SphereCollider>();
        rangeCollider.radius = towerRange;

        if (rangeCollider.isTrigger == false) 
        {
            rangeCollider.isTrigger = true;
        }

        shotReloadTimeCurrent = shotReloadTimeMax;
    }

    // Update is called once per frame
    void Update()
    {
        shotReloadTimeCurrent -=  1 * Time.deltaTime;


       
        
        getTarget();
      
        if (shotReloadTimeCurrent < 0 && targetEnemy != null)
        {
            Shoot();
            shotReloadTimeCurrent = shotReloadTimeMax;
        }
        
    }

    private void getTarget() 
    {
        if(enemyList.Count == 0) 
        {
            targetEnemy = null;
        }
        else 
        {
            switch(targetMode)
            {
                case targetModeOptions.First:
                    targetEnemy = enemyList[0];
                    break;

                case targetModeOptions.Last:
                    targetEnemy = enemyList[enemyList.Count - 1]; ;
                    break;

                case targetModeOptions.Strong:

                    targetEnemy = enemyList[0];
                    foreach(Enemy E in enemyList) 
                    {
                        if(E.GetHealthPoints() > targetEnemy.GetHealthPoints()) 
                        {
                            targetEnemy = E;
                        }
                    }
                    break;

                case targetModeOptions.Weak:

                    targetEnemy = enemyList[0];
                    foreach (Enemy E in enemyList)
                    {
                        if (E.GetHealthPoints() < targetEnemy.GetHealthPoints())
                        {
                            targetEnemy = E;
                        }
                    }
                    break;
            }
        }
    }

    public void removeTarget()
    {
        enemyList.Remove(targetEnemy);
        targetEnemy = null;
    }
    public void removeTarget(Enemy enemyToRemove)
    {
        enemyList.Remove(enemyToRemove);
        targetEnemy = null;
    }


    private void Shoot() 
    {
        Debug.Log("Im Shooting");
        targetEnemy.Damage(shotDamage, gameObject.GetComponent<Tower>());
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter trig");

        if(other.tag == ENEMYTAG) 
        {
            Enemy enemyScript = other.gameObject.GetComponent<Enemy>();

            enemyList.Add(enemyScript);
            enemyScript.Track(this);

            getTarget();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == ENEMYTAG)
        {
            Enemy enemyScript = other.gameObject.GetComponent<Enemy>();

            enemyList.Remove(enemyScript);
            enemyScript.UnTrack(this);

            getTarget();
        }
    }
}
