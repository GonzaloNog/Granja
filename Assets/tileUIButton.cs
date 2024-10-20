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
        LevelManager.instance.setDelateModel(false);
        Debug.Log(LevelManager.instance.getTileID());
    }

    public void setUI()
    {
        price.text = LevelManager.instance.tiles[idButton].precio.ToString();
        spri.sprite = LevelManager.instance.tiles[idButton].img;
    }

    public void setUISemilla()
    {
        price.text = LevelManager.instance.semillas[idButton].precio.ToString();
        spri.sprite = LevelManager.instance.semillas[idButton].semillaIMG;
    }
    public void newSemilla()
    {
        LevelManager.instance.setIdSemilla(idButton);
    }
}
