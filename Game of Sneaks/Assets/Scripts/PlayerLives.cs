using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerLives : MonoBehaviour
{
    public TextMeshProUGUI liveUI;

    public float lives = 0;

    // Start is called before the first frame update
    void Start()
    {
        liveUI.text = lives.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Return))
            lives++;

        liveUI.text = lives.ToString();
    }//impliments lifes into game. Enables them to be lost or gained. 
}
