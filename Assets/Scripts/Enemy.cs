using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MovingToCastle {
    void MovingToCastle();
}
public interface OnDeath {
    void Death();
}

public class Enemy : MonoBehaviour {

    [HideInInspector]
    public string Nname;

    [HideInInspector]
    public float maxHp;

    [HideInInspector]
    public int damage;

    [HideInInspector]
    public float hp;

    [HideInInspector]
    public int speed;

    [HideInInspector]
    public SpriteRenderer sprite;

    [HideInInspector]
    public Rigidbody2D rb;

    public int direction;

    public float distanceToCastle;

    public GameObject HpBar;

    public float distanceToPlayer;

    public float energyReward;

    public void Initialization(GameObject gameObject) {
        rb = gameObject.transform.GetComponent<Rigidbody2D>();
        HpBar = gameObject.transform.GetChild(0).GetChild(0).gameObject;

        direction = gameObject.transform.position.x > 0 ? 1 : -1;
        transform.localScale = new Vector3(transform.localScale.x * -direction, transform.localScale.y, transform.localScale.z);
    }

    public virtual void MovingToCastle() {
        float moveBy = -direction * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
    }

    public void OnDeath() {
        EnemySpawner.killedEnemies++;
        if (EnemySpawner.killedEnemies >= EnemySpawner.needToKillEnemies) {
            GameLogic.Instance.Victory();
        }

        Player.energy += energyReward;
        if (Player.energy > Player.maxEnergy) Player.energy = Player.maxEnergy;

        EnemySpawner.spawnedEnemies.Remove(gameObject);
        Destroy(gameObject);
    }

    public virtual void TakeDamage(float damage) {
        hp -= damage;
        if (hp <= 0) {
            OnDeath();
        }
    }

}
