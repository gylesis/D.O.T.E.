using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Enemy {

    [SerializeField]
    int height = 10;

    Animator animator;

    Vector3 targetPos;

    Vector3 touchPos;

    bool allowToFly = true;

    void Start() {
        Nname = "Dragon";
        hp = 250;
        maxHp = hp;
        damage = 50;
        speed = 5;
        energyReward = 125;
        Initialization(gameObject);
        targetPos = Castle.pos;
        animator = GetComponent<Animator>();

        height = Random.Range(10, 20);

        distanceToCastle = Vector3.Distance(transform.position, Castle.pos);       
    }

    public override void MovingToCastle() {
        transform.position = new Vector3(transform.position.x, height, transform.position.z);
        base.MovingToCastle();
    }

    void HpBarControl() {
        var hpbar = HpBar.transform.GetChild(0);
        hpbar.transform.localScale = new Vector3(hp / maxHp, hpbar.transform.localScale.y, hpbar.transform.localScale.z);
    }

    void Update() {
        distanceToCastle = Vector3.Distance(transform.position, Castle.pos);
        MovingToCastle();
        HpBarControl();
    }

    IEnumerator DashToCastle() {
        while (distanceToCastle > distanceToCastle / 2) {
            yield return null;

            transform.position = Vector3.Lerp(transform.position, targetPos, 0.002f);
            if (allowToFly) {
                break;
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.CompareTag("Castle")) {
            allowToFly = false;
            touchPos = collision.ClosestPoint(transform.position);
            targetPos = (Castle.pos - touchPos) * 2f;
            StartCoroutine(DashToCastle());

            Castle.TakeDamage(damage);

        }
    }

    private void OnTriggerExit2D(Collider2D collision) {

        if (collision.CompareTag("Castle")) {
            direction *= -1;
            allowToFly = true;
            transform.localScale = new Vector3(transform.localScale.x * direction, transform.localScale.y, transform.localScale.z);
        }
    }

}
