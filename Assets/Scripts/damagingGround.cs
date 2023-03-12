using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damagingGround : MonoBehaviour{
	public Logic logic;	
    // Start is called before the first frame update
    void Start(){
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<Logic>();
    }

    // Update is called once per frame
    void Update(){
        
    }
    private void OnCollisionEnter2D(Collision2D collision){
    	logic.damage(1);
    }
}
