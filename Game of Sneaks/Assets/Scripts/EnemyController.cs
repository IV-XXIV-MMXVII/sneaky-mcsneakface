using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Controller
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
       
    }

      void Update ()
    {//Controls pawn movement. 
        switch (pawn.currentState)
        {
            case Pawn.AIStates.Idle:

                pawn.Idle();

                if (pawn.senses.CanHear(GameManager.instance.player.gameObject))
                {
                    pawn.currentState = Pawn.AIStates.LookAround;
                }//Directs pawn to react when it hears. 
                if (pawn.senses.CanSee(GameManager.instance.player.gameObject))
                {
                    pawn.currentState = Pawn.AIStates.Chase;
                }//Directs pawn to react when it sees. 

                break;

            case Pawn.AIStates.Chase:

                pawn.Chase();

                if (!pawn.senses.CanSee(GameManager.instance.player.gameObject))
                {
                    pawn.currentState = Pawn.AIStates.LookAround;
                }//Directs pawn to look around. 
                if (Vector3.Distance(pawn.tf.position, GameManager.instance.player.tf.position) > pawn.giveUpChaseDistance)
                {
                    pawn.currentState = Pawn.AIStates.GoHome;
                }//If player gets to far pawn will go home. 
                break;

            case Pawn.AIStates.LookAround:
                //impliments the AI that allows the pawn to look around for the player. 
                pawn.LookAround();

                if (pawn.senses.CanSee(GameManager.instance.player.gameObject))
                {
                    pawn.currentState = Pawn.AIStates.Chase;
                }
                else if (Vector3.Distance(pawn.tf.position, GameManager.instance.player.tf.position) > pawn.giveUpChaseDistance)
                {
                    pawn.currentState = Pawn.AIStates.Chase;
                }
                else if (!pawn.senses.CanHear(GameManager.instance.player.gameObject))
                {
                    pawn.currentState = Pawn.AIStates.GoHome;
                }
                break;

            case Pawn.AIStates.GoHome:
                //impliments the AI that allows the pawn to go home when player moves to far. 
                pawn.GoHome();
                //Below gives pawn directions for Go Home when certain requirments have been met. 
                if (pawn.senses.CanHear(GameManager.instance.player.gameObject))
                {
                    pawn.currentState = Pawn.AIStates.LookAround;
                }
                if (pawn.senses.CanSee(GameManager.instance.player.gameObject))
                {
                    pawn.currentState = Pawn.AIStates.Chase;
                }
                if (Vector3.Distance(pawn.tf.position, pawn.homePoint) <= pawn.closeEnough)
                {
                    pawn.currentState = Pawn.AIStates.Idle;
                }
                break;

        }
    }
}
