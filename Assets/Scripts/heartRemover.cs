using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartRemover : MonoBehaviour{
    public Logic logic;
    // Start is called before the first frame update
    void Start(){
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<Logic>();
    }

    void OnTriggerEnter2D(Collider2D c){
        if(c.gameObject.name == "player"){
    	    logic.maxHP -= 1;
            logic.refresh();
            Destroy(gameObject);
        }
    }
}
