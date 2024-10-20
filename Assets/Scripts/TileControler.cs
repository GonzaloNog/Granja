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
    private int DayPlantado;
    public int DayCosecha;
    private bool regado = false;
    public bool plantado = false;

    private tileType tileT;

    public GameObject modelo;
    private semilla sem;
    private GameObject planta;
    private GameObject coin;

    private void Awake()
    {
        tileT = tileType.normal;
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
                if(tileT == tileType.normal)
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
                        AudioManager.instance.newSFX("build");
                    }
                    else
                    {
                        StartCoroutine(notMOney());
                        AudioManager.instance.newSFX("money");
                    }
                }
                else
                {
                    StartCoroutine(notMOney());
                    AudioManager.instance.newSFX("money");
                }

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
            Debug.Log("ColorOriginal");
            ren.material.color = colorOriginal;
            dentro = false;
        }
    }
    public void resetMouseEnter()
    {
        ren.material.color = colorOriginal;
        dentro = false;
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
        tileT = tip;
    }
    public tileType getTileType()
    {
        return tileT;
    }

    public IEnumerator notMOney()
    {
        Material instanceMaterial = GetComponent<Renderer>().material;
        instanceMaterial.color = LevelManager.instance.error;
        yield return new WaitForSeconds(1);
        ren.material.color = colorOriginal;
    }

    public void UpdateTile()
    {
        switch (tileT)
        {
            case tileType.normal:
                ren.material = LevelManager.instance.terreno[0];
                break;
            case tileType.areado:
                ren.material = LevelManager.instance.terreno[1];
                AudioManager.instance.newSFX("cabar");
                break;
            case tileType.mojado:
                if (!regado)
                {
                    ren.material = LevelManager.instance.terreno[2];
                    regado = true;
                    AudioManager.instance.newSFX("regar");
                    Destroy(coin);
                }
                break;
        }
        colorOriginal = GetComponent<Renderer>().material.color;
    }
    public void newDay()
    {
        regado = false;
        if(tileT == tileType.mojado)
        {
            Debug.Log("DayCosecha: " + DayCosecha + "DayPlantado: " + DayPlantado + "DayActual: " + LevelManager.instance.Day);
            tileT = tileType.areado;
            ren.material = LevelManager.instance.terreno[1];
            coin = Instantiate(LevelManager.instance.monedas[0],this.transform.position,this.transform.rotation);

        }
        else if (plantado)
        {
            DayCosecha++;
            coin = Instantiate(LevelManager.instance.monedas[0], this.transform.position, this.transform.rotation);
        }
        if (plantado)
        {
            if((DayPlantado + 2) == LevelManager.instance.Day)
            {
                Destroy(planta);
                planta = Instantiate(sem.plantaPrefadP1, this.transform.position, this.transform.rotation);
            }
            if (DayCosecha <= LevelManager.instance.Day)
            {
                Destroy(planta);
                Destroy(coin);
                coin = Instantiate(LevelManager.instance.monedas[1], this.transform.position, this.transform.rotation);
                planta = Instantiate(sem.plantaPrefadP2, this.transform.position, this.transform.rotation);
            }
        }
    }
    public void gestionarSemillas()
    {
        if (!plantado)
        {
            sem = LevelManager.instance.semillas[LevelManager.instance.getIdSemilla()];
            DayPlantado = LevelManager.instance.Day;
            DayCosecha = LevelManager.instance.Day + sem.cosecha;
            planta = Instantiate(sem.semillaPrefad, this.transform.position, this.transform.rotation);
            plantado = true;
        }
    }
    public void cosechar()
    {
        if (DayCosecha <= LevelManager.instance.Day && plantado)
        {
            Debug.Log("COSECHA");
            plantado = false;
            Destroy(planta);
            tileT = tileType.areado;
            LevelManager.instance.money += sem.ganancia;
            GameUIManager.Instance.UpdateUI();
            tileT = tileType.normal;
            UpdateTile();
            AudioManager.instance.newSFX("cosechar");
        }
    }
}
