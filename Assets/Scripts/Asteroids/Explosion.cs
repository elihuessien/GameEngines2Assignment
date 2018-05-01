using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    float durration = 2;
    void Start()
    {
        Destroy(gameObject, durration);
    }
}