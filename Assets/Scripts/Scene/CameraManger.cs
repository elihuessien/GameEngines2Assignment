using UnityEngine;

public class CameraManger : MonoBehaviour {

    int position = 0;
    bool sceneChanging;
    [SerializeField]
    float changeRate = 4;

    float time;
	// Use this for initialization
	void Start () {
        transform.GetChild(position).gameObject.SetActive(true);
        time = changeRate;
    }
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;

        if(time <= 0)
        {
            MoveToNextCam();
            time = changeRate;
        }
	}

    public void MoveToNextCam()
    {
        transform.GetChild(position).gameObject.SetActive(false);

        if (position + 1 == transform.childCount)
            position = 1;
        else
            position++;

        transform.GetChild(position).gameObject.SetActive(true);
    }
}
