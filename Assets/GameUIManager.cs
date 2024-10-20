using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;
    public GameObject[] contenedor;
    public GameObject tileButton;
    public GameObject contenedorSemillas;
    public GameObject tileSemilla;
    public TextMeshProUGUI dinero;
    public GameObject editor;
    public GameObject game;
    private int idStore = 0;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        spawnButtons();
        UpdateUI();
    }

    public void spawnButtons()
    {
        int tempMax = 9;
        int contenedorId = 0;
        for(int a = 0; a < LevelManager.instance.tiles.Length; a++)
        {
            GameObject temp = Instantiate(tileButton);

            temp.transform.SetParent(contenedor[contenedorId].transform, false);
            temp.SetActive(true);
            temp.GetComponent<tileUIButton>().idButton = a;
            temp.GetComponent<tileUIButton>().setUI();
            if(a >= tempMax)
            {
                tempMax = tempMax + 9;
                contenedorId++;
            }
        }
        for(int a = 0; a < LevelManager.instance.semillas.Length;a++)
        {
            GameObject temp = Instantiate(tileSemilla);

            temp.transform.SetParent(contenedorSemillas.transform, false);
            temp.SetActive(true);
            temp.GetComponent<tileUIButton>().idButton = a;
            temp.GetComponent<tileUIButton>().setUISemilla();
            temp.GetComponent<tileUIButton>().setUI2(a + 1);
        }
    }
    public void DelateMode()
    {
        LevelManager.instance.setDelateModel(true);
    }
    public void UpdateUI()
    {
        dinero.text = LevelManager.instance.money.ToString();
        editor.SetActive(LevelManager.instance.editorMode);
        game.SetActive(!LevelManager.instance.editorMode);

    }
    public void changeTileScene(bool plus)
    {
        if (plus)
        {
            idStore++;
            if (idStore == contenedor.Length)
                idStore = 0;
        }
        else
        {
            idStore--;
            if(idStore < 0)
                idStore = contenedor.Length - 1;
        }
        for(int a = 0; a < contenedor.Length; a++)
        {
            if (a == idStore)
                contenedor[a].SetActive(true);
            else
                contenedor[a].SetActive(false);
        }
    }
}
