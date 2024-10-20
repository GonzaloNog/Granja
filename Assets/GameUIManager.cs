using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;
    public GameObject contenedor;
    public GameObject tileButton;
    public GameObject contenedorSemillas;
    public GameObject tileSemilla;
    public TextMeshProUGUI dinero;
    public GameObject editor;
    public GameObject game;

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
        for(int a = 0; a < LevelManager.instance.tiles.Length; a++)
        {
            GameObject temp = Instantiate(tileButton);

            temp.transform.SetParent(contenedor.transform, false);
            temp.SetActive(true);
            temp.GetComponent<tileUIButton>().idButton = a;
            temp.GetComponent<tileUIButton>().setUI();
        }
        for(int a = 0; a < LevelManager.instance.semillas.Length;a++)
        {
            GameObject temp = Instantiate(tileSemilla);

            temp.transform.SetParent(contenedorSemillas.transform, false);
            temp.SetActive(true);
            temp.GetComponent<tileUIButton>().idButton = a;
            temp.GetComponent<tileUIButton>().setUISemilla();
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
}
