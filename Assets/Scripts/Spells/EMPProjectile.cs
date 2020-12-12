using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPProjectile : MonoBehaviour {
    [SerializeField]
    ParticleSystem ps;
    [SerializeField]
    private GameObject Explosion;

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject == gameObject) {
            return;
        }
        Destroy(gameObject);
        Destroy(Instantiate(ps, transform.position, Quaternion.identity).gameObject, 0.2f);
        Destroy(Instantiate(Explosion, transform.position, Quaternion.identity), .2f);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject == gameObject) {
            return;
        }
        Destroy(gameObject);
        Destroy(Instantiate(ps, transform.position, Quaternion.identity).gameObject, 0.2f);
        Destroy(Instantiate(Explosion, transform.position, Quaternion.identity), .2f);
    }
}
