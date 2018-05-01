using UnityEngine;

public class Missile : MonoBehaviour {
    public Transform target;
    int speed = 10;

    bool arrived;
	// Use this for initialization
	void Start () {
        transform.LookAt(target);
	}
	
	// Update is called once per frame
	void Update () {
        if (!arrived)
        {
            transform.position += transform.forward * speed;

            if (Vector3.Distance(transform.position, target.position) < 10)
            {
                target.GetComponent<Spawner>().die = true;
                arrived = true;
            }
        }
        else
        {
            Destroy(gameObject, 3);
        }
    }
}
