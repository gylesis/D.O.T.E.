using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Zap : Spell {

    public static Spell zap;

    float timer;

    [SerializeField]
    ParticleSystem particleSystem;

    protected override void Start() {
        base.Start();
        kostantin();
        spellName = "Zap";
        damage = 50;
        energyCost = 30;
        zap = this;
        price = 200;
    }


    public override void UseSpell() {

        timer += Time.deltaTime;


        if (Input.GetButton("Fire1") && Player.energy > energyCost) {

            if (timer > 0.5f) {
                timer = 0;
            }
            else return;

            Player.energy -= energyCost;

            Destroy(Instantiate(particleSystem, transform.position, Quaternion.Euler(0, 0, Player.rotationZ)).gameObject, 3f);

            Vector3 mousePos = MousePosition.GetMouseWorldPosition();
            Vector3 aimDirection = (mousePos - transform.position).normalized;


            RaycastHit2D hit = Physics2D.Raycast(transform.position, aimDirection,15f);

             Debug.Log(hit.collider);

            ApplyDamage(hit.collider);


            Destroy(Instantiate(GameLogic.Instance.expolionPaticles, hit.collider.transform.position, Quaternion.identity), 0.5f);
        }


    }

}






