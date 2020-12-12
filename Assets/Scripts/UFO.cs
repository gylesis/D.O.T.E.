using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    public GameObject projectile;
    Enemy ufo;

    public void Attack()
    {
        Instantiate(projectile, transform.parent.position - new Vector3(0, 1.15f), Quaternion.identity);
    }

    void Update()
    {
        transform.parent.position = Vector3.Lerp(transform.parent.position, Player.Instance.transform.position + new Vector3(0, 15), 0.001f);
    }
}
