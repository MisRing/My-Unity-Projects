using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Cntrl : MonoBehaviour
{
    public float strBonus, agBonus, acBonus;

    public int damage;
    public float atackSpeed = 0.5f, criticalChance = 0;

    public string Type;

    public Character_cntrl player;

    public delegate void Atack(Character_cntrl Character);
    public Atack CallAtackMethod;

    float timer = 0;
    public void Atack_()
    {
        if(timer <= 0)
        {
            CallAtackMethod.Invoke(player);
            timer = atackSpeed;
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;
    }
}
