using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileControler : MonoBehaviour
{
    private EventSystem eventSystem;
    public int idTile;

    private Color colorOriginal;
    private Renderer ren;
    private bool dentro;
    public bool dentroModelo;

    private tileType tileType;

    public GameObject modelo;

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
            if (Input.GetKeyDown(KeyCode.Mouse0) && dentro && !IsPointerOverUI() && modelo == null)
            {
                if (LevelManager.instance.money >= LevelManager.instance.tiles[LevelManager.instance.getTileID()].precio)
                {
                    Debug.Log("Entro");
                    ren.material.color = LevelManager.instance.confirm;

                    modelo = Instantiate(LevelManager.instance.tiles[LevelManager.instance.getTileID()].prefad, this.transform.position, this.transform.rotation);
                    modelo.GetComponent<ModelTile>().padre = this;
                    Collider objCollider = modelo.GetComponent<Collider>();
                    if (objCollider != null && !modelo.GetComponent<ModelTile>().suelo)
                    {
                        // Calculamos la mitad de la altura del objeto
                        float altura = objCollider.bounds.size.y / 2;

                        // Ajustamos la posición en Y para que no quede hundido
                        modelo.transform.position = new UnityEngine.Vector3(this.transform.position.x, this.transform.position.y + altura, this.transform.position.z);
                    }
                    LevelManager.instance.money -= LevelManager.instance.tiles[LevelManager.instance.getTileID()].precio;
                    GameUIManager.Instance.UpdateUI();
                }
                else
                    StartCoroutine(notMOney());
            }
            else if (LevelManager.instance.getDelatemodel() && modelo != null && (dentro || dentroModelo) && Input.GetKeyDown(KeyCode.Mouse0))
            {
                Destroy(modelo.gameObject);
                LevelManager.instance.money += Mathf.FloorToInt(((float)LevelManager.instance.tiles[LevelManager.instance.getTileID()].precio * 0.8f));
                GameUIManager.Instance.UpdateUI();
            }
            if (Input.GetKeyDown(KeyCode.Mouse1) && (dentro || dentroModelo) && !IsPointerOverUI() && modelo != null)
            {
                Debug.Log("RotarModelo");
                modelo.transform.Rotate(0, 90, 0);
                //||
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
    public void setType(tileType tip)
    {
        tileType = tip;
    }

    public IEnumerator notMOney()
    {
        Material instanceMaterial = GetComponent<Renderer>().material;
        instanceMaterial.color = LevelManager.instance.error;
        yield return new WaitForSeconds(1);
        ren.material.color = colorOriginal;
    }
}
