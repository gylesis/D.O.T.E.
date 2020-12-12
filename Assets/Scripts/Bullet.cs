using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    Vector3 direction;
    [SerializeField]
    float speed = 50f;
    float damage;

    [SerializeField]
    private AudioSource shootSound;

    private void Start() {
        shootSound.Play();
    }

    public void SetDirection(Vector3 _direction, float _damage) {
        direction = _direction;
        Destroy(gameObject, 5f);
        damage = _damage;
    }

    void FixedUpdate() {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Castle")) {
            Castle.TakeDamage(damage);
            Destroy(Instantiate(GameLogic.Instance.expolionPaticles, collision.transform.position, Quaternion.identity),0.5f);
            Destroy(gameObject,0.1f);
        }
        else if (collision.CompareTag("Player")) {
            Player.TakeDamage(damage);
            Destroy(Instantiate(GameLogic.Instance.expolionPaticles, collision.transform.position, Quaternion.identity), 0.5f);
            Destroy(gameObject,0.1f);
        }

    }
}
