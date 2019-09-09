    
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

//Important: 

//Team please make sure you do not put in any code that will edit player Z- axis. Only make changes in regards to player movement in the X or Y axis. 

// its a 2D game- NO Z  AXIS!!!!   :) 

// Z axis is frozen in unity project. But it can still cause problems!

// Thanks

public class Player : MonoBehaviour {


    //configuration of game
    [SerializeField] float jSpeed = 25f;  // edit these fields from the unity engine screen, under the player object. 
    [SerializeField] float runSpeed = 8f;

    [SerializeField] float climbSpeed= 5f;

    //states

    bool isAlive = true;


    //cache
    Rigidbody2D playerBody;
    Animator playerAnimation;
    Collider2D player2Dcollision;
    

	// Use this for initialization--default unity message

    //Methods
	void Start () {
        playerBody = GetComponent<Rigidbody2D>(); // create class reference to rigid body(otherwise locked in methods) 
	
        playerAnimation = GetComponent<Animator>();
        player2Dcollision = GetComponent<Collider2D>();
    }
	
	// Update is called once per frame
	void Update () {


        Run(); // call run method to perform player movements. 
        Jump();

        FlipSprite(); //change players animation direction for running left or right. 

        ClimbLadder();
	}

    private void Run()
    {

        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // val is -1 to +1 (though multiplied by our runspeed!!!!)
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, playerBody.velocity.y);  // Vector2 API!- not variable name :) , velocity.y- leave! -- do not change to other variables-- will lock y axis!!!
        playerBody.velocity = playerVelocity;  // pass player verlocity to body. 

        bool playerXAxis = Mathf.Abs(playerBody.velocity.x)> Mathf.Epsilon;  // this bool will be fed into the next line of code

        playerAnimation.SetBool("Running" , playerXAxis); // here if player is running it will trigger our bool in unity that controls run animation
    }

    private void ClimbLadder(){

        if(!player2Dcollision.IsTouchingLayers(LayerMask.GetMask("Climb")))      {return; //if player is touching a ladder in ladder layer, player will climb
        }
        float controlThrow =CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbVelocity= new Vector2 (playerBody.velocity.x, controlThrow * climbSpeed);

        playerBody.velocity = climbVelocity;
    }


    private void Jump(){
            if(!player2Dcollision.IsTouchingLayers(LayerMask.GetMask("Ground"))){

                return;
            }
            
        if (CrossPlatformInputManager.GetButtonDown("Jump")){

            Vector2 jumpVelocityAdd= new Vector2(0f, jSpeed);
            playerBody.velocity += jumpVelocityAdd;
        }
    }

    private void FlipSprite(){

        bool playerXAxis = Mathf.Abs(playerBody.velocity.x)> Mathf.Epsilon;

        if (playerXAxis ){

            transform.localScale = new Vector2 (Mathf.Sign(playerBody.velocity.x),1f);


        }
        // during x axis movement

        //reverse current x axis scaling 
    }

}