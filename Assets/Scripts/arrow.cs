using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum orientation{
    left,right
}

public class arrow : MonoBehaviour{
    public orientation facing;
    public int speed;
    public SpriteRenderer renderer;
    public Rigidbody2D rigidBody;
    public int fallingSpeed;
    void Start(){
        switch (facing)
        {
            case orientation.left:
                renderer.flipX = true;
                break;
            case orientation.right:
                renderer.flipX = false;
                break;
            default:
                break;
        }
    }


    // Update is called once per frame
    void Update(){
        switch (facing){
            case orientation.left:
                rigidBody.velocity = new Vector2(-speed,-fallingSpeed);
                break;
            case orientation.right:
                rigidBody.velocity = new Vector2(speed,-fallingSpeed);
                break;
            default:
                break;
        }
        if (transform.position.x <= -1000 || transform.position.x >= 1000|| transform.position.y <=-1000){
            Destroy(gameObject);
        }
    }
}
