using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {

    [SerializeField]
    int AsteroidCount = 50;

    [SerializeField]
    List<GameObject> Asteroids; 

    [SerializeField]
    GameObject[] AsteroidPrefabs;

    [SerializeField]
    int range = 10;

    // Use this for initialization
    void Start () {
        Asteroids = new List<GameObject>();
	}

    // Update is called once per frame
    void Update()
    {
        if (Asteroids.Count < AsteroidCount)
        {
            SpawnAsteroid();
        }

        for(int i = 0; i < Asteroids.Count; i++)
        {
            CleanUp(Asteroids[i]);
        }
    }

    void SpawnAsteroid()
    {
        //get spawn point
        Vector3 point = GetRandomPoint(range);
        //spawn asteroid
        GameObject a = Instantiate(GetPrefab(), transform.position + point, Quaternion.identity);
        Asteroids.Add(a);
    }

    GameObject GetPrefab()
    {
        //get a random prefab to instanciate
        int choice = Random.Range(0, AsteroidPrefabs.Length);

        return AsteroidPrefabs[choice];
    }

    Vector3 GetRandomPoint(float num)
    {
        float randX = Random.Range(-num, num);
        float randY = Random.Range(-num, num);
        float randZ = Random.Range(-num, num);
        Vector3 position = new Vector3(randX, randY, randZ);
        return position;
    }

    void CleanUp(GameObject a)
    {
        if(Vector3.Distance(a.transform.position, transform.position) > range)
        {
            //IF out of range delete asteroid
            Asteroids.Remove(a);
            a.GetComponent<Asteroid>().Die();
        }
    }
}
