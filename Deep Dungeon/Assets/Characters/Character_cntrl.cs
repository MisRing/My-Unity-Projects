using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_cntrl : MonoBehaviour
{

    public Animator anim;
    public Rigidbody2D rb;
    private float playerSpeed, jumpPower, invulnerability = 0.8f, invulnerability_timer = 0;
    private bool OnGround = true;
    public LayerMask groundLayer;
    public int coins = 0;
    public bool Alife = true;

    public string Name;
    public int HP, STR, AG, AC, ARMOR;
    [HideInInspector]
    public int curent_HP;

    [HideInInspector]
    public float x_direction;
    [HideInInspector]
    public bool jump;
    private Status_cntrl stat;

    public Transform hand;
    public Weapon_Cntrl weapon;

    void Start()
    {
        stat = FindObjectOfType<Status_cntrl>();
        playerSpeed = AG * 2;
        jumpPower = AG * 4;
        curent_HP = HP;

        stat.UpdateHP(curent_HP, HP);
    }

    public void SetDirection(float dir)
    {
        x_direction = dir;
    }

    public void TakeDamage(int damage)
    {
        if (invulnerability_timer <= 0 && Alife)
        {
            int damageWithArmor = Mathf.RoundToInt(damage - ARMOR * 0.5f);
            damageWithArmor = (damageWithArmor <= 0) ? 1 : damageWithArmor;
            curent_HP -= damageWithArmor;
            curent_HP = (curent_HP < 0) ? 0 : curent_HP;

            stat.UpdateHP(curent_HP, HP);

            StartCoroutine(RedDamage());

            invulnerability_timer = invulnerability;
        }
    }

    void Update()
    {
        if (Alife)
        {
            if (atack)
                weapon.Atack_();

            invulnerability_timer -= Time.deltaTime;
            float x = x_direction;

            Walk(x);

            if (x != 0)
            {
                Flip(x);
            }

            OnGround = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - 0.4f), 0.55f, groundLayer);

            if(!OnGround)
                anim.SetInteger("State", 3);

            if (jump)
            {
                Jump();
            }

            if (curent_HP <= 0)
            {
                Dead();
            }

            CheckRound();
        }
    }

    public void Flip(float x)
    {
        if (x > 0 && transform.localScale.x == -1)
            transform.localScale = new Vector3(1, 1, 1);
        else if (x < 0 && transform.localScale.x == 1)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    public void Walk(float x)
    {
        if(x != 0)
            anim.SetInteger("State", 1);
        else
            anim.SetInteger("State", 0);

        float x_dir = playerSpeed * x;

        rb.velocity = new Vector2(x_dir, rb.velocity.y);
    }

    public void _jump(bool value)
    {
        jump = value;
    }

    void Jump()
    {
        if (OnGround)
        {
            anim.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
    }

    public void TakeWeapon()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(new Vector2(transform.position.x, transform.position.y), 0.75f, Vector2.zero);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.tag == "Weapon")
            {
                if (hit.collider.gameObject.GetComponent<Weapon_Cntrl>() != weapon)
                {
                    weapon.transform.parent = null;
                    weapon.transform.position = transform.position;
                    //weapon.GetComponent<Rigidbody2D>()
                    weapon.GetComponent<Collider2D>().isTrigger = false;
                    weapon.transform.localScale = new Vector3(1, 1, 1);

                    GameObject newWeapon = hit.collider.gameObject;
                    newWeapon.transform.parent = hand;
                    newWeapon.transform.localPosition = Vector2.zero;
                    //newWeapon.GetComponent<Rigidbody2D>()
                    newWeapon.GetComponent<Collider2D>().isTrigger = true;
                    newWeapon.transform.localScale = new Vector3(1, 1, 1);
                    weapon = newWeapon.GetComponent<Weapon_Cntrl>();

                    stat.ChangeWeapon(weapon.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite);

                    break;
                }
            }
        }
    }

    void CheckRound()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(new Vector2(transform.position.x, transform.position.y), 0.75f, Vector2.zero);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.tag == "Coin")
            {
                coins++;
                Destroy(hit.collider.gameObject);
                stat.CoinCount(coins);
            }
        }
    }

    bool atack = false;
    public void Atack(bool _atack)
    {
        atack = _atack;
    }

    public void Dead()
    {
        Alife = false;
        anim.SetInteger("State", 4);
    }

    public Canvas PUIC, PDC;

    public void Death2()
    {
        anim.SetInteger("State", 5);
        PUIC.gameObject.SetActive(false);
        PDC.gameObject.SetActive(true);
    }

    public IEnumerator RedDamage()
    {
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.35f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
