using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField]
    Transform target;
    [SerializeField]
    bool FollowChaser;
    [SerializeField]
    Vector3 preferedPos = new Vector3(0, 9, -30);

    //for tweeking
    [SerializeField]
    float movementDampening = 10;
    [SerializeField]
    float rotationDampening = 7;

    private void Awake()
    {

        //if you haven't given me an object to follow, I'll follw the Player
        if(target == null)
        {
            target = GameObject.Find("Player").transform;
        }

        if(FollowChaser)
        {
            target = GameObject.FindObjectOfType<Spawner>().GetLeader().transform;
        }
        //put us in the prefered position
        transform.position = target.position + preferedPos;
    }
    void LateUpdate()
    {
        Vector3 targetPos = target.position + (target.rotation * preferedPos);
        transform.position = Vector3.Lerp(transform.position, targetPos, movementDampening * Time.deltaTime);

        Quaternion targetRot = Quaternion.LookRotation(target.position - transform.position, target.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationDampening * Time.deltaTime);
    }
}
