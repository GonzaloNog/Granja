using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlantaControler : MonoBehaviour
{
    private TileControler tile;
    private void Update()
    {
        if (!LevelManager.instance.editorMode)
        {
            if (Input.GetKeyDown(KeyCode.E) && tile.modelo == null)
            {
                switch (tile.getTileType())
                {
                    case tileType.normal:
                        tile.setType(tileType.areado);
                        break;
                    case tileType.areado:
                        tile.setType(tileType.mojado);
                        break;
                }
                tile.UpdateTile();
                tile.cosechar();
            }
            if(Input.GetKeyDown(KeyCode.F) && tile.modelo == null)
            {
                if(tile.getTileType() == tileType.areado || tile.getTileType() == tileType.mojado)
                {
                    if (!tile.plantado)
                    {
                       tile.gestionarSemillas();
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                //LevelManager.instance.newDay();
            }
        }
        cambioSemillas();
    }
    private void OnCollisionEnter(Collision collision)
    {
        UnityEngine.Debug.Log(collision.gameObject.name);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Tile")
        {
            tile = hit.gameObject.GetComponent<TileControler>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "cama")
        {
            GetComponent<CharacterController>().enabled = false;
            this.gameObject.transform.position = LevelManager.instance.spawn.transform.position;
            LevelManager.instance.newDay();
            GetComponent<CharacterController>().enabled = true;
        }
    }
    public void cambioSemillas()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UnityEngine.Debug.Log("1");
            LevelManager.instance.setIdSemilla(0);
            GameUIManager.Instance.chageSetSemilla();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UnityEngine.Debug.Log("2");
            LevelManager.instance.setIdSemilla(1);
            GameUIManager.Instance.chageSetSemilla();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UnityEngine.Debug.Log("3");
            LevelManager.instance.setIdSemilla(2);
            GameUIManager.Instance.chageSetSemilla();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UnityEngine.Debug.Log("4");
            LevelManager.instance.setIdSemilla(3);
            GameUIManager.Instance.chageSetSemilla();
        }
    }
}
