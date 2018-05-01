using UnityEngine;

public class FinalZoomCam : MonoBehaviour {
    bool sceneChanging = false;
    [SerializeField]
    int zoomOutDist;
    [SerializeField]
    float time;
    [SerializeField]
    string nextSceneName;

    float rate;
	// Use this for initialization
	void Start () {
        
	}

    // Update is called once per frame
    void Update()
    {
        if (!sceneChanging)
        {
            if (transform.position.x < -zoomOutDist)
            {
                sceneChanging = true;
                SceneController sc = GameObject.Find("SceneManager").GetComponent<SceneController>();
                sc.FadeAndLoadScene(nextSceneName);
            }
            else
            {
                rate = zoomOutDist / time * Time.deltaTime;
                transform.position -= new Vector3(rate, 0, 0);
            }
        }
    }
}
