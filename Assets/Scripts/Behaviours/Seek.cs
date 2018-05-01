using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehaviour
{
    public GameObject targetGameObject = null;
    public Vector3 target = Vector3.zero;
    public GameObject bulletPrefab;
    float reloadTime = 0.5f;
    
    public void OnDrawGizmos()
    {
        if (isActiveAndEnabled && Application.isPlaying)
        {
            Gizmos.color = Color.cyan;
            if (targetGameObject != null)
            {
                target = targetGameObject.transform.position;
            }
            Gizmos.DrawLine(transform.position, target);
        }
    }
    
    public override Vector3 Calculate()
    {
        return boid.SeekForce(target);    
    }

    public void Update()
    {
        if (targetGameObject == null)
        {
            targetGameObject = GameObject.Find("Player");
        }

        reloadTime -= Time.deltaTime;
        if (targetGameObject != null)
        {
            target = targetGameObject.transform.position;
        }
        
        if(reloadTime < 0 && (Vector3.Dot(transform.forward, targetGameObject.transform.forward) > 0.8f || Vector3.Dot(transform.forward, targetGameObject.transform.forward) < -0.8f))
        {
            reloadTime = 0.5f;
            Shoot();
        }

    }

    void Shoot()
    {
        Vector3 gunPoint = transform.GetChild(3).transform.position;
        Instantiate(bulletPrefab, gunPoint + (transform.forward * 5), transform.rotation);
    }
}