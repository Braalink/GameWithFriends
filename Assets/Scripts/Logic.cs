using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Logic : MonoBehaviour{
    int hp;
    public int maxHP;
    public GameObject gameOverScreen;
    public bool alive;
    public GameObject stopScreen;
    public bool active;
    public bool bow;
    public bool hammer;
    [SerializeField] healthSystem hs;
    public bool plan = false;
    
    void Start() {
        hp = maxHP;
        hs.DrawHearts(hp, maxHP);
        Debug.Log(SceneManager.GetActiveScene().name);
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.F11)){
            Screen.fullScreen = !Screen.fullScreen;
        }
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (active){
                stopScreen.SetActive(true);
                active=false;
            }
            else{
                continu();
            }
        }
    }
    public void refresh(){
        hs.DrawHearts(hp, maxHP);
    }
    public void fullHeal(){
        hp = maxHP;
        hs.DrawHearts(hp, maxHP);
    }
    public void continu(){
        stopScreen.SetActive(false);
        active = true;
    }
    public void quit(){
        Application.Quit();
    }
    public void GameOver(){
    	gameOverScreen.SetActive(true);
    	alive = false;
    }
    public void damage(int amount){
    	hp -= amount;
        hs.DrawHearts(hp, maxHP);
    	if(hp <=0){
    		GameOver();
    	}
    }

    public void heal(int amount){
    	if(hp<maxHP){
			hp += amount;
            hs.DrawHearts(hp, maxHP);
    	}
    }
    public void restart(){
    	SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
