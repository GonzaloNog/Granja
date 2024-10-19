using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class tileUIButton : MonoBehaviour
{
    public int idButton;

    public TextMeshProUGUI price;
    public Image spri;

    public void newIdButton()
    {
        LevelManager.instance.setTileID(idButton);
        Debug.Log(LevelManager.instance.getTileID());
    }

    public void setUI()
    {
        price.text = LevelManager.instance.tiles[idButton].precio.ToString();
        spri.sprite = LevelManager.instance.tiles[idButton].img;
    }
}
