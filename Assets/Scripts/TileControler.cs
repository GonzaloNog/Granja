using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileControler : MonoBehaviour
{
    private EventSystem eventSystem;
    public int idTile;

    private Color colorOriginal;
    private Renderer ren;
    private bool dentro;

    private tileType tileType;

    private void Awake()
    {
        tileType = tileType.normal;
    }
    public void Start()
    {
        colorOriginal = GetComponent<Renderer>().material.color;
        ren = GetComponent<Renderer>();
        ren.material = LevelManager.instance.terreno[0];
    }

    private void Update()
    {
        if (LevelManager.instance.editorMode)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && dentro && !IsPointerOverUI())
            {
                Debug.Log("Entro");
                ren.material.color = LevelManager.instance.confirm;
            }
        }
    }


    public void OnMouseEnter()
    {
        if (IsPointerOverUI())
        {
            return; // No hacer nada si el mouse está sobre la UI
        }
        if (LevelManager.instance.editorMode)
        {
            dentro = true;
            if (GetComponent<Renderer>() != null)
            {
                // Crea una instancia del material del objeto
                Material instanceMaterial = GetComponent<Renderer>().material;

                // Cambia el color Albedo del material de la instancia
                instanceMaterial.color = LevelManager.instance.selection;
            }
            else
            {
                Debug.LogWarning("No se encontr� un componente Renderer en este objeto.");
            }
        } 
    }

    public void OnMouseExit()
    {
        if (LevelManager.instance.editorMode)
        {
            ren.material.color = colorOriginal;
            dentro = false;
        }
    }
    private bool IsPointerOverUI()
    {
        // Crea un rayo desde la posición del mouse
        PointerEventData pointerData = new PointerEventData(eventSystem)
        {
            position = Input.mousePosition
        };

        // Lista para almacenar los resultados del raycast
        var results = new List<RaycastResult>();
        // Realiza el raycast
        EventSystem.current.RaycastAll(pointerData, results);

        // Si hay elementos en la lista, significa que el mouse está sobre la UI
        return results.Count > 0;
    }
}
