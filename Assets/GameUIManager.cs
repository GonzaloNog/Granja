using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public GameObject contenedor;
    public GameObject tileButton;
    void Start()
    {
        spawnButtons();
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
}
