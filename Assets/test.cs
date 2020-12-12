using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

/*
    private void OnParticleTrigger() {
        Debug.Log("stuk!");
        Destroy(gameObject);
    }*/

    private void OnParticleCollision(GameObject other) {
        Debug.Log(other.tag);
        Debug.Log("stuk!");
        Destroy(gameObject);
    }

}
