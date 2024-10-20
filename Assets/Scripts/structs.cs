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
[System.Serializable]
public struct semilla
{
    public string name;
    public int precio;
    public int cosecha;
    public int ganancia;
    public GameObject semillaPrefad;
    public GameObject plantaPrefadP1;
    public GameObject plantaPrefadP2;
    public Sprite semillaIMG;
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
