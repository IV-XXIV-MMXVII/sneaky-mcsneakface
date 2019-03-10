using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noisemaker : MonoBehaviour
{
    public float volume = 0;
    public float decayPerFrameDraw = 0.016f;
    // Start is called before the first frame update
  
     

    // Update is called once per frame
    void Update()
    {
        if (volume > 0)//sets volume. 
        {
            volume -= decayPerFrameDraw;
        }
    }//allows lion to move when it hears noise. 
}
