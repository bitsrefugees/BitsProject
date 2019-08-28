    
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {


    //configuration of game
    [SerializeField] float runSpeed = 5f;

    //states

    bool isAlive = true;


    //cache
    Rigidbody2D myRigidBody;
    Animator myAnimator;

	// Use this for initialization--default unity message

    //Methods
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>(); // create class reference to rigid body(otherwise locked in methods) 
	
        myAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {


        Run(); // call run method to perform player movements. 

        FlipSprite();
	}

    private void Run()
    {

        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // val is -1 to +1 (though multiplied by our runspeed!!!!)
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);  // Vector2 API!- not variable name :) , velocity.y- leave! -- do not change to other variables-- will lock y axis!!!
        myRigidBody.velocity = playerVelocity;  // pass player verlocity to body. 

        bool playerXAxis = Mathf.Abs(myRigidBody.velocity.x)> Mathf.Epsilon;  // this bool will be fed into the next line of code

        myAnimator.SetBool("Running" , playerXAxis); // here if player is running it will trigger our bool in unity that controls run animation
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