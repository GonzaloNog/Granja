using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    public Color selection;
    public Color error;
    public Color confirm;
  
    public bool editorMode = true;

    public tilesProp[] tiles;

    public int money;

    public Material[] terreno;

    private int tilePropID;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(gameObject);
    }

    //set and get TIles
    public int getTileID()
    {
        return tilePropID;
    }
    public void setTileID(int id)
    {
        tilePropID = id;
    }

}
