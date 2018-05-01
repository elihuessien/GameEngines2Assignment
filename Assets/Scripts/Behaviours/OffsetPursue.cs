using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetPursue : SteeringBehaviour {

    public Boid leader;
    public bool isLeader = false;
    public Vector3 offset;
    Vector3 worldtarget;


    // Use this for initialization
    public void OnDrawGizmos()
    {
        if (isActiveAndEnabled && Application.isPlaying)
        {
            Gizmos.color = Color.grey;
            Gizmos.DrawLine(transform.position, worldtarget);
        }
    }


    void Start () {
        offset = transform.position - leader.transform.position;
        offset = Quaternion.Inverse(leader.transform.rotation) * offset;
    }

    // Update is called once per frame
    void Update () {
        //if we are the leader we don't need this script
        if(isLeader)
        {
            boid.behaviours.Remove(this);
            Destroy(this);
        }
    }

    public override Vector3 Calculate()
    {
        //if the leader is null, then get a new leader
        if (leader == null)
        {
            leader = GameObject.Find("Spawner").GetComponent<Spawner>().GetLeader();
        }

        worldtarget = leader.transform.TransformPoint(offset);
        float dist = Vector3.Distance(worldtarget
            , transform.position);
        float time = dist / boid.maxSpeed;

        Vector3 targetPos = worldtarget + (leader.velocity * time);
        return boid.ArriveForce(targetPos, 10);
    }


}
