using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct tilesProp
{
    public int id;
    public string name;
    public int precio;
    public GameObject prefad;
    public Sprite img;
}
public enum tileType
{
    normal,
    areado,
    mojado,
    plantado
}
public class structs : MonoBehaviour
{

}
