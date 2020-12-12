using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMP : Spell
{
    public static Spell emp;
    float timer;

    [SerializeField]
    GameObject projectile;
    [SerializeField]
    ParticleSystem particleSystem;

    private void Start()
    {
        kostantin();
        spellName = "EMP";
        damage = 75;
        energyCost = 50;
        emp = this;
        price = 175;
    }

    public override void UseSpell()
    {
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0) && Player.energy > energyCost)
        {
            Player.energy -= energyCost;
            if (timer <= 0.33) return;
            else timer = 0;

            GameObject currentProjectile = Instantiate(projectile, transform.position, Quaternion.identity);

            Vector3 mousePos = MousePosition.GetMouseWorldPosition();
            Vector3 aimDirection = (mousePos - transform.position).normalized;

            currentProjectile.GetComponent<Rigidbody2D>().AddForce(aimDirection * 40, ForceMode2D.Impulse);
        }
    }
}
