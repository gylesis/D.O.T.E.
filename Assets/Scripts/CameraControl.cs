using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 destination = Player.Instance.transform.position;
        destination.y = Mathf.Max(destination.y, 6);
        destination.z = -5;
        transform.position =  Vector3.Lerp(transform.position, destination, 0.1f);
    }
}
