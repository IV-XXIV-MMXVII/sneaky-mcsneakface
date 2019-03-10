using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Transform Target;

    public Player_Pawn player;

    public GameObject[] spawnPoint;
    public List<GameObject> enemies;
    public List<GameObject> activeInstances;

    public int lives;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        activeInstances = new List<GameObject>();
    }

    void Update()//Below is were the key comands are implimented into the game. 
        //This allows the player to control the character. 
    {
        KeyCommand();
    }

    void KeyCommand()
    {
        //Simply quits the game when on standalone
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Spawn();
        }
    }

    void Spawn()
    {
        int id = Random.Range(0, spawnPoint.Length);

        GameObject enemy = enemies[Random.Range(0, enemies.Count)];
        GameObject point = spawnPoint[id];
        GameObject enemyInstance = Instantiate<GameObject>(enemy, point.transform.position, Quaternion.identity);

        activeInstances.Add(enemyInstance);
    }
}