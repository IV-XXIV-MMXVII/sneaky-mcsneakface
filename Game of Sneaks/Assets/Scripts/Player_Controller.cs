using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : Controller
{
    public override void Start()
    {
        base.Start();
    }

    void FixedUpdate()
    {
        pawn.animator.SetBool("isWalking", pawn.isWalking);
        //Game Controls Updated Every Fram

        pawn.coroutine = pawn.Walk();

        if (pawn.col == false)
        {
            if (Input.GetKey(KeyCode.UpArrow))
                pawn.MoveFoward();//if the up arrow is pressed the pawn will move forward. 

            else if (Input.GetKey(KeyCode.DownArrow))
                pawn.MoveBackwards();//if down arrow is pressed pawn will move backwards. 

            else if (Input.GetKey(KeyCode.LeftArrow))
                pawn.MoveLeft();// if left arrow is pressed pawn will move left. 

            else if (Input.GetKey(KeyCode.RightArrow))
                pawn.MoveRight();// if right arrow is pressed pawn will move right. 

            else
            {
                pawn.isWalking = false;//keeps pawn idol. 
            }
        }

        if (pawn.isWalking == true)
        {
            StartCoroutine(pawn.coroutine);//allows pawn to move with imput. 
        }

    }
}
