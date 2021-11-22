using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scare_Atacks_cntrl : MonoBehaviour
{
    public bool Atack = false, mustRotate = false;
    public Vector2 direction;
    public float speed;
    public int Damage;

	public void Update()
    {
        if(Atack)
        {
            if (mustRotate)
                transform.Translate(Vector2.left * transform.localScale.x * Time.deltaTime * speed);
            else
                transform.Translate(direction * Time.deltaTime * speed);
        }
    }

    public void SetDir()
    {
        direction = direction - new Vector2(transform.position.x, transform.position.y);
        direction.Normalize();
        StartCoroutine(DestroyThis());
        if(mustRotate)
        {
            float angle = Vector2.Angle(Vector2.up, direction) + 180;
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                transform.eulerAngles = new Vector3(0, 0, angle);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                transform.eulerAngles = new Vector3(0, 0, -angle);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.GetComponent<Character_cntrl>() != null)
        {
            Character_cntrl player = coll.GetComponent<Character_cntrl>();

            player.TakeDamage(Damage);
        }
    }

    public IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
