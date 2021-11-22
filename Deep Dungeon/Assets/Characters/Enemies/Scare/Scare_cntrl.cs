using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scare_cntrl : MonoBehaviour
{

    Enemy_cntrl ENc;
    Transform player;
    Vector2 moovePoint;

    public int state = 0;
    public float speed = 0.05f;

    private int posKoff = 1;
    private Animator anim;
    public float timer = 0;

	void Start ()
    {
        ENc = GetComponent<Enemy_cntrl>();

        ENc.Death_void = Death;
        ENc.WhenDamageGets_void = WhenDamageTaked;

        anim = GetComponent<Animator>();
    }

    private float fireTimer = 0;
    public GameObject fire_Pref;
    public List<GameObject> fires = new List<GameObject>();
    void FixedUpdate ()
    {
        timer += Time.deltaTime;
        fireTimer += Time.deltaTime;
        if (state == 1)
        {
            transform.Translate((moovePoint - new Vector2(transform.position.x, transform.position.y)) * 0.2f * speed);

            if(timer >= 4)
            {
                k = Random.Range(0, 2);
                anim.SetInteger("state", 2);
                state = 2;
                timer = 0;
            }
        }

        if (state == 2)
        {
            Atack2();
        }

        if (state == 5 && timer >= 2)
        {
            anim.SetInteger("state", 1);
            timer = 0;
            state = 1;
            Moove1();
        }

        if (fireTimer >= 0.15f && state != 0)
        {
            fireTimer = 0;
            Vector2 vec = new Vector2(Random.Range(-1.75f, 1.75f), Random.Range(-2.25f, 3f)) + new Vector2(transform.position.x, transform.position.y);

            GameObject fire = Instantiate(fire_Pref);
            fire.transform.position = vec;
            fires.Add(fire);
            StartCoroutine(fireFire(fire));
        }
    }

    private IEnumerator fireFire(GameObject fire)
    {
        yield return new WaitForSeconds(1f);

        fires.Remove(fire);
        Destroy(fire.gameObject);
    }

    void Moove1()
    {
        anim.SetInteger("state", 1);
        moovePoint = player.position + posKoff * Vector3.right * 8 + Vector3.up * 4f;

        posKoff = posKoff * (-1);
    }

    int k = 0;
    public int step = 0;
    public GameObject Axe_pref, Knife_pref;
    private List<Scare_Atacks_cntrl> lsac = new List<Scare_Atacks_cntrl>();
    
    void Atack2()
    {
        if(k == 0)
        {
            Vector2 axePos = new Vector2();

            if(timer >= 0.5f && step == 0)
            {
                axePos = new Vector2(transform.position.x, transform.position.y) + new Vector2(-1, 2);
                step++;
                timer = 0;

                GameObject axe = GameObject.Instantiate(Axe_pref);
                axe.transform.position = axePos;
                axe.transform.localScale = new Vector3(-posKoff, 1, 1);
                lsac.Add(axe.GetComponent<Scare_Atacks_cntrl>());
            }
            else if (timer >= 0.5f && step == 1)
            {
                axePos = new Vector2(transform.position.x, transform.position.y) + new Vector2(1, 2);
                step++;
                timer = 0;

                GameObject axe = GameObject.Instantiate(Axe_pref);
                axe.transform.position = axePos;
                axe.transform.localScale = new Vector3(-posKoff, 1, 1);
                lsac.Add(axe.GetComponent<Scare_Atacks_cntrl>());
            }
            else if (timer >= 0.5f && step == 2)
            {
                axePos = new Vector2(transform.position.x, transform.position.y) + new Vector2(0, 3);
                step++;
                timer = 0;

                GameObject axe = GameObject.Instantiate(Axe_pref);
                axe.transform.position = axePos;
                axe.transform.localScale = new Vector3(-posKoff, 1, 1);
                lsac.Add(axe.GetComponent<Scare_Atacks_cntrl>());
            }
            else if (timer >= 0.5f && step == 3)
            {
                StartCoroutine(AtackIE());

                step = 0;
                timer = 0;
                state = 5;
                anim.SetInteger("state", 1);
            }
        }
        else if(k == 1)
        {
            Vector2 knifePos = new Vector2();
            try
            {
                if (timer >= 0.5f && step == 0)
                {
                    knifePos = new Vector2(transform.position.x, transform.position.y) + new Vector2(0, 3);
                    step++;
                    timer = 0;

                    GameObject knife = GameObject.Instantiate(Knife_pref);
                    knife.transform.position = knifePos;
                    knife.transform.localScale = new Vector3(-posKoff, 1, 1);
                    lsac.Add(knife.GetComponent<Scare_Atacks_cntrl>());
                }
                else if (timer >= 0.5f && step == 1)
                {
                    knifePos = new Vector2(transform.position.x, transform.position.y) + new Vector2(0, 3.5f);
                    step++;
                    timer = 0;

                    GameObject knife = GameObject.Instantiate(Knife_pref);
                    knife.transform.position = knifePos;
                    knife.transform.localScale = new Vector3(-posKoff, 1, 1);
                    lsac.Add(knife.GetComponent<Scare_Atacks_cntrl>());
                }
                else if (timer >= 0.5f && step == 2)
                {
                    knifePos = new Vector2(transform.position.x, transform.position.y) + new Vector2(0, 4);
                    step++;
                    timer = 0;

                    GameObject knife = GameObject.Instantiate(Knife_pref);
                    knife.transform.position = knifePos;
                    knife.transform.localScale = new Vector3(-posKoff, 1, 1);
                    lsac.Add(knife.GetComponent<Scare_Atacks_cntrl>());
                }
                else if (timer >= 0.5f && step == 3)
                {
                    StartCoroutine(AtackIE());

                    step = 0;
                    timer = 0;
                    state = 5;
                    anim.SetInteger("state", 1);
                }
            }
            catch { }
        }
    }

    IEnumerator AtackIE()
    {
        foreach (Scare_Atacks_cntrl a in lsac)
        {
            a.direction = player.position;
            a.SetDir();
            a.Atack = true;

            yield return new WaitForSeconds(0.15f);
        }

        lsac.Clear();
    }

    void SeePlayer()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(new Vector2(transform.position.x, gameObject.transform.position.y), 15, Vector2.zero);
        player = null;

        foreach (RaycastHit2D hit in hits)
        {
            if(hit.collider.GetComponent<Character_cntrl>() != null)
            {
                player = hit.collider.transform;
            }
        }

        if (player == null)
            state = 0;
    }

    public void WhenDamageTaked(int damage, int damageWithArmor, int armor)
    {
        SeePlayer();

        if (state == 0)
        {
            GetComponent<SpriteRenderer>().sortingOrder = -4;
            timer = 0;
            state = 1;
            Moove1();
        }
    }

    public void Death()
    {
        foreach(Scare_Atacks_cntrl a in lsac)
        {
            Destroy(a.gameObject);
        }
        lsac.Clear();

        state = 3;
        anim.SetInteger("state", 3);
    }

    public void DestroyScare()
    {
        foreach(GameObject fire in fires)
        {
            Destroy(fire);
        }
        fires.Clear();
        Destroy(gameObject);
    }
}
