using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    Rigidbody2D rb;
    CircleCollider2D collider;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        while (transform.localScale.x < 1.5f)
        {
            Vector2 newScale = new Vector2(transform.localScale.x + 0.1f, transform.localScale.y + 0.1f);
            transform.localScale = newScale;
            yield return null;
        }

        List<Collider2D> colliders = new List<Collider2D>();
        collider.OverlapCollider(new ContactFilter2D(), colliders);

        if (colliders.Count == 0) StopCoroutine(Explode());
        foreach (Collider2D enemy in colliders)
        {
            EMP.emp.ApplyDamage(enemy);
        }
        Destroy(gameObject);
    }
}
