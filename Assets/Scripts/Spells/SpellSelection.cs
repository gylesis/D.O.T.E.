using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellSelection : MonoBehaviour
{
    public static SpellSelection Instance;
    public static Spell currentSpell;
    [SerializeField]
    private Transform cursor;
    [SerializeField]
    private GameObject[] palki;

    public static Text MessageBox;
    public Text nameBox;
    public Text damageBox;
    public Text energyCostBox;
    public Text priceBox;

    private void Start()
    {
        Instance = this;       
    }

    private void Update()
    {
        float x = Input.mousePosition.x - Screen.width / 2;
        float y = Input.mousePosition.y - Screen.height / 2;

        Vector2 direction = new Vector2(-x, -y);
        direction.Normalize();

        Vector3 newRotation = cursor.rotation.eulerAngles;

        if (direction.y >= 0)
        {
            newRotation.z = 180 - Mathf.Asin(direction.x) * 180 / Mathf.PI;
            cursor.rotation = Quaternion.Euler(newRotation);
        }
        else
        {
            newRotation.z = Mathf.Asin(direction.x) * 180 / Mathf.PI;
            cursor.rotation = Quaternion.Euler(newRotation);
        }


        switch (newRotation.z + 72)
        {
            case float n when (n >= 228 * 0 && n < 72):

                break;

            case float n when (n >= 72 && n < 144):
                Select(EnergyRay.energyRay, palki[1]);
                break;

            case float n when (n >= 144 && n < 216):
                Select(EMP.emp, palki[2]);
                break;

            case float n when (n >= 216 && n < 288):
                
                break;

            case float n when (n >= 288 || n < 72 * 0):
                Select(Zap.zap, palki[4]);
                break;
        }
    }

    void Select(Spell spell, GameObject gameObject)
    {
        ShowInfo(spell);
        if (Input.GetKey(KeyCode.Mouse0) && !spell.locked)
            spell.Choose();
        else if (Input.GetKey(KeyCode.Mouse1) && spell.locked)
            spell.Unlock(gameObject);
    }

    void ShowInfo(Spell spell)
    {
        nameBox.text = spell.spellName;
        damageBox.text = "Damage:" + spell.damage.ToString();
        energyCostBox.text = "Energy per use:" + spell.energyCost.ToString();
        priceBox.text = "Price is " + spell.price.ToString() + " energy";
    }

    public void ShowMessage(string message)
    {
        if (MessageBox == null) MessageBox = GameObject.Find("MsgBox").GetComponent<Text>();

        MessageBox.text = message;
        Color color = Color.white;
        MessageBox.color = color;
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(0.1f);
        Color color = Color.white;
        while (MessageBox.color.a > 0)
        {
            color.a -= 0.02f;
            MessageBox.color = color;
            yield return null;
        }
    }
}
