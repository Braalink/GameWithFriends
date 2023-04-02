using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public enum weapon{
    none,
    bow,
    hammer
}
public class player : MonoBehaviour{
    public Rigidbody2D rigidBody;
    public float jumpPower;
    public float speed;
    public Logic logic;
    public int maxJumps;
    private int jumps = 0;
    public SpriteRenderer renderer;
    public weapon currentWeapon;
    public bool dash;
    public float dashSpeed;
    Camera cam;
    private PlayerControls Controls;
    private bool jumpHeld = false;
    private bool rightHeld = false;
    private bool leftHeld = false;
    private bool dashHeld = false;
    private float dashCooldownSec;
    public float cooldown;
    private float time;
    public GameObject bowSprite;
    public int arrowSpeed;
    public GameObject arrow;
    private bool shooting = false;
    public float arrowCooldown;
    private float arrowCool;
    private void Awake(){
        Controls = new PlayerControls(); 
    }
    private void OnEnable() {
        Controls.player.jump.started += JumpChange;
        Controls.player.jump.canceled += JumpChange;
        Controls.player.jump.Enable();
        Controls.player.right.started += RightChange;
        Controls.player.right.canceled += RightChange;
        Controls.player.right.Enable();
        Controls.player.left.started += LeftChange;
        Controls.player.left.canceled += LeftChange;
        Controls.player.left.Enable();
        Controls.player.attack.performed += Attack;
        Controls.player.attack.Enable();
        Controls.player.dash.started += DashChange;
        Controls.player.dash.canceled += DashChange;
        Controls.player.dash.Enable();
        Controls.player.bow.performed += Bow;
        Controls.player.bow.Enable();
    }



    private void JumpChange(InputAction.CallbackContext obj){
        jumpHeld = ! jumpHeld;
    }
    private void RightChange(InputAction.CallbackContext obj){
        rightHeld = ! rightHeld;
    }
    private void LeftChange(InputAction.CallbackContext obj){
        leftHeld = ! leftHeld;
    }
    private void DashChange(InputAction.CallbackContext obj){
        dashHeld = ! dashHeld;
    }
    private void Bow(InputAction.CallbackContext obj){
        if (currentWeapon != weapon.bow && logic.bow){
            currentWeapon = weapon.bow;
            bowSprite.SetActive(true);
        }else{
            currentWeapon = weapon.none;
            bowSprite.SetActive(false);
        }
    }
    
    private void Jump(){
        if(logic.alive && logic.active){
            if(jumps < maxJumps){
                rigidBody.velocity = new Vector2(rigidBody.velocity.x,jumpPower);
        	    jumps++;
            }
        }
    }
    private void Right(){
        if(logic.alive && logic.active){
            rigidBody.velocity = new Vector2(speed,rigidBody.velocity.y);
            	renderer.flipX=false;
        }
    }
    private void Left(){
        if(logic.alive && logic.active){
            rigidBody.velocity = new Vector2(-speed,rigidBody.velocity.y);
            	renderer.flipX=true;
        }
    }
    private void OnDisable() {
        Controls.player.jump.Disable();
        Controls.player.right.Disable();
        Controls.player.left.Disable();
        Controls.player.attack.Disable();
        Controls.player.dash.Disable();
    }
    // Start is called before the first frame update
    void Start(){
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<Logic>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        time = Time.time;
        dashCooldownSec = 0;
        arrowCool = arrowCooldown;
    }

    private void Update() {
        dashCooldownSec -= (Time.time - time);
        if (shooting){
            arrowCooldown -= (Time.time - time);
            if (arrowCooldown <= 0){
                var newArrow = Instantiate(arrow, transform.position, transform.rotation);
                var arrowScript = newArrow.GetComponentInChildren<arrow>();
                if (renderer.flipX){
                    arrowScript.facing = orientation.left; 
                }else{
                    arrowScript.facing = orientation.right;
                }
                arrowCooldown = arrowCool;
                shooting = false;
            }
        }
        time = Time.time;
        if (jumpHeld){
            Jump();
        }if (rightHeld){
            Right();
        }if(leftHeld){
            Left();
        }if(dashHeld && dashCooldownSec <= 0){
            Dash();
        }
    }
    void Dash(){
    	if(logic.alive && logic.active && dash){
            dashCooldownSec = cooldown;
            Vector3 mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            if (mousePos.x < transform.position.x){
                    rigidBody.velocity = new Vector2(-dashSpeed,rigidBody.velocity.y);
                    renderer.flipX=true;
                }else{
                    rigidBody.velocity = new Vector2(dashSpeed,rigidBody.velocity.y);
                    renderer.flipX=false;
                }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision){
    	jumps=0;
    }
    private void Attack(InputAction.CallbackContext obj){
        if(logic.alive && logic.active){
            switch (currentWeapon){
                case weapon.bow:
                    shooting = true;
                    break;
                default:
                    break;
            }
        }
    }
}
