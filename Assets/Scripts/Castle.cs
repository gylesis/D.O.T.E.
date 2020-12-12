using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour {
    [SerializeField]
    public static float hp;

    float maxHp;

    private SpriteRenderer _hpBar;

    [HideInInspector]
    public CircleCollider2D circleCollider;

    [HideInInspector]
    public static Vector3 pos;

    private void Start() {
        circleCollider = transform.GetChild(1).GetComponent<CircleCollider2D>();
        _hpBar = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();

        pos = circleCollider.transform.position;
        hp = 5000;
        maxHp = hp;
    }

    private void Update() {
        ShowHp();
    }

    private void ShowHp() {
        _hpBar.transform.localScale = new Vector3(hp / maxHp, _hpBar.transform.localScale.y, 0);
    }

    public static void TakeDamage(float damage) {
        hp -= damage;
        if (hp <= 0) {
            hp = 0;
            GameLogic.GameOver();
        }
    }

}

