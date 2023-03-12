using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour{
    public Rigidbody2D rigidBody;
    public float jumpPower;
    public float speed;
    public Logic logic;
    public int maxJumps;
    private int jumps = 0;
    public SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start(){
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<Logic>();
    }

    // Update is called once per frame
    void Update(){
    	if(logic.alive){
        	if ((Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.UpArrow)||Input.GetKeyDown(KeyCode.W)) && jumps <maxJumps){	
        	    rigidBody.velocity = new Vector2(rigidBody.velocity.x,jumpPower);
        	    jumps++;
        	}else if (Input.GetKeyDown(KeyCode.LeftArrow)||Input.GetKeyDown(KeyCode.A)){
            	rigidBody.velocity = new Vector2(-speed,rigidBody.velocity.y);
            	renderer.flipX=true;
        	}else if (Input.GetKeyDown(KeyCode.RightArrow)||Input.GetKeyDown(KeyCode.D)){
            	rigidBody.velocity = new Vector2(speed,rigidBody.velocity.y);
            	renderer.flipX=false;
        	}
        }
    }
    private void OnCollisionEnter2D(Collision2D collision){
    	jumps=0;
    }
}
