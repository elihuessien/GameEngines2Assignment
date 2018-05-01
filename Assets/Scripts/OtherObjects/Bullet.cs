using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour {
    public GameObject explosion;
    int speed = 10;
    bool final = false;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, 3);
        Scene sc = SceneManager.GetActiveScene();
        if (sc.name == "EndingScene")
        {
            final = true;
        }
    }

    public void Update()
    {
        transform.position += transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            //player should never die
        }
        else if(other.gameObject.name.Contains("Node") || other.gameObject.name == "Path")
        {
            return;
        }
        else if(!final)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);

            //if we hit a chaser
            if (other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }
            else //if we hit another bullet or anything else
            {
                Destroy(other.gameObject);
            }
            Destroy(gameObject);
        }
        else if(other.name.Contains("Chaser"))
        {
            //ignore our ships in the end shooting scene
            return;
        }
        
    }
}
