using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantaControler : MonoBehaviour
{
    private TileControler tile;
    void Start()
    {
        
    }
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
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Tile")
        {
            tile = hit.gameObject.GetComponent<TileControler>();
        }
    }
}
