using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public void Moovevv()
    {
        StartCoroutine(dTextAnim());
    }

    public IEnumerator dTextAnim()
    {
        for (int i = 0; i <= 50; i++)
        {
            yield return new WaitForSeconds(0.01f);
            gameObject.transform.Translate(transform.up * (0.15f - i * 0.003f));
        }
        Destroy(gameObject);
    }
}
