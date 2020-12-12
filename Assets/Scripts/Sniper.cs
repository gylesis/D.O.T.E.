using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Enemy {
    bool allowToWalk = true;

    [SerializeField]
    enum Target {
        Castle, Player
    }

    [SerializeField]
    Target target;

    bool canAttack = true;

    [SerializeField]
    GameObject bullet;
    public static Vector3 directionForBullet;

    Animator animator;

    float lastTimeShoot;
    float bulletsPerSecond = 6f;

    int bulletShot = 0;

    private void Start() {
        hp = 100;
        maxHp = hp;
        speed = 5;
        damage = 50;
        energyReward = 150;
        Initialization(gameObject);

        animator = GetComponent<Animator>();

        distanceToCastle = Vector3.Distance(transform.position, Castle.pos);
    }

    public override void MovingToCastle() {


        if (!allowToWalk) {
            return;
        }
        else if (distanceToCastle > 50) {
            animator.SetTrigger("Walk");
            base.MovingToCastle();

            if (distanceToPlayer < 30 && canAttack) {
                canAttack = false;
                target = Target.Player;
                animator.SetTrigger("Attack");
                Ataka();
            }

            else if (distanceToPlayer > 40 && distanceToPlayer < 41) {
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
        if (distanceToCastle < 50 && canAttack) {
            canAttack = false;
            target = Target.Castle;
            animator.SetTrigger("Attack");
            Ataka();
        }
    }

    private void Update() {
        distanceToCastle = Vector3.Distance(transform.position, Castle.pos);
        distanceToPlayer = Vector3.Distance(transform.position, Player.player.position);
        MovingToCastle();
        HpBarControl();
    }

    void HpBarControl() {
        var hpbar = HpBar.transform.GetChild(0);
        hpbar.transform.localScale = new Vector3(hp / maxHp, hpbar.transform.localScale.y, hpbar.transform.localScale.z);
    }

    void Attack(Vector3 targetPos) {
        if (Time.time - lastTimeShoot > 1 / bulletsPerSecond) {
            if (bulletShot > 3) {
                lastTimeShoot = 1.5f + Time.time;
                bulletShot = 0;
                return;
            }
            bulletShot++;
            directionForBullet = (targetPos - transform.position).normalized;

            Bullet patron = Instantiate(bullet, transform.position, Quaternion.Euler(directionForBullet - new Vector3(0, 0, 0))).GetComponent<Bullet>();

            patron.SetDirection(directionForBullet, damage);

            lastTimeShoot = Time.time;
        }
    }

    void Ataka() {
        StartCoroutine(Attack());
    }

    IEnumerator Attack() {
        yield return new WaitForSeconds(0.5f);
        switch (target) {
            case Target.Castle:
                if (transform.position.x > Castle.pos.x) {
                    direction = 1;
                }
                else {
                    direction = -1;
                }
                Attack(Castle.pos);
                target = Target.Castle;
                break;

            case Target.Player:
                if (transform.position.x > Player.player.position.x) {
                    direction = 1;
                }
                else {
                    direction = -1;
                }
                Attack(Player.player.position);
                target = Target.Player;
                break;
        }
        canAttack = true;
        transform.localScale = new Vector3(transform.localScale.x * direction, transform.localScale.y, transform.localScale.z);
    }

    private void OnDrawGizmos() {
        var targetPos = Castle.pos;
        directionForBullet = (targetPos - transform.position).normalized;

        Gizmos.DrawLine(transform.position, targetPos);
    }

}
