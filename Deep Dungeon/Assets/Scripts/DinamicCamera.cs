using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinamicCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 topLeft, botRight;
	
	void FixedUpdate ()
    {
        //transform.Translate((new Vector3(player.position.x, player.position.y + 2, -10) - transform.position) * 0.2f);
        Vector3 position = transform.position + (new Vector3(player.position.x, player.position.y + 2, -10) - transform.position) * 0.2f;
        if (position.x < topLeft.x)
            position.x = topLeft.x;
        else if (position.x > botRight.x)
            position.x = botRight.x;

        if (position.y > topLeft.y)
            position.y = topLeft.y;
        else if (position.y < botRight.y)
            position.y = botRight.y;

        transform.position = position;
    }
}
