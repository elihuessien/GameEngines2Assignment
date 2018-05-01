using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : SteeringBehaviour {

    GameObject path;
    public string nextSceneName;
    bool sceneChanging = false;
    Vector3 Waypoint;
    public GameObject bulletPrefab;
    float reloadTime = 0.5f;

    int counter;

    Spawner sp = null; 
    
    List<Transform> Enemies = new List<Transform>();

    public void OnDrawGizmos()
    {
        if (isActiveAndEnabled && Application.isPlaying)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, Waypoint);
        }
    }

    public void Start()
    {
        path = GameObject.Find("Path");
        counter = 0;
        //error check
        if(path == null)
        {
            Debug.Log("No path found");
        }
        else
        {
            Waypoint = GetWaypoint();
        }

        if(GameObject.Find("Spawner"))
            sp = GameObject.Find("Spawner").GetComponent<Spawner>();
    }


    public void Update()
    {
        reloadTime -= Time.deltaTime;
        if (sp != null)
        {
            //if we can shoot at an enemy then do it
            foreach (GameObject s in sp.fleet)
            {
                if (reloadTime < 0 && Vector3.Dot(transform.forward, s.transform.forward) < -0.8f)
                {
                    reloadTime = 0.5f;
                    Shoot();
                }
            }
        }
    }
    public override Vector3 Calculate()
    {
        if (!sceneChanging)
        {
            if (path != null && Vector3.Distance(transform.position, Waypoint) < 20)
            {
                //if we have reached the end then next scene
                if (counter == path.transform.childCount)
                {
                    sceneChanging = true;
                    SceneController sc = GameObject.Find("SceneManager").GetComponent<SceneController>();
                    sc.FadeAndLoadScene(nextSceneName);
                }
                else
                {
                    Waypoint = GetWaypoint();
                }
            }
            //go to the point
            return boid.SeekForce(Waypoint);
        }
        //go no where
        return transform.position;
    }


    public Vector3 GetWaypoint()
    {
        //get the child's position
        Vector3 location = path.transform.GetChild(counter).transform.position;
        counter++;
        return location;
    }

    void Shoot()
    {
        //from three guns
        Vector3 gunPoint = transform.GetChild(6).transform.position;
        Instantiate(bulletPrefab, gunPoint + (transform.forward * 5), transform.rotation);

        gunPoint = transform.GetChild(7).transform.position;
        Instantiate(bulletPrefab, gunPoint + (transform.forward * 5), transform.rotation);

        gunPoint = transform.GetChild(8).transform.position;
        Instantiate(bulletPrefab, gunPoint + (transform.forward * 5), transform.rotation);
    }
}
