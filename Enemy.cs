using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    const float WAYPOINTRANGE = 0.5f;

    [SerializeField] private string name;
    [SerializeField] private int healthPoints = 75;
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] List<Tower>  trackingTowers; // towers currently tracking me / im in  their list

    private Transform targetWaypoint;
    private int targetWaypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        targetWaypoint = Waypoints.points[targetWaypointIndex];
        transform.LookAt(targetWaypoint);
    }

    // Update is called once per frame
    void Update()
    {
        MoveLogic();    
    }

    private void MoveLogic() 
    {
        Vector3 dir = targetWaypoint.position - transform.position;
        dir.y = 0;

       // transform.Translate(dir.normalized * moveSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < WAYPOINTRANGE)
        {
            if(targetWaypointIndex >= Waypoints.points.Length - 1) 
            {
                Debug.LogError("Waypoint Error- Enemy has reached end of waypoints before destruction");
            }

            targetWaypointIndex++;
            targetWaypoint = Waypoints.points[targetWaypointIndex];
            transform.LookAt(targetWaypoint);
        }
    }

    public void Damage(int damage, Tower source) 
    {
        healthPoints -= damage;

        if(healthPoints < 1) 
        {
            Die(true);        
        }
    }

    public void Damage(int damage, Enemy source)
    {
        healthPoints -= damage;
    }

    public int GetHealthPoints() 
    {
        return healthPoints;
    }
    
    public void Track(Tower tower) 
    {
        trackingTowers.Add(tower);
    }
    public void UnTrack(Tower tower)
    {
        trackingTowers.Remove(tower);
    }

    public void Die(bool reward) 
    {
        foreach(Tower t in trackingTowers) 
        {
            t.removeTarget(this);
     
        }

        Destroy(gameObject);
    }    
}
