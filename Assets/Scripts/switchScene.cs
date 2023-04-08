using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class switchScene : MonoBehaviour{
    public void startScene(int id){
        SceneManager.LoadScene(id);
    }
    public void debug(){
        startScene(1);
    }

}
