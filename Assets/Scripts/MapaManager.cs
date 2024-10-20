using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapaManager : MonoBehaviour
{
    public static MapaManager instance;

    public TileControler[] modelTile; 

    public GameObject tilePrefab;
    // Dimensiones de la cuadrícula
    public int rows = 4;
    public int columns = 4;
    private int countTiles = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        modelTile = new TileControler[rows * columns];
        Debug.Log(modelTile.Length);
        GenerateGrid();
    }

    void GenerateGrid()
    {
        // Obtiene el tamaño del tile (su escala local se considera en los ejes X y Z)
        Vector3 tileSize = tilePrefab.GetComponent<Renderer>().bounds.size;
        // Recorre las columnas y filas para crear la cuadrícula
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                // Calcula la posición basada en el tamaño del tile
                Vector3 position = new Vector3(x * tileSize.x, 0, y * tileSize.z);

                // Instancia el tile en la posición calculada
                GameObject tempTile = Instantiate(tilePrefab, position, Quaternion.identity, transform);
                tempTile.GetComponent<TileControler>().idTile = countTiles;
                modelTile[countTiles] = tempTile.GetComponent<TileControler>();
                countTiles++;
            }
        }
    }
    public void ResetEditor()
    {
        for(int a = 0; a < modelTile.Length; a++)
        {
            modelTile[a].resetMouseEnter();
        }
    }
    public void newDay()
    {
        for (int a = 0; a < modelTile.Length; a++)
        {
            modelTile[a].newDay();
        }
    }
}
