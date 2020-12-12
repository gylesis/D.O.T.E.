using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy {

    [SerializeField]
    enum Target {
        Castle, Player
    }

    [SerializeField]
    bool allowToWalk = true;
    Animator animator;

    [SerializeField]
    Target target;

    bool canAttack = true;


    void Start() {
        Nname = "Zombie";
        damage = 25;
        hp = 200;
        maxHp = hp;
        speed = 4;
        energyReward = 200;
        Initialization(gameObject);
        animator = GetComponent<Animator>();
        target = Target.Castle;
    }

    public override void MovingToCastle() {


        if (!allowToWalk) {
            return;
        }
        else if (distanceToCastle > 25) {
            animator.SetTrigger("Walk");
            base.MovingToCastle();

            if (distanceToPlayer < 10 && canAttack) {
                canAttack = false;
                target = Target.Player;
                animator.SetTrigger("Attack");
                Ataka();
            }

            else if (distanceToPlayer > 25 && distanceToPlayer < 26) {
                target = Target.Castle;
                if (transform.position.x > Castle.pos.x) {
                    direction = 1;
                }
                else {
                    direction = -1;
                }
                transform.localScale = new Vector3(transform.localScale.x * direction, transform.localScale.y, transform.localScale.z);

            }
        }
        if (distanceToCastle < 25 && canAttack) {
            canAttack = false;
            target = Target.Castle;
            animator.SetTrigger("Attack");
            Ataka();
        }
    }

    void HpBarControl() {
        var hpbar = HpBar.transform.GetChild(0);
        hpbar.transform.localScale = new Vector3(hp / maxHp, hpbar.transform.localScale.y, hpbar.transform.localScale.z);
    }


    void Update() {
        distanceToCastle = Vector3.Distance(transform.position, Castle.pos);
        distanceToPlayer = Vector3.Distance(transform.position, Player.player.position);

        HpBarControl();
        MovingToCastle();

    }

    public void SetTrigger() {
        animator.SetTrigger("Idle");
    }

    IEnumerator Attack() {
        yield return new WaitForSeconds(1f);
        switch (target) {
            case Target.Castle:
                if (transform.position.x > Castle.pos.x) {
                    direction = 1;
                }
                else {
                    direction = -1;
                }
                Castle.TakeDamage(damage);
                target = Target.Castle;
                break;

            case Target.Player:
                if (transform.position.x > Player.player.position.x) {
                    direction = 1;
                }
                else {
                    direction = -1;
                }
                Player.TakeDamage(damage);
                target = Target.Player;
                break;
        }
        canAttack = true;
        transform.localScale = new Vector3(transform.localScale.x * direction, transform.localScale.y, transform.localScale.z);
    }

    void Ataka() {
        StartCoroutine(Attack());
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Castle")) {
            allowToWalk = false;
            // Attack();
        }
    }




}
