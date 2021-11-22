using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Status_cntrl : MonoBehaviour
{
    public Image HP_bar;
    public Text coinText, HP_text;

    public void UpdateHP (int carrent_HP, int HP)
    {
        HP_bar.GetComponent<RectTransform>().localScale = new Vector3((float)carrent_HP / (float)HP, 1, 1);
        HP_text.text = carrent_HP.ToString() + " / " + HP.ToString();
	}

    public void CoinCount(int coins)
    {
        coinText.text = coins.ToString();
    }

    public Image weaponImg;
    public void ChangeWeapon(Sprite image)
    {
        weaponImg.sprite = image;
    }
}
