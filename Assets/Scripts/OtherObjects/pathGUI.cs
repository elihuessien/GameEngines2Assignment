using UnityEngine;

public class pathGUI : MonoBehaviour {
    bool Show = false;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.G))
        {
            Show = !Show;
        }
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if(Show)
        {
            Gizmos.DrawSphere(transform.position, 5);
        }
    }
}
