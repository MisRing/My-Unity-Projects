using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSlime : MonoBehaviour
{
    Enemy_cntrl ENc;
    Transform player;

    public int state = 0;
    public float jumpForce = 10f;
    public int Damage = 1;
    private bool OnGround;
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private bool alife = true;

    private Animator anim;
    public float timer = 0;

    void Start()
    {
        ENc = GetComponent<Enemy_cntrl>();

        ENc.Death_void = Death;
        ENc.WhenDamageGets_void = WhenDamageTaked;

        anim = GetComponent<Animator>();
        rb = GetComponent <Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (alife)
        {
            timer += Time.deltaTime;

            if (player == null)
            {
                SeePlayer();
            }
            else
            {
                OnGround = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), 0.5f, groundLayer);
                if (state != 3)
                {
                    AtackWhenIn();

                    if (!OnGround)
                    {
                        state = 2;
                        anim.SetInteger("State", 2);
                    }
                    else
                    {
                        if (state != 1 && state != 2)
                            rb.velocity = Vector2.zero;

                        RaycastHit2D[] hits = Physics2D.CircleCastAll(new Vector2(transform.position.x, gameObject.transform.position.y), 1.25f, Vector2.zero);
                        bool atacked = false;
                        foreach (RaycastHit2D hit in hits)
                        {
                            if (hit.collider.GetComponent<Character_cntrl>() != null)
                            {
                                atacked = true;
                            }
                        }

                        if (timer >= 0.5f && atacked)
                        {
                            Atack();
                        }
                        else if (timer >= 3f)
                        {
                            Jump();
                        }
                        else
                        {
                            state = 0;
                            anim.SetInteger("State", 0);
                        }
                    }
                }
            }
        }
    }

    private void Jump()
    {
        timer = 0;
        state = 1;
        anim.SetInteger("State", 1);

        float x_dir = 0;

        if(player.transform.position.x > transform.position.x)
        {
            x_dir = 1;
        }
        else
        {
            x_dir = -1;
        }

        transform.localScale = new Vector3(-x_dir, 1, 1);

        rb.AddForce((Vector2.right * x_dir + Vector2.up * 2) * jumpForce);
    }

    private void Atack()
    {
        float x_dir = 0;
        if (player.transform.position.x > transform.position.x)
        {
            x_dir = 1;
        }
        else
        {
            x_dir = -1;
        }
        transform.localScale = new Vector3(-x_dir, 1, 1);

        timer = 0;
        state = 3;
        anim.SetInteger("State", 3);
    }

    public void DamagePlayer()
    {
        state = 0;
        anim.SetInteger("State", 0);

        RaycastHit2D[] hits = Physics2D.CircleCastAll(new Vector2(transform.position.x, gameObject.transform.position.y), 1.25f, Vector2.zero);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.GetComponent<Character_cntrl>() != null)
            {
                Character_cntrl target = hit.collider.GetComponent<Character_cntrl>();

                target.TakeDamage(Damage * 5);
            }
        }
    }

    private void AtackWhenIn()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(new Vector2(transform.position.x, gameObject.transform.position.y), 0.5f, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.GetComponent<Character_cntrl>() != null)
            {
                Character_cntrl target = hit.collider.GetComponent<Character_cntrl>();

                target.TakeDamage(Damage);
            }
        }
    }

    void SeePlayer()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(new Vector2(transform.position.x, gameObject.transform.position.y), 15, Vector2.zero);
        player = null;

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.GetComponent<Character_cntrl>() != null)
            {
                player = hit.collider.transform;
            }
        }
    }

    public void WhenDamageTaked(int damage, int damageWithArmor, int armor)
    {

    }

    public void Death()
    {
        state = 4;
        anim.SetInteger("State", 4);

        alife = false;
    }

    public void DestroyScare()
    {
        Destroy(gameObject);
    }
}
