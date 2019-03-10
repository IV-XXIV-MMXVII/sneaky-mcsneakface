using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lion_of_death : MonoBehaviour
{
    private AISenses senses;

    public enum AIStates
    {
        Idle,
        Chase,
        LookAround,
        GoHome,
        StartAlert,
        EndAlert
    }

    
    public float speed;
    public float distance;
    public Vector2 heading;
    public Transform playerPosition;
    public Vector3 homePoint;
    public Vector3 goalPoint;
    public AIStates currentState;
    public float giveUpChaseDistance;
    public float closeEnough;
    public int health;
    public float moveSpeed = 1;
    public float turnSpeed = 1;
    Transform tf;


    // Start is called before the first frame update
    void Start()
    {
        senses = GetComponent<AISenses>();

        tf = GetComponent<Transform>();

        homePoint = tf.position;
    }

    // Update is called once per frame
    void Update()
    {
       
        switch (currentState)
        {
            case AIStates.Idle:

                Idle();//Gives the enemy directions on when to be idol and when not to be. 

                if (senses.CanHear(GameManager.instance.player.gameObject))
                {
                    currentState = AIStates.LookAround;//allows lion to look around. 
                }
                if (senses.CanSee(GameManager.instance.player.gameObject))
                {
                    currentState = AIStates.Chase;//allows lion to chase. 
                }

                break;

            case AIStates.Chase:

                Chase();//gives lion the abillity to chase depending on situation. 

                if (!senses.CanSee(GameManager.instance.player.gameObject))
                {
                    currentState = AIStates.LookAround;//lion will look during chase. 
                }
                if (Vector3.Distance(tf.position, GameManager.instance.player.tf.position) > giveUpChaseDistance)
                {
                    currentState = AIStates.GoHome;//if player is lost lion will go back to start position. 
                }
                break;

            case AIStates.LookAround:

                LookAround();//Lion will scan area. 

                if (senses.CanSee(GameManager.instance.player.gameObject))
                {
                    currentState = AIStates.Chase;//if player is spotted it will chase. 
                }
                else if (Vector3.Distance(tf.position, GameManager.instance.player.tf.position) > giveUpChaseDistance)
                {
                    currentState = AIStates.Chase;//if player is within distance it will chase. 
                }
                else if (!senses.CanHear(GameManager.instance.player.gameObject))
                {
                    currentState = AIStates.GoHome;//if player is not within distance or is out of site lion will go home. 
                }
                break;

            case AIStates.GoHome:

                GoHome();//allows lion to move back to the starting position. 

                if (senses.CanHear(GameManager.instance.player.gameObject))
                {
                    currentState = AIStates.LookAround;//will go home if sight lost. 
                }
                if (senses.CanSee(GameManager.instance.player.gameObject))
                {
                    currentState = AIStates.Chase;
                }
                if (Vector3.Distance(tf.position, homePoint) <= closeEnough)
                {
                    currentState = AIStates.Idle;
                }
                break;
                //Lion pawn's actions are managed in all code above. 
        }
        heading = transform.position - playerPosition.position;
        distance = heading.magnitude;

        if (distance < 2)//sets distance. 
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, speed * Time.deltaTime);
        }//if player is within distance lion will move towards player. 
        
        
       

    }

    public void Idle()//lion stays idol. 
    {

    }

    public void Chase()
    {
        goalPoint = playerPosition.position;
        Move(goalPoint);//if player is within distance lion will chase. 
    }

    public void LookAround()//looks around. 
    {

    }

    public void GoHome()//goes home. 
    {

    }

    public void MoveTowards(Vector3 target)
    {
        if (Vector3.Distance(tf.position, target) > closeEnough)
        {
            Vector3 vectorToTarget = target - tf.position;
            tf.right = vectorToTarget;

            MoveTowards(tf.right);
        }//Code to move lion towards target position. 

    }
    public void Move(Vector3 direction)
    {
        tf.position += (direction.normalized * moveSpeed * Time.deltaTime);
    }//move speed. 

    public void Turn(bool isTurnClockwise)
    {//This Rotates based on the turnSpeed and direction.
        if (isTurnClockwise)
        {
            tf.Rotate(0, 0, turnSpeed * Time.deltaTime);
        }//sets rotate. 
        else
        {
            tf.Rotate(0, 0, -turnSpeed * Time.deltaTime);
        }
    }//sets time to rotate. 

  }

