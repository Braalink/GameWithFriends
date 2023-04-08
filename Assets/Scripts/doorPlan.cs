using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorPlan : MonoBehaviour{
    [SerializeField] Logic logic;
    void Start(){
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<Logic>();
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer==3){
            Destroy(gameObject);
	    logic.plan = true;
	}
    }
}
