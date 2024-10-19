using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;
    public GameObject contenedor;
    public GameObject tileButton;
    public TextMeshProUGUI dinero;
    public GameObject tienda;

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
    }
    public void DelateMode()
    {
        LevelManager.instance.setDelateModel(true);
    }
    public void UpdateUI()
    {
        dinero.text = LevelManager.instance.money.ToString();
        tienda.SetActive(LevelManager.instance.editorMode);
        
    }
}
