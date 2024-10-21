using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    public Color selection;
    public Color error;
    public Color confirm;
  
    public bool editorMode = false;

    public tilesProp[] tiles;

    public int money;

    public Material[] terreno;

    private int tilePropID;
    private bool delateModels = false;

    public Camera camConstruccion;
    public Camera camJuego;

    public int Day = 1;

    public semilla[] semillas;
    private int idSemilla;
    public GameObject[] monedas;
    public GameObject spawn;
    public bool[] audios; 

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
    void Start()
    {
        // Activamos solo la primera cámara al inicio
        camJuego.gameObject.SetActive(true);
        camConstruccion.gameObject.SetActive(false);
        if (editorMode)
            CambiarEntreCamaras();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CambiarEntreCamaras();
        }
    }
    void CambiarEntreCamaras()
    {
        // Cambiamos el estado activo de las cámaras
        MapaManager.instance.ResetEditor();
        bool camara1Activa = camJuego.gameObject.activeSelf;
        if (camara1Activa)
        {
            editorMode = true;
            GameUIManager.Instance.UpdateUI();
            UnityEngine.Cursor.visible = true;
        }
        else
        {
            UnityEngine.Cursor.visible = false;
            editorMode = false;
            GameUIManager.Instance.UpdateUI();
        }
        // Si la cámara 1 está activa, la apagamos y encendemos la cámara 2, y viceversa
        camJuego.gameObject.SetActive(!camara1Activa);
        camConstruccion.gameObject.SetActive(camara1Activa);
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
    public void setDelateModel(bool _delat)
    {
        delateModels = _delat;
    }
    public bool getDelatemodel()
    {
        return delateModels;
    }
    public void setIdSemilla(int num)
    {
        idSemilla = num;
    }
    public int getIdSemilla()
    {
        return idSemilla;
    }
    public void newDay()
    {
        Day++;
        MapaManager.instance.newDay();
        StartCoroutine(GameUIManager.Instance.newNight());
    }

}
