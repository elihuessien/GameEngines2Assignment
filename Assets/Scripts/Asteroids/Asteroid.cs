using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {
    [SerializeField]
    GameObject trailPrefab;
    [SerializeField]
    GameObject ExplosionPrefab;
    [SerializeField]
    int rotationRate = 3;

    float interval;

    Vector3 direciton;

    GameObject trail;

	// Use this for initialization
	void Awake () {
        Reset();

        //make my trail trail
        trail = Instantiate(trailPrefab, transform.position, Quaternion.identity);
    }

	// Update is called once per frame
	void Update () {
        interval -= Time.deltaTime;
        trail.transform.position = transform.position;


        //rotate the asteroids
        transform.rotation *= Quaternion.AngleAxis(rotationRate * Time.deltaTime, transform.right);


        if (interval <=0)
        {
            Teleport();
        }
        transform.position += direciton;
	}

    void Reset()
    {
        //reset random values
        transform.rotation = Quaternion.Euler(GetRandomPoint(10));
        direciton = GetRandomPoint(1f);
        interval = Random.Range(3, 4);
        transform.localScale = new Vector3(10, 10, 10);
    }

    Vector3 GetRandomPoint(float num)
    {
        float randX = Random.Range(-num, num);
        float randY = Random.Range(-num, num);
        float randZ = Random.Range(-num, num);
        Vector3 position = new Vector3(randX, randY, randZ);
        return position;
    }

    void Teleport()
    {
        //shrink the game object
        transform.localScale -= new Vector3(2f,2f,2f);

        if(transform.localScale.x <= 0)
        {
            //tell current trail to die
            trail.GetComponent<AsteroidPoof>().Die();

            //move and reset
            transform.position += GetRandomPoint(50);
            Reset();

            //give me a new trail
            trail = Instantiate(trailPrefab, transform.position, Quaternion.identity);
        }
    }

    public void Die()
    {
        //tell current trail to die
        trail.GetComponent<AsteroidPoof>().Die();

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //make the explosion effect
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);

        //If it's an asteroid
        if (collision.gameObject.name.Contains("Asteroid"))
        {
            collision.gameObject.GetComponent<Asteroid>().Die();
        }
        else
        {
            //Destroy what collided with me
            Destroy(collision.gameObject);
        }

        //Destroy me
        Die();
    }
}
