using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bob : MonoBehaviour{
    [SerializeField] Logic logic;
    [SerializeField] GameObject ui;
    [SerializeField] door door;
    // Start is called before the first frame update
    void Start(){
        logic = GameObject.FindGameObjectWithTag("logic").GetComponent<Logic>();
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer==3){
            ui.SetActive(true);
	    logic.active = false;
	    if(door.openable == true){
		ui.transform.GetChild(1).gameObject.SetActive(false);
		ui.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(false);
		ui.transform.GetChild(0).gameObject.SetActive(false);
		ui.transform.GetChild(3).gameObject.SetActive(true);
	    }
	}
    }
    public void repaire(){
	if(logic.plan){
	    stopTalking();
	    door.openable =true;
	    logic.plan = false;
	}
    }
    public void stopTalking(){
	ui.SetActive(false);
	logic.active=true;
    }
}
