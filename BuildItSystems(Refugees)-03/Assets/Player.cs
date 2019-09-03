    
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {


    //configuration of game
    [SerializeField] float jSpeed = 25f;
    [SerializeField] float runSpeed = 8f;

    [SerializeField] float climbSpeed= 5f;

    //states

    bool isAlive = true;


    //cache
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    Collider2D myCollider2D;
    

	// Use this for initialization--default unity message

    //Methods
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>(); // create class reference to rigid body(otherwise locked in methods) 
	
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();
    }
	
	// Update is called once per frame
	void Update () {


        Run(); // call run method to perform player movements. 
        Jump();

        FlipSprite();

        ClimbLadder();
	}

    private void Run()
    {

        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // val is -1 to +1 (though multiplied by our runspeed!!!!)
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);  // Vector2 API!- not variable name :) , velocity.y- leave! -- do not change to other variables-- will lock y axis!!!
        myRigidBody.velocity = playerVelocity;  // pass player verlocity to body. 

        bool playerXAxis = Mathf.Abs(myRigidBody.velocity.x)> Mathf.Epsilon;  // this bool will be fed into the next line of code

        myAnimator.SetBool("Running" , playerXAxis); // here if player is running it will trigger our bool in unity that controls run animation
    }

    private void ClimbLadder(){

        if(!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Climb")))      {return; //if player is touching a ladder in ladder layer, player will climb
        }
        float controlThrow =CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbVelocity= new Vector2 (myRigidBody.velocity.x, controlThrow * climbSpeed);

        myRigidBody.velocity = climbVelocity;
    }


    private void Jump(){
            if(!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))){

                return;
            }
            
        if (CrossPlatformInputManager.GetButtonDown("Jump")){

            Vector2 jumpVelocityAdd= new Vector2(0f, jSpeed);
            myRigidBody.velocity += jumpVelocityAdd;
        }
    }

    private void FlipSprite(){

        bool playerXAxis = Mathf.Abs(myRigidBody.velocity.x)> Mathf.Epsilon;

        if (playerXAxis ){

            transform.localScale = new Vector2 (Mathf.Sign(myRigidBody.velocity.x),1f);


        }
        // during x axis movement

        //reverse current x axis scaling 
    }

}