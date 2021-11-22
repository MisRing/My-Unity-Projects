using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Cntrl : MonoBehaviour
{
    public float Angle = 120;
    public float swordLenth = 1;

    private Weapon_Cntrl WPc;
    private Animator anim;
    public void Start()
    {
        WPc = GetComponent<Weapon_Cntrl>();
        WPc.CallAtackMethod = Atack;
        anim = GetComponent<Animator>();
    }

    public void Atack(Character_cntrl Character)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(new Vector2(Character.gameObject.transform.position.x, Character.gameObject.transform.position.y),
                                                        swordLenth,Vector2.zero);

        foreach(RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Enemy")
                {
                    GameObject Enemy = hit.collider.gameObject;
                    float angle = Vector2.Angle(Vector2.right * Character.transform.localScale.x, hit.point - new Vector2(Character.gameObject.transform.position.x, Character.gameObject.transform.position.y));
                    if (Enemy.GetComponent<Enemy_cntrl>() != null && angle <= Angle/2)
                    {
                        Enemy_cntrl enemy = Enemy.GetComponent<Enemy_cntrl>();
                        int damage = Mathf.RoundToInt(WPc.damage + WPc.strBonus * WPc.player.STR + WPc.agBonus * WPc.player.AG + WPc.acBonus * WPc.player.AC);
                        enemy.TakeDamage(damage, WPc.criticalChance);
                    }
                }
            }
        }

        anim.SetTrigger("Atack");
    }
}
