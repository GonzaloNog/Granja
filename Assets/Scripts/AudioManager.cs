using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource musica;
    public AudioSource sfx;
    public AudioClip[] walk;
    public AudioClip construir;
    public AudioClip notMoney;
    public AudioClip[] cabar;
    public AudioClip[] regar;
    public AudioClip[] cosechar;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        musica.volume = GameManager.instance.volMusica;
        sfx.volume = GameManager.instance.volSFX;
    }
    public void newSFX(string code)
    {
        switch(code)
        {
            case "walk":
                sfx.clip = walk[Random.Range(0, walk.Length)];
                break;
            case "build":
                sfx.clip = construir;
                break;
            case "money":
                sfx.clip = notMoney;
                break;
            case "cabar":
                sfx.clip = cabar[Random.Range(0, cabar.Length)];
                break;
            case "regar":
                sfx.clip = regar[Random.Range(0,regar.Length)];
                break;
            case "cosechar":
                sfx.clip = cosechar[Random.Range(0, cosechar.Length)];
                break;
        }
        sfx.Play();
    }
}
