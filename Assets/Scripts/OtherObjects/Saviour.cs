using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saviour : MonoBehaviour {
    [SerializeField]
    GameObject missilePrefab;

    List<Transform> targets = new List<Transform>();

    float reload = 5f;
    int position = 0;
	// Use this for initialization
	void Start () {
        targets.Clear();

        GameObject spawners = GameObject.Find("Spawners");
        for (int i = 0; i < spawners.transform.childCount; i++)
        {
            Transform g = spawners.transform.GetChild(i);
            targets.Add(g);
        }
	}
	
	// Update is called once per frame
	void Update () {
        reload -= Time.deltaTime;

        if(reload <= 0)
        {
            Shoot();
            reload = 0.5f;
        }
	}

    void Shoot()
    {
        if(position < targets.Count)
        {
            GameObject g = Instantiate(missilePrefab, transform.position, Quaternion.identity);
            g.GetComponent<Missile>().target = targets[position];
            position++;
        }
    }
}
