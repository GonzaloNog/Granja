using System.Collections;
using System.Collections.Generic;
using UnityEditor.TerrainTools;
using UnityEngine;

public class ModelTile : MonoBehaviour
{
    public TileControler padre;
    public bool suelo;
    private void OnMouseEnter()
    {
        Debug.Log("ENTRO");
        padre.dentroModelo = true;
    }
    private void OnMouseExit()
    {
        padre.dentroModelo = false;
    }
}
