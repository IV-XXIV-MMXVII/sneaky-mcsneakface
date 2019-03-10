using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPawn : Pawn
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (enemyHealth <= 0)
        {
            Debug.Log("YATTA!!!!");
            Destroy(gameObject);
        }
    }
    public override void Idle()
    {

    }

    public override void Chase()
    {//Controls enemy Chase function
        goalPoint = GameManager.instance.player.tf.position;
        MoveTowards(goalPoint);
    }

    public override void LookAround()
    {//Enemy looks for player. 
        Turn(true);
    }

    public override void GoHome()
    {//Sends enemy back to home position. 
        goalPoint = homePoint;
        MoveTowards(goalPoint);
    }

    public override void MoveTowards(Vector3 target)
    {
        if (Vector3.Distance(tf.position, target) > closeEnough)
        {
            Vector3 vectorToTarget = target - tf.position;
            tf.right = vectorToTarget;

            Move(tf.right);
        }

    }
    public override void Move(Vector3 direction)
    {
        tf.position += (direction.normalized * moveSpeed * Time.deltaTime);
    }

    public override void Turn(bool isTurnClockwise)
    {//This Rotates based on the turnSpeed and direction.
        if (isTurnClockwise)
        {
            tf.Rotate(0, 0, turnSpeed * Time.deltaTime);
        }
        else
        {
            tf.Rotate(0, 0, -turnSpeed * Time.deltaTime);
        }
    }
    public override void TakeDamage(int damage)
    {
        enemyHealth -= damage;
       
        Debug.Log("damage TAKEN");

    }
}
