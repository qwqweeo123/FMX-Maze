using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialExplosionObj : MonoBehaviour 
{
    private AudioSource audio;

    void Aake()
    {
        audio = GetComponent<AudioSource>();
       
    }
    void OnEnable()
    {
        if (audio != null)
        {
            audio.Play();
        }
    }
}
