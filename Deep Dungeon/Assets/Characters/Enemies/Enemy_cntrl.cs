using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_cntrl : MonoBehaviour
{

    public string Name = "———";
    
    public int maxhp = 0, hp = 0, armor = 0;
    public Transform HpBar;

    public delegate void WhenDamageGets(int damage, int damageWithArmor, int armor);
    public WhenDamageGets WhenDamageGets_void;

    public delegate void Death();
    public Death Death_void;

    public GameObject damageText;

    public void TakeDamage (int damage, float criticalChance)
    {
        if (HpBar != null)
            HpBar.parent.gameObject.SetActive(true);

        int damageWithArmor = 0;
        Color collor = new Color(0,0,0,0);
        if(Random.Range(0f, 100f) < criticalChance)
        {
            damageWithArmor = Mathf.RoundToInt(damage * 1.5f);
            collor = new Color(225, 206, 0);
        }
        else
        {
            damageWithArmor = Mathf.RoundToInt(damage - armor * 0.5f);
        }
        damageWithArmor = (damageWithArmor <= 0) ? 1 : damageWithArmor;
        hp -= damageWithArmor;
        hp = (hp < 0) ? 0 : hp;

        GameObject g = GameObject.Instantiate(damageText);
        g.transform.position = transform.position + new Vector3(Random.Range(-1, 2), 0.6f, 0);
        g.transform.GetChild(0).GetComponent<Text>().text = "-" + damageWithArmor.ToString();
        if (collor.a != 0)
            g.transform.GetChild(0).GetComponent<Text>().color = collor;
        g.GetComponent<DamageText>().Moovevv();

        StartCoroutine(RedDamage());

        if(HpBar != null)
            HpBar.localScale = new Vector3((float)hp / (float)maxhp, 1, 1);

        if (WhenDamageGets_void != null)
        {
            WhenDamageGets_void(damage, damageWithArmor, armor);
        }
	}

    public void Update ()
    {
		if(hp <= 0 && maxhp != -1)
        {
            Death_void.Invoke();
        }
	}

    public IEnumerator RedDamage()
    {
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.35f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
