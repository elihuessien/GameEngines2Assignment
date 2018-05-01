using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidPoof : MonoBehaviour {

    public GameObject fireSpark;
    public GameObject dieSpark;


	// Use this for initialization
	void Awake () {
        fireSpark.SetActive(true);
        dieSpark.SetActive(false);
	}

    public void Die()
    {
        fireSpark.SetActive(false);
        dieSpark.SetActive(true);
        Destroy(gameObject, 1f);
    }
}
