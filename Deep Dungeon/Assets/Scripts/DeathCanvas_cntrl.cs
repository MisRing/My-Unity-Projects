using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCanvas_cntrl : MonoBehaviour
{

	public void LoadTavern ()
    {
        SceneManager.LoadScene(1);
	}
	
	public void LoadMainMenu ()
    {
        SceneManager.LoadScene(0);
    }
}
