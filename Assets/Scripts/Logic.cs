using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Logic : MonoBehaviour{
    public int hp;
    public int maxHP;
    public Text HPtxt;
    public GameObject gameOverScreen;
    public bool alive;
    
    public void GameOver(){
    	gameOverScreen.SetActive(true);
    	alive = false;
    }
    public void damage(int amount){
    	hp -= amount;
    	HPtxt.text = "HP:" + hp.ToString();
    	if(hp <=0){
    		GameOver();
    	}
    }

    public void heal(int amount){
    	hp += amount;
    	if(hp>maxHP){
			hp = maxHP;
    	}
    	HPtxt.text = "HP:" + hp.ToString();
    }
    public void restart(){
    	SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
