using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyRay : Spell
{
    public static Spell energyRay;
    float timer;
    ParticleSystem _particleSystem;
    bool enabled = false;

    private void Start()
    {
        kostantin();
        _particleSystem = GetComponent<ParticleSystem>();

        spellName = "Disruption Ray";
        damage = 25;
        energyCost = 10;
        price = 150;
        energyRay = this;
    }

    public override void UseSpell()
    {
        base.UseSpell();
        timer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Mouse0) && Player.energy > energyCost)
        {
            if (timer > 0.2f) {
                timer = 0;
            }
            else return;

            Player.energy -= energyCost;
            _particleSystem.Play();
            Player.rb.AddForce(new Vector2(-transform.forward.x, 0) * 50 , ForceMode2D.Force);
            Player.rb.AddForce(new Vector2(0, -transform.forward.y) / 2, ForceMode2D.Impulse);


            List<Collider2D> colliders = new List<Collider2D>();
            transform.GetChild(0).GetComponent<BoxCollider2D>().OverlapCollider(new ContactFilter2D(), colliders);

            if (colliders.Count == 0) return;
            foreach (Collider2D enemy in colliders)
            {
            //    Debug.Log(enemy.name);
                ApplyDamage(enemy);
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && !enabled)
        {
            enabled = true;
            _particleSystem.Play();
        }
        else
        {
            enabled = false;
            _particleSystem.Stop();
        }
        
    }
}
