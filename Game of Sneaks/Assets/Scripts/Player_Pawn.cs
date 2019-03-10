using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Pawn : Pawn
{
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        tf = playerPosition;
        playerPosition = gameObject.transform;
    }//grabs player position. 

    public override void MoveFoward()
    {
        
        fowardDown = Input.GetKey(KeyCode.UpArrow);

        //Going fowards
        if (fowardDown == true)
        {
            isWalking = true;

            if (Input.GetKey(KeyCode.RightArrow) == true)
            {//moves right 
                Vector3 xscale = transform.localScale;
                xscale.x = 1;
                transform.localScale = xscale;
                move = new Vector2((float)rateOfSpeed, (float)rateOfSpeed);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) == true)
            {//moves left. 
                Vector3 xscale = transform.localScale;
                xscale.x = -1;
                transform.localScale = xscale;
                move = new Vector2((float)-rateOfSpeed, (float)rateOfSpeed);
            }
            else
                move = new Vector2(0, (float)rateOfSpeed);
            //rate of speed. 
            if (rb.velocity.magnitude < maxSpeed)
                rb.velocity += move * Time.fixedDeltaTime;
            //impliments actions of player controller to the player pawn 
            //this allows for actions to be taken by pawn when buttons are pressed. 

        }

        if (noisemaker != null)
        {
            noisemaker.volume = Mathf.Max(noisemaker.volume, moveVolume);
        }//if noise is made enemy will move. 
    }

    public override void MoveLeft()
    {//moves left. 

        leftDown = Input.GetKey(KeyCode.LeftArrow);

        if (leftDown == true)
        {
            isWalking = true;
            Vector3 xscale = transform.localScale;
            xscale.x = -1;
            transform.localScale = xscale;

            //Going fowards


            Vector2 move = new Vector2((float)-rateOfSpeed, 0);
            if (rb.velocity.magnitude < maxSpeed)
                rb.velocity += move * Time.fixedDeltaTime;

        }
        if (noisemaker != null)
        {
            noisemaker.volume = Mathf.Max(noisemaker.volume, turnVolume);
        }
    }

    public override void MoveRight()
    {//moves right. 

        rightDown = Input.GetKey(KeyCode.RightArrow);

        if (rightDown == true)
        {
            isWalking = true;
            Vector3 xscale = transform.localScale;
            xscale.x = 1;
            transform.localScale = xscale;

            //Going fowards

            Vector2 move = new Vector2((float)rateOfSpeed, 0);
            if (rb.velocity.magnitude < maxSpeed)
                rb.velocity += move * Time.fixedDeltaTime;

        }
        if (noisemaker != null)
        {
            noisemaker.volume = Mathf.Max(noisemaker.volume, turnVolume);
        }
    }

    public override void MoveBackwards()
    {//moves backwards. 


        backwardsDown = Input.GetKey(KeyCode.DownArrow);


        //Going backwards
        if (backwardsDown == true)
        {
            isWalking = true;

            if (Input.GetKey(KeyCode.RightArrow) == true)
            {
                Vector3 xscale = transform.localScale;
                xscale.x = 1;
                transform.localScale = xscale;
                move = new Vector2((float)rateOfSpeed, (float)-rateOfSpeed);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) == true)
            {
                Vector3 xscale = transform.localScale;
                xscale.x = -1;
                transform.localScale = xscale;
                move = new Vector2((float)-rateOfSpeed, (float)-rateOfSpeed);
            }
            else
                move = new Vector2(0, (float)-rateOfSpeed);

            if (rb.velocity.magnitude < maxSpeed)
                rb.velocity += move * Time.fixedDeltaTime;

        }

        if (noisemaker != null)
        {
            noisemaker.volume = Mathf.Max(noisemaker.volume, moveVolume);
        }
    }
    private void OnTriggerStay2D(Collider2D hit)
    {
        if (hit.gameObject.tag == "hitbox")
        {
            SceneManager.LoadScene(3);
        }
    }
}
