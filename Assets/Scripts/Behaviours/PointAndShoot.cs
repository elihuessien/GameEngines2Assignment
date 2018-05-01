using UnityEngine;

public class PointAndShoot : MonoBehaviour {
    Transform player;
    public GameObject bulletPrefab;
    float reloadTime;

	// Use this for initialization
	void Awake () {
        player = GameObject.Find("Player").transform;
        transform.LookAt(player);
    }
	
	// Update is called once per frame
	void Update () {
        reloadTime -= Time.deltaTime;

        if(reloadTime <= 0 )
        {
            reloadTime = 0.25f;
            Shoot();
        }
	}

    void Shoot()
    {
        Vector3 gunPoint = transform.GetChild(3).transform.position;
        Instantiate(bulletPrefab, gunPoint, transform.rotation);
    }
}
