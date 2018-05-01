using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    float moveSpeed = 50;
    float pitchTurnspeed = 0.5f;
    float rollTurnspeed = 0.8f;
    bool Slowing;

    float reloadTime = 0;
    public GameObject bullet;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float pitch = pitchTurnspeed * Input.GetAxis("Vertical");
        float roll = rollTurnspeed * Input.GetAxis("Horizontal");
        transform.Rotate(pitch, 0, -roll);

        transform.position += (transform.forward * moveSpeed) * Time.deltaTime;


        if(Input.GetAxis("Fire1") > 0 && reloadTime <= 0)
        {
            Shoot();
        }

        if (reloadTime > 0)
            reloadTime -= Time.deltaTime;
	}

    void Shoot()
    {
        Instantiate(bullet, transform.position, transform.rotation);
        reloadTime = 0.5f;
    }
}
