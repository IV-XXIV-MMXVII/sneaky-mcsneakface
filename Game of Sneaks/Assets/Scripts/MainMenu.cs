using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//impliments the main menu screen functions. 
public class MainMenu : MonoBehaviour
{
    public void Play()
    {//Allows for the player to press play and start the game. 
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {// allows for the player to press quit and quit the game. 
        Application.Quit();
    }

   

}
