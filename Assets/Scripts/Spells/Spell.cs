using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spell : MonoBehaviour
{
    public virtual string spellName { get; set; }
    public virtual float damage { get; set; }
    public virtual float energyCost { get; set; }
    public virtual float price { get; set; }
    public bool locked;

    public static Text MessageBox;

    protected virtual void Start() {
        
    }


    public void kostantin()
    {
        locked = true;
    }

    public virtual void UseSpell()
    {
       // Debug.Log("Default Spell Used");
    }

    public void ApplyDamage(Collider2D collider)
    {
        collider.TryGetComponent<Enemy>(out var enemy);
        enemy.TakeDamage(damage);
    }

    public void Choose()
    {
        SpellSelection.Instance.ShowMessage(spellName + " have chosen");
        SpellSelection.currentSpell = this;
    }
    public void Unlock(GameObject gameObject)
    {

        if (Player.energy < price)
        {
            SpellSelection.Instance.ShowMessage("Not Enough Energy");
        }
        else
        {
            SpellSelection.Instance.ShowMessage(spellName + " unlocked");

            locked = false;
            Player.energy -= price;
            gameObject.SetActive(false);
        }

    }
}
