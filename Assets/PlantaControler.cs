using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantaControler : MonoBehaviour
{

    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
