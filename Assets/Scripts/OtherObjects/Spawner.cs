using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    float swirlRate = Mathf.PI/4;
    float radius;
    [SerializeField]
    float angle;
    [SerializeField]
    bool final = false;
    float z;
    [SerializeField]
    bool left;

    //variables used in layered spawn;
    int layer = 1;
    int numInThisLayer = 0;
    [SerializeField]
    int padding = 25;

    public GameObject shipPrefab;
    public GameObject bulletPrefab;
    public int fleetSize = 10;
    public List<GameObject> fleet = new List<GameObject>();

    float reloadTime = .25f;
    public bool die = false;
    // Use this for initialization
    void Start() {
        // spiral algorithm is radius = angle * 3;
        z = 0;
        radius = 0;

        fleet.Clear();

        if (!final)
            SwirlSpawn();
        else
            LayeredSpawn();
    }

    private void Update()
    {
        //cleanUp
        for(int i = fleet.Count-1; i > -1; i--)
        {
            if (fleet[i]==null)
            {
                fleet.Remove(fleet[i]);
            }
        }

        //death cleanUp
        if(die)
        {
            reloadTime -= Time.deltaTime;

            if(reloadTime<=0)
            {
                reloadTime = .1f;
                if(fleet.Count > 0)
                {
                    fleet[0].GetComponent<Boid>().Die();
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    void LayeredSpawn()
    {
        for (int i = 0; i < fleetSize; i++)
        {
            Vector3 offset = GetNextLayeredPosition();
            GameObject ship = Instantiate(shipPrefab, transform.position + offset, Quaternion.identity);
            numInThisLayer++;

            ship.AddComponent<PointAndShoot>();
            ship.GetComponent<PointAndShoot>().bulletPrefab = bulletPrefab;
            fleet.Add(ship);
        }
    }

    void SwirlSpawn()
    {
        for (int i = 0; i < fleetSize; i++)
        {
            Vector3 offset = GetNextSwirlPosition();
            GameObject ship = Instantiate(shipPrefab, transform.position + offset, Quaternion.identity);

            //if we have a leader
            if(fleet.Count > 1)
            {
                ship.AddComponent<OffsetPursue>();
                ship.GetComponent<OffsetPursue>().leader = fleet[0].GetComponent<Boid>();
            }

            ship.AddComponent<Seek>();
            ship.GetComponent<Seek>().bulletPrefab = bulletPrefab;
            fleet.Add(ship);
        }
	}

    public Boid GetLeader()
    {
        //if there is a rookie leader then prep him
        if (fleet[0].GetComponent<OffsetPursue>())
        {
            fleet[0].GetComponent<OffsetPursue>().isLeader = true;
        }

        //tell everyone the leader
        Boid leader = fleet[0].GetComponent<Boid>();
        return leader;
    }

    Vector3 GetNextSwirlPosition()
    {
        //swirl algorithm
        radius = 3 * angle;
        float x = radius * Mathf.Cos(angle);
        float y = radius * Mathf.Sin(angle);

        //start at z of zero
        Vector3 swirlPosition = new Vector3(x, y, z);

        //update variables
        z += 10;
        angle += swirlRate;

        return swirlPosition;
    }


    Vector3 GetNextLayeredPosition()
    {
        //prepare variables
        if(numInThisLayer == layer)
        {
            numInThisLayer = 0;
            layer++;
        }
        
        //first position
        if(layer == 1)
        {
            return Vector3.zero;
        }
        else
        {
            float width = (layer-1) * (padding*2);
            float interval = width / (layer-1);

            float z;

            if (left)
                z = (padding * (layer-1));
            else
                z = -(padding * (layer - 1));

            float y = (-padding * (layer-1)) + ((padding*2) * numInThisLayer);

            return new Vector3(0, y, z);
        }
    }
}
